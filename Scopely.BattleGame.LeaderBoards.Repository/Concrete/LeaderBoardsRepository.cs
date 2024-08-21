using Scopely.BattleGame.Repositories.Interfaces;

namespace Scopely.BattleGame.LeaderBoards.Repository
{
    public class LeaderBoardsRepository : ILeaderBoardsRepository
    {
        private readonly IRedisRepository<LeaderBoard> _repository;

        public LeaderBoardsRepository(IRedisRepository<LeaderBoard> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LeaderBoard>> GetLeaderBoard(string leaderBoardName) 
        {
            return await _repository.GetAllFromSortedSet(leaderBoardName);
        }

        public async Task AddToLeaderBoard(string leaderBoardName, string playerName, long score)
        {
            await _repository.AddToSortedSet(leaderBoardName, playerName, score);
        }
    }
}
