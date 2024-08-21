using Microsoft.Extensions.Options;
using Scopely.BattleGame.Redis;
using Scopely.BattleGame.Repositories.Interfaces;

namespace Scopely.BattleGame.LeaderBoards.Repository
{
    public class LeaderBoardsRepository : ILeaderBoardsRepository
    {
        private readonly IRedisRepository<LeaderBoard> _repository;
        private readonly RedisSettings _redisSettings;

        public LeaderBoardsRepository(IRedisRepository<LeaderBoard> repository, IOptions<RedisSettings> redisSettings)
        {
            _repository = repository;
            _redisSettings = redisSettings.Value;
        }

        public async Task<LeaderBoard> GetLeaderBoard(string leaderBoardName) 
        {
            var leaderBoardEntries = await _repository.GetAllFromSortedSet(leaderBoardName);
            var leaderBoard = new LeaderBoard()
            {
                Name = _redisSettings.BattleLeaderBoardName
            };

            var leaderBoardRanking = new List<LeaderBoardPlayer>();
            var rank = 1;

            foreach (var leaderBoardEntry in leaderBoardEntries) 
            {
                leaderBoardRanking.Add(new LeaderBoardPlayer() 
                {
                    playerName = leaderBoardEntry.Element.ToString(),
                    rank = rank,
                    score = Convert.ToInt64(leaderBoardEntry.Score),
                });

                rank++;
            }

            leaderBoard.LeaderBoardRanking = leaderBoardRanking;

            return leaderBoard;
        }

        public async Task AddToLeaderBoard(string leaderBoardName, string playerName, long score)
        {
            await _repository.AddToSortedSet(leaderBoardName, playerName, score);
        }

        public async Task<long> GetUserScore(string leaderBoardName, string playerName) 
        {
            var score = await _repository.GetPlayerScore(leaderBoardName, playerName);
            return score is null ? 0 : Convert.ToInt64(score);
        }

        public async Task SortedSetIncrement(string setName, string playerId, long score)
        {
            await _repository.SortedSetIncrement(setName, playerId, score);
        }
    }
}
