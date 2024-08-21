namespace Scopely.BattleGame.Players
{
    public class Player
    {
        public string Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public PlayerWallet Wallet { get; set; }
        public PlayerBattleAttributes BattleAttributes { get; set; }

        public Player()
        {
            Id = Guid.NewGuid().ToString();
            Description = string.Empty;
            Wallet = new PlayerWallet();
            BattleAttributes = new PlayerBattleAttributes();
        }

    }
}
