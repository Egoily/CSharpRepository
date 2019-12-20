using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Extensions;
using ee.iLawyer.App.Wpf.UserControls;
using ee.iLawyer.App.Wpf.UserControls.Pickers;
using ee.iLawyer.App.Wpf.ViewModels;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageClient.xaml
    /// </summary>
    [BizModule(3, "Root", "客户管理", "module.client", "", typeof(ClientViewModel))]
    public partial class ManageClient : UserControl
    {
        public ManageClient()
        {
            InitializeComponent();

        }

        private void ClientTypeControl_TypeChanged(object sender, UserControls.TypeRoutedEventArge e)
        {
            txtName.Text = e.IsNaturalPerson ? "姓名" : "机构名称";

        }


    }
    [ValueConversion(typeof(bool), typeof(string))]
    public class IsNPToType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return "自然人";
            }
            else
            {
                return "机构";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "自然人")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    [ValueConversion(typeof(bool), typeof(string))]
    public class IsNPToPackIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
            {
                return "Accessibility";
            }
            else
            {
                return "OfficeBuilding";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "Accessibility")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    [ValueConversion(typeof(ObservableCollection<Picker>), typeof(ObservableCollection<PropertySourceItem>))]
    public class PropertySourceItemConverter : IValueConverter
    {
        #region IValueConverter Members

        private PropertySourceItem Convert(Picker source)
        {
            var dst = new PropertySourceItem()
            {
                Id = source.Id,
                Code = source.Code,
                Name = source.Name,
                Icon = source.Value,
                ParentId = source.ParentId,
            };
            if (source.Children != null)
            {
                dst.Children = source.Children.Select(Convert).ToList();
            }
            return dst;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pickers = value as ObservableCollection<Picker>;
            return new ObservableCollection<PropertySourceItem>(pickers.Select(Convert));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        #endregion
    }


    [ValueConversion(typeof(ObservableCollection<ClientProperty>), typeof(ObservableCollection<PropertyPickValue>))]
    public class ClientPropertyConverter : IValueConverter
    {
        #region IValueConverter Members

        private PropertyPickValue Convert(ClientProperty source)
        {
            var dst = new PropertyPickValue()
            {
                Id = source.Id,
                PickerId = source.PickerId,
                PickerName = source.PickerName,
                Value = source.Value,
                Value2 = null,
            };

            return dst;
        }
        private ClientProperty Convert(PropertyPickValue source)
        {
            var dst = new ClientProperty()
            {
                Id = source.Id,
                PickerId = source.PickerId,
                PickerName = source.PickerName,
                Value = source.Value,
                OrderNo = 0,
            };

            return dst;
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pickers = value as ObservableCollection<ClientProperty>;
            return new ObservableCollection<PropertyPickValue>(pickers.Select(Convert));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pickers = value as ObservableCollection<PropertyPickValue>;
            return new ObservableCollection<ClientProperty>(pickers.Select(Convert));
        }

        #endregion
    }










}
