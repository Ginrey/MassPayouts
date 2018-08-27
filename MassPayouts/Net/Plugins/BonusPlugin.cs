namespace MassPayouts.Net.Plugins
{
    public class BonusPlugin : Plugin
    {       
        public decimal Money { get; set; }

        protected internal override void Initialize()
        {
            Money = 50;
        }

        protected internal override void OnStart()
        {
            if (!Enabled) return;
            foreach (var wallet in Context.Wallets)
            {
                wallet.Balance += Money;
            }
        }
    }
}
