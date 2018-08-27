using System;
using System.Collections.Generic;
using MassPayouts.Data.Sql;

namespace MassPayouts.Net
{
    public class PluginManager
    {
        static Type[] _emptyTypes = { };
        static object[] _emptyArgs = { };
        static Type _pluginType = typeof(Plugin);

        public DatabaseContext Context { get; }

        Dictionary<Type, Plugin> Plugins { get; }

        public PluginManager(DatabaseContext context)
        {
            Context = context;
            Plugins = new Dictionary<Type, Plugin>();
        }                  

        public int Count => Plugins.Count;

        public static bool IsPlugin(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException();
            }
            return type.IsSubclassOf(_pluginType);
        }
        static void ValidateType(Type type)
        {
            if (!IsPlugin(type))
            {
                throw new ArgumentException();
            }
        }

        public T Register<T>() where T : Plugin
        {
            return Register(typeof(T)) as T;
        }
        public Plugin Register(Type type)
        {
            ValidateType(type);

            if (!TryGetPlugin(type, out var plugin))
            {
                plugin = type.GetConstructor(_emptyTypes).Invoke(_emptyArgs) as Plugin;
                Plugins[type] = plugin;

                plugin.Context = Context;
                plugin.Enabled = true;
                plugin.Initialize();
            }
            return plugin;
        }

        public T GetPlugin<T>() where T : Plugin
        {
            return GetPlugin(typeof(T)) as T;
        }
        public Plugin GetPlugin(Type type)
        {
            ValidateType(type);    
            return Plugins[type];
        }

        public bool TryGetPlugin<T>(out T plugin) where T : Plugin
        {
            if (TryGetPlugin(typeof(T), out var res))
            {
                plugin = res as T;
                return true;
            }

            plugin = default(T);
            return false;
        }
        public bool TryGetPlugin(Type type, out Plugin plugin)
        {
            ValidateType(type);   
            return Plugins.TryGetValue(type, out plugin);
        }

        public bool Contains<T>() where T : Plugin
        {
            return Contains(typeof(T));
        }
        public bool Contains(Type type)
        {
            ValidateType(type);
            return Plugins.ContainsKey(type);
        }

        public Plugin[] ToArray()
        {
            var res = new Plugin[Plugins.Count];
            var i = 0;
            foreach (var plugin in Plugins.Values)
            {
                res[i++] = plugin;
            }
            return res;
        }

        internal void OnStart()
        {
            foreach (var plugin in Plugins.Values)
            {
                plugin.OnStart();
            }
        }
        internal void OnStop()
        {
            foreach (var plugin in Plugins.Values)
            {
                plugin.OnStop();
            }
        }
    }
}
