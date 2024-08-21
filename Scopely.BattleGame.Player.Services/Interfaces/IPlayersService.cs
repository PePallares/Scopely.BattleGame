namespace Scopely.BattleGame.Players.Services
{
    public  interface IPlayersService
    {
        /// <summary>
        /// Gets an existing player from de DDBB
        /// </summary>
        /// <param name="playerId">The desired player identifier</param>
        /// <returns>The player</returns>
        Task<Player?> GetPlayer(string playerId);

        /// <summary>
        /// Creates a new player to the DDBB
        /// </summary>
        /// <param name="player">The player to add</param>
        Task CreatePlayer(NewPlayerRequest newPlayer);

        /// <summary>
        /// Updates an existing player
        /// </summary>
        Task UpdatePlayer(Player player);

        /// <summary>
        /// Removes an existing player from the DDBB
        /// </summary>
        Task RemovePlayer(string playerId);

    }
}
