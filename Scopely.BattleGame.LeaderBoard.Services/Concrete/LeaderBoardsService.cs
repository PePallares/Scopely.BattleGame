﻿using Scopely.BattleGame.LeaderBoards.Repository;

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

        public Task<IEnumerable<LeaderBoard>> GetLeaderBoard(string leaderBoardName)
        {
            return _leaderBoardsRepository.GetLeaderBoard(leaderBoardName);
        }
    }
}
