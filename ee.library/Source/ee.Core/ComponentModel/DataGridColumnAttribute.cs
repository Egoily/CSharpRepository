using System;
using System.Windows.Data;

namespace ee.Core.ComponentModel
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum | AttributeTargets.Parameter, AllowMultiple = false)]

    public class DataGridColumnAttribute : Attribute
    {
        public string Header { get; set; }
        public int DisplayIndex { get; set; }
        public IValueConverter Converter { get; set; }

        public DataGridColumnAttribute(string header, int index)
        {
            Header = header;
            DisplayIndex = index;
        }
        public DataGridColumnAttribute(string header,int index, IValueConverter converter)
        {
            Header = header;
            DisplayIndex = index;
            Converter = converter;
        }
    }
}
