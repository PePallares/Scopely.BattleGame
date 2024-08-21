namespace Scopely.BattleGame.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds a new entity to the DDBB
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="value">Entity to add</param>
        Task Add(string id, T entity);

        /// <summary>
        /// Gets the entity with the given identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <returns>The value</returns>
        Task<T?> GetById(string id);

        /// <summary>
        /// Remove from the DDBB the entity with the given identifier
        /// </summary>
        /// <param name="id">Entity identifier</param>
        Task Remove(string id);

        /// <summary>
        /// Updates an existing entity in the DDBB
        /// </summary>
        /// <param name="id">Entity identifier</param>
        /// <param name="value">Entity to update</param>
        Task Update(string id, T entity);

    }
}
