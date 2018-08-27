using System.Linq;
using MassPayouts.Data.Sql;

namespace MassPayouts.Net.BaseActions.Actions
{
    [ActionIdentifier(0, ActionType.Bonus)]
    public class OnBonusAction :IAction
    {
        public int WalletId { get; set; }
        public decimal Money { get; set; }

        public override DatabaseContext Execute(DatabaseContext db)
        {
            var wallet = db.Wallets.FirstOrDefault(w => w.Id == WalletId);
            if (wallet != null)
                wallet.Balance += Money;
            return db;
        }
    }                    
}
