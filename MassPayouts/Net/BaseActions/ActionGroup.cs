using System;

namespace MassPayouts.Net.BaseActions
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionGroup : Attribute
    {
        public string GroupName { get; private set; }

        public ActionGroup()
        {
            GroupName = string.Empty;
        }

        public ActionGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentException("Argument is null or empty");
            }    
            GroupName = groupName;
        }
    }
}
