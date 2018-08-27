using System;
using MassPayouts.Data.Sql;

namespace MassPayouts.Net.BaseActions
{
    public class IAction: DataExecuter
    {
        DatabaseContext Context;    
    
        static Type _actionType = typeof(IAction);
        static Type _actionIdType = typeof(ActionIdentifier);
        static Type _actionGroupType = typeof(ActionGroup);
        public static ActionRegistry ActionRegistry;


        public static bool IsAction(Type type)
        {
            return type.IsSubclassOf(_actionType);
        }
        static void ValidateGameAction(Type type)
        {
            if (!IsAction(type))
            {
                throw new Exception("Is no action");
            }
        }

        public static ActionIdentifier GetOneActionIdentifier<T>() where T : IAction
        {
            return GetOneActionIdentifier(typeof(T));
        }
        public static ActionIdentifier GetOneActionIdentifier(IAction action)
        {
            return GetOneActionIdentifier(action.GetType());
        }
        public static ActionIdentifier GetOneActionIdentifier(Type type)
        {
            ValidateGameAction(type);
            var identifiers = GetActionIdentifiers(type);
            if (identifiers.Length != 1)
            {
                throw new Exception("Can't select one action identifier");
            }
            return identifiers[0];
        }
        public static ActionIdentifier[] GetActionIdentifiers<T>() where T : IAction
        {
            return GetActionIdentifiers(typeof(T));
        }
        public static ActionIdentifier[] GetActionIdentifiers(IAction action)
        {
            return GetActionIdentifiers(action.GetType());
        }
        public static ActionIdentifier[] GetActionIdentifiers(Type type)
        {
            ValidateGameAction(type);
            var attributes = type.GetCustomAttributes(typeof(ActionIdentifier), false);
            if (ActionRegistry != null)
            {
                ActionIdentifier[] result = new ActionIdentifier[1];
                foreach (var att in (ActionIdentifier[])attributes)
                    if (ActionRegistry.Contains(att))
                    {
                        result[0] = att;
                        return result;
                    }
            }
            return attributes as ActionIdentifier[];
        }

        public static string[] GetActionGroups<T>() where T : IAction
        {
            return GetActionGroups(typeof(T));
        }
        public static string[] GetActionGroups(IAction action)
        {
            return GetActionGroups(action.GetType());
        }
        public static string[] GetActionGroups(Type type)
        {
            ValidateGameAction(type);
            var attributes = type.GetCustomAttributes(typeof(ActionGroup), false);
            var groups = new string[attributes.Length];
            for (var i = 0; i < attributes.Length; i++)
            {
                groups[i] = (attributes[i] as ActionGroup).GroupName;
            }
            return groups;
        }
    }     
}
