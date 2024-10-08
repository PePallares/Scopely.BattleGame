﻿using StackExchange.Redis;

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
        Task<SortedSetEntry[]> GetAllFromSortedSet(string setName);

        /// <summary>
        /// Adds the entity to the desired set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</param>
        /// <param name="score">The user</param>
        Task AddToSortedSet(string setName, string playerId, long score);

        /// <summary>
        /// Gets the player's score
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</param>
        /// <returns>The player's score</returns>
        Task<double?> GetPlayerScore(string setName, string playerId);

        /// <summary>
        /// Adds or increments the entity to the desired set
        /// </summary>
        /// <param name="setName">The set name</param>
        /// <param name="playerId">The player id</par
        Task SortedSetIncrement(string setName, string playerId, long score);
    }
}
