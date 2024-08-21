namespace Scopely.BattleGame.LeaderBoards.Repository
{
    public interface ILeaderBoardsRepository
    {
        /// <summary>
        /// Gets all the entities of the given sorted set
        /// </summary>
        /// <param name="leaderBoardName">The set name</param>
        /// <returns>All the sorted set entities</returns>
        Task<LeaderBoard> GetLeaderBoard(string leaderBoardName);

        /// <summary>
        /// Adds the entity to the desired set
        /// </summary>
        /// <param name="leaderBoardName">The set name</param>
        /// <param name="playerId">The player id</param>
        /// <param name="score">The user</param>
        Task AddToLeaderBoard(string leaderBoardName, string playerId, long score);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="leaderBoardName"></param>
        /// <param name="playerName"></param>
        /// <returns></returns>
        Task<long> GetUserScore(string leaderBoardName, string playerName);

        /// <summary>
        /// Adds or increments the entity to the desired set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</par
        Task SortedSetIncrement(string setName, string playerId, long score);
    }
}
