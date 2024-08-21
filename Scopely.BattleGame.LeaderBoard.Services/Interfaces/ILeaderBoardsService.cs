namespace Scopely.BattleGame.LeaderBoards.Services
{
    public interface ILeaderBoardsService
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
        /// Gets the player's score
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</param>
        /// <returns>The player's score</returns>
        Task<long> GetUserScore(string leaderBoardName, string playerName);

        /// <summary>
        /// Adds or increments the entity to the desired set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</par
        Task SortedSetIncrement(string setName, string playerId, long score);
    }
}
