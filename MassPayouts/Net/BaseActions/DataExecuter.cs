using MassPayouts.Data.Sql;

namespace MassPayouts.Net.BaseActions
{
    public abstract class DataExecuter
    {
        public virtual DatabaseContext Execute(DatabaseContext db)
        {
            return db;
        }   
    }
}
