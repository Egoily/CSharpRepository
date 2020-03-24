using Autofac;
using ee.Core.ComponentModel;
using ee.Core.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ee.Core.Framework.ServiceLocation
{
    public class DependencyLocator : ServiceLocatorImplBase, IDependencyLocator
    {
        private IContainer container;
        private ContainerBuilder containerBuilder;

        private static readonly object _instanceLock = new object();

        private static DependencyLocator _default;

        /// <summary>
        /// This class' default instance.
        /// </summary>
        public static DependencyLocator Default
        {
            get
            {
                if (_default == null)
                {
                    lock (_instanceLock)
                    {
                        if (_default == null)
                        {
                            _default = new DependencyLocator();
                        }
                    }
                }

                return _default;
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (container == null)
            {
                throw new System.Exception("container is null.");
            }
            if (string.IsNullOrEmpty(key))
            {
                return container.Resolve(serviceType);
            }
            else
            {
                return container.ResolveNamed(key, serviceType);
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (container == null)
            {
                throw new System.Exception("container is null.");
            }
            if (container.ComponentRegistry?.Registrations?.Any() ?? false)
            {
                return container.ComponentRegistry?.Registrations.Select(x =>
                container.ResolveComponent(new ResolveRequest(x.Services.FirstOrDefault(), x, null)));
            }
            else
            {
                return null;
            }
        }






        //public TInterface Get<TInterface>(string typeName)
        //{
        //    if (container == null)
        //    {
        //        throw new System.Exception("container is null.");
        //    }
        //    return container.ResolveNamed<TInterface>(typeName);
        //}

        //public TInterface Get<TInterface>()
        //{
        //    if (container == null)
        //    {
        //        throw new System.Exception("container is null.");
        //    }
        //    return container.Resolve<TInterface>();
        //}

        public void Register()
        {
            RegisterByGetEntryAssembly();
            Build();
        }

        public ContainerBuilder RegisterByAssembly(Assembly asm)
        {
            if (containerBuilder == null)
            {
                containerBuilder = new ContainerBuilder();
            }
            var types = asm.GetTypes();
            foreach (var t in types)
            {
                var attr = (IocAttribute)t.GetCustomAttribute(typeof(IocAttribute), false);
                if (attr != null && attr.Allow)
                {
                    var interfaceDefault = (attr.ServiceType == null && attr.UserFirstInterfaceIfNull) ? t.GetInterfaces().FirstOrDefault() : attr.ServiceType;
                    if (interfaceDefault != null)
                    {
                        containerBuilder.RegisterType(t).Named(t.Name, interfaceDefault);
                    }
                    else
                    {
                        containerBuilder.RegisterType(t);
                    }
                }
            }
            return containerBuilder;
        }


        public void Build()
        {
            if (containerBuilder == null)
            {
                throw new System.Exception("ContainerBuilder is null.");
            }
            container = containerBuilder.Build();
        }

        public ContainerBuilder RegisterByExecutingAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return RegisterByAssembly(asm);
        }
        public ContainerBuilder RegisterByCallingAssembly()
        {
            Assembly asm = Assembly.GetCallingAssembly();
            return RegisterByAssembly(asm);
        }

        public ContainerBuilder RegisterByGetEntryAssembly()
        {
            Assembly asm = Assembly.GetEntryAssembly();
            return RegisterByAssembly(asm);
        }




        public bool ContainsCreated<TClass>()
        {
            return ContainsCreated<TClass>(null);
        }

        public bool ContainsCreated<TClass>(string key)
        {
            if (container == null || container.ComponentRegistry == null || container.ComponentRegistry.Registrations == null || !container.ComponentRegistry.Registrations.Any())
            {
                return false;
            }
            var classType = typeof(TClass);



            if (string.IsNullOrEmpty(key))
            {
                return container.ComponentRegistry.Registrations.SelectMany(x => x.Services).Select(x => ((Autofac.Core.KeyedService)x).ServiceType).Contains(classType);
            }

            return container.ComponentRegistry.Registrations.SelectMany(x => x.Services).Select(x => ((Autofac.Core.KeyedService)x).ServiceKey.ToString()).Contains(key);
        }

        public bool IsRegistered<T>()
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface, TClass>()
            where TInterface : class
            where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Register<TInterface, TClass>(bool createInstanceImmediately)
            where TInterface : class
            where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Register<TClass>() where TClass : class
        {
            if (containerBuilder == null)
            {
                containerBuilder = new ContainerBuilder();
            }
            var type = typeof(TClass);
            containerBuilder.RegisterType(type);

        }

        public void Register<TClass>(bool createInstanceImmediately) where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Register<TClass>(Func<TClass> factory) where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately) where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Register<TClass>(Func<TClass> factory, string key) where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Register<TClass>(Func<TClass> factory, string key, bool createInstanceImmediately) where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void Unregister<TClass>() where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Unregister<TClass>(TClass instance) where TClass : class
        {
            throw new NotImplementedException();
        }

        public void Unregister<TClass>(string key) where TClass : class
        {
            throw new NotImplementedException();
        }
    }
}
