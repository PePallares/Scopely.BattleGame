
using Scopely.BattleGame.Repositories.Interfaces;

namespace Scopely.BattleGame.Players.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IRedisRepository<Player> _repository;

        public PlayerRepository(IRedisRepository<Player> repository) 
        { 
            _repository = repository;
        }

        public async Task AddPlayer(Player player)
        {
            await _repository.Add(player.Id, player);
        }

        public async Task<Player?> GetPlayer(string playerId)
        {
            return await _repository.GetById(playerId);
        }

        public async Task RemovePlayer(string playerId)
        {
            await _repository.Remove(playerId);
        }

        public async Task UpdatePlayer(Player player)
        {
            await _repository.Update(player.Id, player);
        }
    }
}
