using ee.Core.ComponentModel;
using System;
using System.Reflection;

namespace ee.Core.Utils
{
    public static class EnumUtils
    {
        public static T GetEnumAttribute<T>(Enum source) where T : Attribute
        {
            Type type = source.GetType();
            var sourceName = Enum.GetName(type, source);
            FieldInfo field = type.GetField(sourceName);
            object[] attributes = field.GetCustomAttributes(typeof(T), false);
            foreach (var o in attributes)
            {
                if (o is T)
                {
                    return (T)o;
                }
            }
            return null;
        }

        public static string GetDescription(Enum source)
        {
            var attribute = GetEnumAttribute<System.ComponentModel.DescriptionAttribute>(source);
            if (attribute == null)
            {
                var fieldattribute = GetEnumAttribute<FieldDescriptionAttribute>(source);
                return fieldattribute?.Caption;
            }
            return attribute.Description;
        }

    }
}
