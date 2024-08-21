namespace Scopely.BattleGame.Repositories.Interfaces
{
    public  interface IRedisRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all the entities of the given set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <returns>All the set entities</returns>
        Task<IEnumerable<T>> GetAll(string setName);

        /// <summary>
        /// Adds the entity to the desired set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="entity">The entity to add</param>
        Task AddToSet(string setName, T entity);

        /// <summary>
        /// Gets all the entities of the given sorted set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <returns>All the sorted set entities</returns>
        Task<IEnumerable<T>> GetAllFromSortedSet(string setName);

        /// <summary>
        /// Adds the entity to the desired set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</param>
        /// <param name="score">The user</param>
        Task AddToSortedSet(string setName, string playerId, long score);
    }
}
