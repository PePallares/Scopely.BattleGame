using Scopely.BattleGame.Players;

namespace Scopely.BattleGame.Battles
{
    public class Battle
    {
        public Player attacker { get; set; }
        public Player defender { get; set; }

        public Battle(Player attacker, Player defender)
        {
            this.attacker = attacker;
            this.defender = defender;
        }
    }
}
