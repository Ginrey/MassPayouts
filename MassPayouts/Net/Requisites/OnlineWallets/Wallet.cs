namespace MassPayouts.Net.Requisites.OnlineWallets
{
    public abstract class Wallet : IRequisites
    {
        public abstract string WalletId { get; set; }
        public string GetRequisites()
        {
            return WalletId;
        }
    }
}
