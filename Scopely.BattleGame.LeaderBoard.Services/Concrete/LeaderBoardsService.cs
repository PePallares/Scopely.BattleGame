using Scopely.BattleGame.LeaderBoards.Repository;

namespace Scopely.BattleGame.LeaderBoards.Services
{
    public class LeaderBoardsService : ILeaderBoardsService
    {
        private readonly ILeaderBoardsRepository _leaderBoardsRepository;

        public LeaderBoardsService(ILeaderBoardsRepository leaderBoardsRepository)
        {
            _leaderBoardsRepository = leaderBoardsRepository;
        }

        public async Task AddToLeaderBoard(string leaderBoardName, string playerName, long score)
        {
            await _leaderBoardsRepository.AddToLeaderBoard(leaderBoardName, playerName, score);
        }

        public async Task<LeaderBoard> GetLeaderBoard(string leaderBoardName)
        {
            return await _leaderBoardsRepository.GetLeaderBoard(leaderBoardName);
        }

        public async Task<long> GetUserScore(string leaderBoardName, string playerName)
        {
            return await _leaderBoardsRepository.GetUserScore(leaderBoardName, playerName);
        }

        public async Task SortedSetIncrement(string setName, string playerId, long score)
        {
            await _leaderBoardsRepository.SortedSetIncrement(setName, playerId, score);
        }
    }
}
