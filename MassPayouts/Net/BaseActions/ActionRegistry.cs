using System;
using System.Collections.Generic;
using System.Reflection;

namespace MassPayouts.Net.BaseActions
{
    public class ActionRegistry
    {
        static string[] _emptyGroups = { };
        static string[] _globalGroups = { string.Empty };
        static Type[] _emptyTypes = { };
        static object[] _emptyObjects = { };
        static Assembly[] _currentAssembly = { Assembly.GetExecutingAssembly() };   
        Dictionary<ActionIdentifier, Type> _actions = new Dictionary<ActionIdentifier, Type>();

        public static ActionRegistry Create(bool includeGlobal = true)
        {
            return Create(includeGlobal, _emptyGroups);
        }
        public static ActionRegistry Create(bool includeGlobal, params string[] groups)
        {
            return Create(_currentAssembly, includeGlobal, groups);
        }
        public static ActionRegistry Create(Assembly[] assemblies, bool includeGlobal, params string[] groups)
        {
            var registry = new ActionRegistry();
            registry.Register(assemblies, includeGlobal, groups);
            return registry;
        }      
      
        public ActionRegistry()
        {
           
        }
       
        public ActionRegistry(Assembly[] assemblies)
        {
            Register(assemblies, true, _emptyGroups);
        }
       
        public ActionRegistry(IEnumerable<Type> types)
        {       
            Register(types);
        }

        public void Register()
        {
            Register(true, _emptyGroups);
        }
        public void Register(params string[] groups)
        {
            Register(true, groups);
        }
        public void Register(bool includeGlobal)
        {
            Register(includeGlobal, _emptyGroups);
        }
        public void Register(bool includeGlobal, params string[] groups)
        {
            Register(_currentAssembly, includeGlobal, groups);
        }
        public void Register(Assembly[] assemblies, bool includeGlobal, params string[] groups)
        {
            var types = new Dictionary<string, HashSet<Type>>();

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (IAction.IsAction(type))
                    {
                        var actionGroups = IAction.GetActionGroups(type);
                        if (actionGroups.Length == 0)
                        {
                            actionGroups = _globalGroups;
                        }

                        foreach (var group in actionGroups)
                        {
                            if (!types.TryGetValue(group, out var set))
                            {
                                set = new HashSet<Type>();
                                types[group] = set;
                            }
                            set.Add(type);
                        }
                    }
                }
            }

            if (includeGlobal)
            {
                if (types.TryGetValue(string.Empty, out var set))
                {
                    Register(set);
                }
            }
            foreach (var group in groups)
            {
                if (types.TryGetValue(group, out var set))
                {
                    Register(set);
                }
            }
        }
        public void Register(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                Register(type);
            }
        }
        public void Register<T>() where T : IAction
        {
            Register(typeof(T));
        }
        public void Register(Type type)
        {
            var actionIds = IAction.GetActionIdentifiers(type);

            foreach (var actionId in actionIds)
            {
                Register(type, actionId);
            }
        }
        public void Register(Type type, int actionId, ActionType actionType)
        {
            Register(type, new ActionIdentifier(actionId, actionType));
        }
        public void Register(Type type, ActionIdentifier actionId)
        {
            if (!IAction.IsAction(type))
            {
                return;
            }
            IAction.ActionRegistry = this;
            _actions[actionId] = type;
        }
        public IAction GetAction(int actionId, ActionType actionType)
        {
            return GetAction(new ActionIdentifier(actionId, actionType));
        }
        public IAction GetAction(ActionIdentifier actionId)
        {
            return GetActionType(actionId).GetConstructor(_emptyTypes).Invoke(_emptyObjects) as IAction;
        }
        public bool TryGetAction(int actionId, ActionType actionType, out IAction action)
        {
            return TryGetAction(new ActionIdentifier(actionId, actionType), out action);
        }
        public bool TryGetAction(ActionIdentifier actionId, out IAction action)
        {
            if (TryGetActionType(actionId, out var type))
            {
                action = type.GetConstructor(_emptyTypes).Invoke(_emptyObjects) as IAction;
                return true;
            }

            action = null;
            return false;
        }

        public Type GetActionType(int actionId, ActionType actionType)
        {
            return GetActionType(new ActionIdentifier(actionId, actionType));
        }
        public Type GetActionType(ActionIdentifier actionId)
        {
            if (!TryGetActionType(actionId, out var type))
            {
                throw new Exception("Unknown action");
            }
            return type;
        }

        public bool TryGetActionType(int actionId, ActionType actionType, out Type type)
        {
            return TryGetActionType(new ActionIdentifier(actionId, actionType), out type);
        }
        public bool TryGetActionType(ActionIdentifier actionId, out Type type)
        {
            return _actions.TryGetValue(actionId, out type);
        }

        public bool Contains(int actionId, ActionType actionType)
        {
            return Contains(new ActionIdentifier(actionId, actionType));
        }
        public bool Contains(ActionIdentifier actionId)
        {
            return _actions.ContainsKey(actionId);
        }

        public void Remove<T>() where T : IAction
        {
            RemoveRange(IAction.GetActionIdentifiers<T>());
        }
        public void Remove(ActionIdentifier actionId)
        {
            _actions.Remove(actionId);
        }
        public void RemoveRange(ActionIdentifier[] actionIds)
        {
            foreach (var actionId in actionIds)
            {
                _actions.Remove(actionId);
            }
        }
    }
}
