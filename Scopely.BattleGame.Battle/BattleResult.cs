namespace Scopely.BattleGame.Battles
{
    public class BattleResult
    {
        public BattleStatus Status { get; set; }
        public long Score { get; set; }
    }

    public enum BattleStatus
    {
        Win,
        Defeat,
        Error
    }
}
