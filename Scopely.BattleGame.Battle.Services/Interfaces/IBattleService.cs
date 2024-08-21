namespace Scopely.BattleGame.Battles.Services
{
    public interface IBattleService
    {
        Task SubmitBattle(string attackerId, string defenderId);
    }
}
