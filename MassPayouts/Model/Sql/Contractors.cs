using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MassPayouts.Model.Sql
{    
    public class Contractors
    {
        public int Id { get; set; }   

        [ForeignKey("TypeOfContractor")]
        public int TypeId { get; set; }  

        public string Name { get; set; }
   
        public Contractors()
        {
            Wallets = new HashSet<Wallets>();
        }   
        
        public virtual TypeOfContractor TypeOfContractor { get; set; }
        public virtual ICollection<Wallets> Wallets { get; set; }
    }
}
