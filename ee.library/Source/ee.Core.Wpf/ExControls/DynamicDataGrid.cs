using ee.Core.ComponentModel;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.Core.Wpf.ExControls
{
    public class DynamicDataGrid : DataGrid
    {
        static DynamicDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicDataGrid), new FrameworkPropertyMetadata(typeof(DataGrid)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GenerateColumns();
        }
        public void GenerateColumns()
        {
            Columns.Clear();
            var types = ItemsSource.GetType().GenericTypeArguments;
            foreach (var type in types)
            {
             
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    var attr = (DataGridColumnAttribute)pi.GetCustomAttributes(typeof(DataGridColumnAttribute), true).FirstOrDefault();
                    if (attr != null)
                    {
                        Columns.Add(new DataGridTextColumn()
                        {
                            Header = attr.Header,
                            Width= DataGridLength.Auto,
                            DisplayIndex = attr.DisplayIndex,
                            Binding = new Binding()
                            {
                                Path = new PropertyPath(pi.Name),
                                Mode = BindingMode.TwoWay,
                                UpdateSourceTrigger = UpdateSourceTrigger.Explicit,
                                //Converter=
                            }
                        });
                    }
                }
                  


            }

        }

    }


}
