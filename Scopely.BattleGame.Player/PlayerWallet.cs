namespace Scopely.BattleGame.Players
{
    public class PlayerWallet
    {
        private readonly int _maxWalletValue = 1000000000;

        private int _gold;
        public int Gold 
        {
            get
            {
                return _gold;
            }

            set
            {
                _gold = Math.Min(value, _maxWalletValue);
            }
        }

        private int _silver;
        public int Silver
        {
            get
            {
                return _silver;
            }

            set
            {
                _silver = Math.Min(value, _maxWalletValue);
            }
        }

        public PlayerWallet() 
        { 
            Gold = 0;
            Silver = 0;
        }
    }
}
