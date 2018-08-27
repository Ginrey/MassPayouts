using MassPayouts.Data.Sql;

namespace MassPayouts.Net
{
    public class Plugin
    {        
        public DatabaseContext Context { get; set; }
        public virtual bool Enabled { get; set; }

        protected internal virtual void Initialize()
        {
        }

        protected internal virtual void OnStart()
        {

        }

        protected internal virtual void OnStop()
        {

        }
    }
}
