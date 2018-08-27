using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassPayouts.Model.Sql
{      
    public class Transactions
    {
        public int Id { get; set; }

        public int WalletId { get; set; }    
      
        public decimal Amount { get; set; }

        [ForeignKey("TypeOfTransaction")]
        public int TypeId { get; set; }

        public DateTime Date { get; set; }

        public virtual TypeOfTransaction TypeOfTransaction { get; set; }

        public virtual Wallets Wallets { get; set; }
    }
}
