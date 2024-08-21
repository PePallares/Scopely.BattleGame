namespace Scopely.BattleGame.Players
{
    public class PlayerBattleAttributes
    {
        public int Attack {  get; set; }
        public int HitPoints { get; set; }
        public int Luck { get; set; }

        public PlayerBattleAttributes()
        {
            Attack = 10;
            HitPoints = 100;
            Luck = 7;
        }
    }
}
