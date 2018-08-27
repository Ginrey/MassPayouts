using System.Linq;
using MassPayouts.Data.Sql;

namespace MassPayouts.Net.BaseActions.Actions
{
    [ActionIdentifier(2, ActionType.ShowOwnerName)]
    public class OnShowOwnerName :IAction
    {
        public int WalletId { get; set; }
        public string Name { get; set; }

        public override DatabaseContext Execute(DatabaseContext db)
        {
            var wallet = db.Wallets.FirstOrDefault(w => w.Id == WalletId);
            if (wallet != null)
                Name = wallet.Contractors.Name;
            return db;
        }
    }                    
}
