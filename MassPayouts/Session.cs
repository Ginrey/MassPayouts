using System;
using MassPayouts.Data.Sql;
using MassPayouts.Net;
using MassPayouts.Net.BaseActions;
using MassPayouts.Net.BaseActions.Actions;
using MassPayouts.Net.Plugins;

namespace MassPayouts
{
    public class Session
    {
        public  DatabaseContext Context { get; set; }
        public PluginManager PluginManager { get; set; }
        public ActionRegistry ActionRegistry { get; set; }
        public ActionHandler ActionHandler { get; set; }
        public TransfersHandler TransfersHandler { get; set; }

        public Session()
        {
            Context = new DatabaseContext();
            PluginManager = new PluginManager(Context);
            ActionRegistry = ActionRegistry.Create();
            ActionHandler = new ActionHandler();
            TransfersHandler = new TransfersHandler(this);
            Initialize();
        }

        public void Initialize()
        {
            PluginManager.Register<BonusPlugin>();
            PluginManager.Register<CommissionPlugin>();
            ActionHandler.AddHandler<OnShowOwnerName>(Handler);
        }

        void Handler(object sender, ActionEventArgs e)
        {
            if(e.Action is OnShowOwnerName ownerName)
            Console.WriteLine(ownerName.Name);
        }
    }
}
