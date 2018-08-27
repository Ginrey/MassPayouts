using System.Collections.Generic;

namespace MassPayouts.Model.Sql
{   
    public class TypeOfWallet
    {
        public int Id { get; set; }   
        public string Name { get; set; }

        public TypeOfWallet()
        {
            Wallets = new HashSet<Wallets>();
        }    

        public virtual ICollection<Wallets> Wallets { get; set; }
    }
}
