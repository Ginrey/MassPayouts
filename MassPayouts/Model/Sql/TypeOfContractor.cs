using System.Collections.Generic;

namespace MassPayouts.Model.Sql
{    
    public class TypeOfContractor
    {
        public int Id { get; set; }         
      
        public string TypeName { get; set; }   
        public TypeOfContractor()
        {
            Contractors = new HashSet<Contractors>();
        }   
      
        public virtual ICollection<Contractors> Contractors { get; set; }

        public virtual TypeOfContractor TypeOfContractor1 { get; set; }

        public virtual TypeOfContractor TypeOfContractor2 { get; set; }
    }
}
