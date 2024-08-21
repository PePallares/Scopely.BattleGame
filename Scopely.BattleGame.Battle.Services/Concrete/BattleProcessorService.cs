
using Microsoft.Extensions.Logging;
using Scopely.BattleGame.LeaderBoards.Services;
using Scopely.BattleGame.Players;

namespace Scopely.BattleGame.Battles.Services
{
    public class BattleProcessorService : IBattleProcessorService
    {
        private Queue<Battle> _battleQueue;
        private Random _random;
        private readonly ILeaderBoardsService _leaderBoardsService;

        public ILogger<BattleProcessorService> _logger { get; set; }

        public BattleProcessorService(ILogger<BattleProcessorService> logger, ILeaderBoardsService leaderBoardsService)
        {
            _battleQueue = new Queue<Battle>();
            _random = new Random();

            _logger = logger;
            _leaderBoardsService = leaderBoardsService;
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
            var attackerHP = battle.attacker.BattleAttributes.HitPoints;
            var defenderHP = battle.defender.BattleAttributes.HitPoints;

            while (CheckHitPoints(battle)) 
            {
                var attackerAttack = CalculateAttack(attackerHP, battle.attacker.BattleAttributes);
                var attackerDamage = CalculateDamage(attackerAttack, battle.defender.BattleAttributes);
                battle.defender.BattleAttributes.HitPoints -= attackerDamage;

                _logger.LogInformation($"Attacker {battle.attacker.Id} has done {attackerDamage} to defender {battle.defender.Id}");

                if (battle.defender.BattleAttributes.HitPoints <= 0) 
                {
                    break;
                }

                var defenderAttack = CalculateAttack(defenderHP, battle.defender.BattleAttributes);
                var defenderDamage = CalculateDamage(defenderAttack, battle.attacker.BattleAttributes);
                battle.attacker.BattleAttributes.HitPoints -= defenderDamage;

                _logger.LogInformation($"Defender {battle.defender.Id} has done {defenderDamage} to attacker {battle.attacker.Id}");
            }

            await GiveRewards(battle);
        }

        private bool CheckHitPoints(Battle battle) 
        {
            return battle.attacker.BattleAttributes.HitPoints > 0 &&
                battle.defender.BattleAttributes.HitPoints > 0;
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
            //Calculate the rewards for the winner using a random within 10 and 20
            //Calculate the proportion of each currency with a random withon 10 a the prvious value
            //Use the leaderboard service to modify the values
        }
    }
}
