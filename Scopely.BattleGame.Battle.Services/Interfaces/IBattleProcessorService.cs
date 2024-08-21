namespace Scopely.BattleGame.Battles.Services
{
    public interface IBattleProcessorService
    {
        /// <summary>
        /// Process all the enqueded battels
        /// </summary>
        Task ProcessBattles();
        /// <summary>
        /// Enqueue the battle to be processed
        /// </summary>
        /// <param name="battle">The battle to enqueue</param>
        Task EnqueueBattle(Battle battle);
    }
}
