using System;

namespace ee.Core.ComponentModel
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FieldDescriptionAttribute : Attribute
    {
        protected string caption = string.Empty;
        protected string remark = string.Empty;

        public string Caption { get { return caption; } }
        public string Remark { get { return remark; } }

        public FieldDescriptionAttribute(string caption, string remark = "")
        {
            this.caption = caption;
            this.remark = remark;
        }

    }
}
