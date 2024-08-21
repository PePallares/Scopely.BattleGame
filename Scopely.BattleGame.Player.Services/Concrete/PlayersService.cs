using Scopely.BattleGame.Players.Repository;

namespace Scopely.BattleGame.Players.Services
{
    public class PlayersService : IPlayersService
    {
        private IPlayerRepository _playersRepository;

        private const int _maxNameLength = 20;
        private const int _maxdescriptionLength = 1000;

        public PlayersService(IPlayerRepository playerRepository) 
        {
            _playersRepository = playerRepository;
        }

        public async Task<Player?> GetPlayer(string playerId) 
        {
            return await _playersRepository.GetPlayer(playerId);
        }

        public async Task CreatePlayer(Player player)
        {
            if (ValidatePlayerValues(player))
            {
                await _playersRepository.AddPlayer(player);
            }
        }

        public async Task UpdatePlayer(Player player)
        {

            await _playersRepository.UpdatePlayer(player);
        }

        public async Task RemovePlayer(string playerId)
        {
            await _playersRepository.RemovePlayer(playerId);
        }

        private bool ValidatePlayerValues(Player player)
        {
            if (String.IsNullOrEmpty(player.Name) || 
                player.Name.Length > _maxNameLength)
            {
                return false;
            }
            
            if (String.IsNullOrEmpty(player.Description) || 
                player.Description.Length > _maxdescriptionLength) 
            {
                return false;
            }

            return true;
        }
    }
}
