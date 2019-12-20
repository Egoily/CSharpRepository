using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ee.Core.ComponentModel
{
    public class BizModuleManager
    {
        private static readonly object CreationLock = new object();
        private static BizModuleManager _defaultInstance;

        public static BizModuleManager Default
        {
            get
            {
                if (_defaultInstance == null)
                {
                    lock (CreationLock)
                    {
                        if (_defaultInstance == null)
                        {
                            _defaultInstance = new BizModuleManager();
                        }
                    }
                }

                return _defaultInstance;
            }
        }

        /// <summary>
        /// Provides a way to override the BizModuleManager.Default instance with
        /// a custom instance, for example for unit testing purposes.
        /// </summary>
        /// <param name="newInstance">The instance that will be used as BizModuleManager.Default.</param>
        public static void OverrideDefault(BizModuleManager newInstance)
        {
            _defaultInstance = newInstance;
        }
        /// <summary>
        /// Sets the BizModuleManager's default (static) instance to null.
        /// </summary>
        public static void Reset()
        {
            _defaultInstance = null;
        }

        public IList<BizModule> GetAssemblyBizModules(Assembly asm)
        {
            IList<BizModule> list = new List<BizModule>();

            var types = asm.GetTypes();
            foreach (var t in types)
            {
                var attr = (BizModuleAttribute)t.GetCustomAttribute(typeof(BizModuleAttribute), false);
                if (attr != null)
                {
                    var content = Activator.CreateInstance(t);
                    if (attr.DataContextType != null)
                    {
                        content.GetType().GetProperty("DataContext")?.SetValue(content, Activator.CreateInstance(attr.DataContextType));
                    }

                    list.Add(new BizModule(attr.Index, attr.ScopeName, attr.Name, attr.MetadataToken, attr.Icon, content));
                }
            }
            return list?.OrderBy(x => x.Index)?.ToList();
        }
        public IList<BizModule> GetCallingAssemblyBizModules(string[] filter = null)
        {
            var allModules = GetAssemblyBizModules(Assembly.GetCallingAssembly());
            if (filter != null)
            {
                return allModules?.Where(x => filter.Contains(x.MetadataToken))?.ToList();
            }
            else
            {
                return allModules;
            }
        }

        public IList<BizModule> GetEntryAssemblyBizModules(string[] filter = null)
        {
            var allModules = GetAssemblyBizModules(Assembly.GetEntryAssembly());
            if (filter != null)
            {
                return allModules?.Where(x => filter.Contains(x.MetadataToken))?.ToList();
            }
            else
            {
                return allModules;
            }
        }

        public virtual async Task<IList<BizModule>> GetCallinAssemblyModulesAsync(string[] filter = null)
        {
            try
            {
                IList<BizModule> list = new List<BizModule>();

                await Task.Run(() =>
                {
                    list = GetCallingAssemblyBizModules(filter);
                });
                return list;
            }
            catch
            {
                return null;
            }
        }
        public virtual async Task<IList<BizModule>> GetEntryAssemblyModulesAsync(string[] filter = null)
        {
            try
            {
                IList<BizModule> list = new List<BizModule>();

                await Task.Run(() =>
                {
                    list = GetEntryAssemblyBizModules(filter);
                });
                return list;
            }
            catch
            {
                return null;
            }
        }



    }
}
