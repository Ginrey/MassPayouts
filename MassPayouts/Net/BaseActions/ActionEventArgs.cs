using System;

namespace MassPayouts.Net.BaseActions
{
    public class ActionEventArgs : EventArgs
    {
        public ActionIdentifier ActionId { get; private set; }
        public IAction Action { get; private set; }

        public ActionEventArgs(ActionIdentifier actionId, IAction action)
        {
            ActionId = actionId;
            Action = action;
        }

        public override string ToString()
        {
            return ActionId.ToString();
        }
    }
}
