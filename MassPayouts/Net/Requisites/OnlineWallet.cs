using MassPayouts.Net.Requisites.OnlineWallets;

namespace MassPayouts.Net.Requisites
{
    public class OnlineWallet : IRequisites
    {
        public Wallet Wallet { get; set; }
        public string GetRequisites()
        {
            return Wallet.GetRequisites();
        }
    }
}
