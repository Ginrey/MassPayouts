using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassPayouts.Model.Sql
{     
    public  class Wallets
    {
        public int Id { get; set; } 
        public decimal Balance { get; set; }   
        public string Requisites { get; set; }

        [ForeignKey("TypeOfWallet")]
        public int TypeId { get; set; }   
        public int OwnerId { get; set; }   
        
        public Wallets()
        {
            Transactions = new HashSet<Transactions>();
        }     
     
        public string Description { get; set; }

        public virtual Contractors Contractors { get; set; }

        public virtual ICollection<Transactions> Transactions { get; set; }

        public virtual TypeOfWallet TypeOfWallet { get; set; }
    }
}
