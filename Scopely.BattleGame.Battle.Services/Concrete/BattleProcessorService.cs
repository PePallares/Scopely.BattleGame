
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Scopely.BattleGame.LeaderBoards.Services;
using Scopely.BattleGame.Players;
using Scopely.BattleGame.Players.Services;
using Scopely.BattleGame.Redis;

namespace Scopely.BattleGame.Battles.Services
{
    public class BattleProcessorService : IBattleProcessorService
    {
        private Queue<Battle> _battleQueue;
        private Random _random;

        private readonly ILeaderBoardsService _leaderBoardsService;
        private readonly IPlayersService _playersService;
        private ILogger<BattleProcessorService> _logger;
        private readonly RedisSettings _redisSettings;

        public BattleProcessorService(
            ILogger<BattleProcessorService> logger,
            ILeaderBoardsService leaderBoardsService, 
            IPlayersService playersService,
            IOptions<RedisSettings> redisSettings)
        {
            _battleQueue = new Queue<Battle>();
            _random = new Random();

            _logger = logger;
            _leaderBoardsService = leaderBoardsService;
            _playersService = playersService;
            _redisSettings = redisSettings.Value;
        }

        public async Task EnqueueBattle(Battle battle) 
        {
            _battleQueue.Enqueue(battle);
            _logger.LogInformation($"Battle within attacker {battle.attacker.Id} and defender {battle.defender.Id} enqueued");
            ProcessBattles();
        }

        public async Task ProcessBattles()
        {
            while (_battleQueue.Count > 0)
            {
                Battle battle = _battleQueue.Dequeue();
                await PerformBattle(battle);
            }
        }

        private async Task PerformBattle(Battle battle) 
        {
            var attackerRemainingHP = battle.attacker.BattleAttributes.HitPoints;
            var defenderRemainginHP = battle.defender.BattleAttributes.HitPoints;

            while (CheckRemainingHitPoints(attackerRemainingHP, defenderRemainginHP)) 
            {
                var attackerAttack = CalculateAttack(attackerRemainingHP, battle.attacker.BattleAttributes);
                var attackerDamage = CalculateDamage(attackerAttack, battle.defender.BattleAttributes);
                battle.defender.BattleAttributes.HitPoints -= attackerDamage;

                _logger.LogInformation($"Attacker {battle.attacker.Id} has done {attackerDamage} to defender {battle.defender.Id}");

                if (battle.defender.BattleAttributes.HitPoints <= 0) 
                {
                    break;
                }

                var defenderAttack = CalculateAttack(defenderRemainginHP, battle.defender.BattleAttributes);
                var defenderDamage = CalculateDamage(defenderAttack, battle.attacker.BattleAttributes);
                battle.attacker.BattleAttributes.HitPoints -= defenderDamage;

                _logger.LogInformation($"Defender {battle.defender.Id} has done {defenderDamage} to attacker {battle.attacker.Id}");
            }

            await GiveRewards(battle);
        }

        private bool CheckRemainingHitPoints(int attackerRemainingHP, int defenderRemainginHP) 
        {
            return attackerRemainingHP > 0 && defenderRemainginHP > 0;
        }

        private int CalculateAttack(int initialHP, PlayerBattleAttributes playerBattleAttributes) 
        {
            var percentilRemaining = playerBattleAttributes.HitPoints / initialHP;
            var attack = playerBattleAttributes.Attack * Math.Max(0.5, percentilRemaining);
            return Convert.ToInt32(attack);
        }

        private int CalculateDamage(int attackerAttack, PlayerBattleAttributes battleAttributes)
        {
            if (_random.Next(0, 100) < battleAttributes.Luck)
            {
                return 0;
            }

            return attackerAttack;
        }

        private async Task GiveRewards(Battle battle) 
        {
            var stealPercentatge = _random.Next(10, 21);
            var goldStealPercentatge = _random.Next(0, stealPercentatge);
            var silverStealPercentatge = stealPercentatge - goldStealPercentatge;

            var winner = battle.attacker.BattleAttributes.HitPoints > 0 ? battle.attacker : battle.defender;
            var loser = battle.attacker.BattleAttributes.HitPoints <= 0 ? battle.attacker : battle.defender;

            var stolenGold = loser.Wallet.Gold * goldStealPercentatge;
            var stolenSilver = loser.Wallet.Silver * silverStealPercentatge;

            winner.Wallet.Gold += stolenGold;
            winner.Wallet.Silver += stolenSilver;

            await _playersService.UpdatePlayer(winner);
            await _leaderBoardsService.AddToLeaderBoard(_redisSettings.BattleLeaderBoardName, winner.Name, stolenGold + stolenSilver);

            loser.Wallet.Gold -= stolenGold;
            loser.Wallet.Silver -= stolenSilver;

            await _playersService.UpdatePlayer(loser);
            await _leaderBoardsService.AddToLeaderBoard(_redisSettings.BattleLeaderBoardName, loser.Name, - stolenGold - stolenSilver);
        }
    }
}
