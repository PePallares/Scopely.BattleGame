using Scopely.BattleGame.Players.Services;

namespace Scopely.BattleGame.Battles.Services
{
    public class BattleService : IBattleService
    {
        private readonly IPlayersService _playersService;
        private readonly IBattleProcessorService _battleProcessorService;

        public BattleService(IPlayersService playersService, IBattleProcessorService battleProcessorService)
        {
            _playersService = playersService;
            _battleProcessorService = battleProcessorService;
        }

        public async Task SubmitBattle(string attackerId, string defenderId)
        {
            var attacker = await _playersService.GetPlayer(attackerId);
            var defender = await _playersService.GetPlayer(defenderId);

            if (attacker is not null || defender is not null) 
            {
                var battle = new Battle(attacker, defender);
                await _battleProcessorService.EnqueueBattle(battle);
            }
        }
    }
}
