using System;

namespace ee.Utilities
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class EnumDescriptionAttribute : Attribute
    {
        private string description;

        public string Description
        {
            get { return this.description; }
        }

        public EnumDescriptionAttribute(string description)
            : base()
        {
            this.description = description;
        }
    }

    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            string description = value.ToString();
            System.Reflection.FieldInfo fieldinfo = value.GetType().GetField(description);
            EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldinfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }
            return description;
        }
    }
}