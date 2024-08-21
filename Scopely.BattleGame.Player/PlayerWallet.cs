namespace Scopely.BattleGame.Players
{
    public class PlayerWallet
    {
        private readonly int _maxWalletValue = 1000000000;
        public int Gold 
        {
            get
            {
                return Gold;
            }

            set
            {
                Gold = Math.Min(value, _maxWalletValue);
            }
        }

        public int Silver
        {
            get
            {
                return Silver;
            }

            set
            {
                Silver = Math.Min(value, _maxWalletValue);
            }
        }

        public PlayerWallet() 
        { 
            Gold = 0;
            Silver = 0;
        }
    }
}
