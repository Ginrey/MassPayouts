using System;

namespace MassPayouts.Net.BaseActions
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ActionIdentifier : Attribute
    {       
        public int ActionId { get; }
        public ActionType ActionType { get; }   
     
        public ActionIdentifier(int actionId, ActionType actionType)
        {
            ActionId = actionId;
            ActionType = actionType;
        }

        public override bool Equals(object obj)
        {
            if (obj is ActionIdentifier pid)
            {
                return pid.ActionId == ActionId && pid.ActionType == ActionType;
            }    
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return (int)(ActionId ^ ((uint)ActionType << 16));
        }
    }
}
