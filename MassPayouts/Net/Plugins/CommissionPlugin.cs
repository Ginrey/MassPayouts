namespace MassPayouts.Net.Plugins
{
    public class CommissionPlugin: Plugin
    {    
        public int Percent { get; set; }

        protected internal override void Initialize()
        {
            Percent = 10;
        }

        protected internal override void OnStart()
        {
            if(!Enabled) return;
            foreach (var wallet in Context.Wallets)
            {
                wallet.Balance -= wallet.Balance / 100 * Percent;
            }  
        }
    }
}
