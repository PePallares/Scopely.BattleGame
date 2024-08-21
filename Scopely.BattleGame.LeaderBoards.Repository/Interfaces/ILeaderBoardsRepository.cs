namespace Scopely.BattleGame.LeaderBoards.Repository
{
    public interface ILeaderBoardsRepository
    {
        /// <summary>
        /// Gets all the entities of the given sorted set
        /// </summary>
        /// <param name="leaderBoardName">The set name</param>
        /// <returns>All the sorted set entities</returns>
        Task<IEnumerable<LeaderBoard>> GetLeaderBoard(string leaderBoardName);

        /// <summary>
        /// Adds the entity to the desired set
        /// </summary>
        /// <param name="leaderBoardName">The set name</param>
        /// <param name="playerId">The player id</param>
        /// <param name="score">The user</param>
        Task AddToLeaderBoard(string leaderBoardName, string playerId, long score);
    }
}
