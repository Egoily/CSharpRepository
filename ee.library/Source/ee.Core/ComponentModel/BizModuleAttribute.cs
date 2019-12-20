using System;

namespace ee.Core.ComponentModel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class BizModuleAttribute : Attribute
    {
        // Summary:
        //     Gets a string representing the name of the module.
        //
        // Returns:
        //     The module name.
        public virtual string ScopeName { get; private set; }
        // Summary:
        //     Gets a String representing the name of the module with the path removed.
        //
        // Returns:
        //     The module name with no path.
        public virtual string Name { get; private set; }

        // Summary:
        //     Gets a token that identifies the module in metadata.
        //
        // Returns:
        //     A token that identifies the current module in metadata.
        public virtual string MetadataToken { get; private set; }

        // Summary:
        //     Gets a string representing the fully qualified name and path to this module.
        //
        // Returns:
        //     The fully qualified module name.

        public virtual string FullyQualifiedName { get; private set; }

        public virtual string Icon { get; private set; }

        public virtual Type DataContextType { get; private set; }

        public virtual int Index { get; private set; }

        public BizModuleAttribute(int index, string scopeName, string name, string metadataToken, string icon = "", Type dataContextType = null)
        {
            Index = index;
            ScopeName = scopeName;
            Name = name;
            MetadataToken = metadataToken;
            Icon = icon;
            DataContextType = dataContextType;
        }



    }
}
