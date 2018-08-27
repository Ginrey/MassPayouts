using System.Collections.Generic;

namespace MassPayouts.Model.Sql
{   
   
    public class TypeOfTransaction
    {
        public int Id { get; set; }   
        public string Name { get; set; }

        public TypeOfTransaction()
        {
            Transactions = new HashSet<Transactions>();
        }      

        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
