using System.Linq;
using MassPayouts.Model.Sql;

namespace MassPayouts.Net
{
    public class TransfersHandler
    {
        public Session Session { get; set; }

        public TransfersHandler(Session session)
        {
            Session = session;
        }

        public void Transfer(Contractors contractor, decimal amount)
        {
            var wallet = contractor.Wallets.FirstOrDefault();
            if (wallet != null)
                wallet.Balance += amount;
        }

        public void Transfer(IRequisites requisites, decimal amount)
        {
            var wallet = Session.Context.Wallets.FirstOrDefault(w => w.Requisites == requisites.GetRequisites());
            if (wallet != null)
                wallet.Balance += amount;
        }
    }
}
