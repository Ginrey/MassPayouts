using System.Collections.Generic;

namespace MassPayouts.Net.BaseActions
{
    public class ActionHandler
    {
        protected Dictionary<ActionIdentifier, List<ActionEventHandler>> Handlers;      
        
        public ActionHandler()
        {
            Handlers = new Dictionary<ActionIdentifier, List<ActionEventHandler>>();   
        }

        public virtual void AddHandler<T>(ActionEventHandler handler) where T : IAction
        {
            var actionIds = IAction.GetActionIdentifiers<T>();

            foreach (var id in actionIds)
            {
                AddHandler(id, handler);
            }
        }

        public virtual void AddHandler(int actionId, ActionType actionType, ActionEventHandler handler)
        {
            AddHandler(new ActionIdentifier(actionId, actionType), handler);
        }

        public virtual void AddHandler(ActionIdentifier actionId, ActionEventHandler handler)
        {
            if (!Handlers.TryGetValue(actionId, out var handlersList))
            {
                handlersList = new List<ActionEventHandler>();
                Handlers.Add(actionId, handlersList);
            }
            handlersList.Add(handler);
        }  
      
        public virtual bool ContainsHandler(ActionIdentifier actionId)
        {
            return Handlers.ContainsKey(actionId);
        }

        public void HandleAction(Session session, ActionIdentifier actionId, IAction action)
        {
            if (Handlers.TryGetValue(actionId, out var handlersList))
            {
                var eventArgs = new ActionEventArgs(actionId, action);
                foreach (var handler in handlersList)
                {
                    handler?.Invoke(this, eventArgs);
                }
            }
        }
    }
}
