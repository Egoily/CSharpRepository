using System;

namespace ee.Core.ComponentModel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IocAttribute : Attribute
    {
        public Type ServiceType { get; private set; }
        public bool UserFirstInterfaceIfNull { get; private set; }
        public bool Allow { get; private set; }

        public IocAttribute(Type serviceType = null,bool userFirstInterfaceIfNull=true, bool allow = true)
        {
            ServiceType = serviceType;
            UserFirstInterfaceIfNull = userFirstInterfaceIfNull;
            Allow = allow;
        }



    }
}
