using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.App.Wpf.UserControls.Pickers
{
    /// <summary>
    /// Interaction logic for PropertyPickerGroup.xaml
    /// </summary>
    public partial class PropertyPickerGroup : UserControl
    {

        public ObservableCollection<PropertySourceItem> ItemsSource
        {
            get { return (ObservableCollection<PropertySourceItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<PropertySourceItem>), typeof(PropertyPickerGroup), new PropertyMetadata(new ObservableCollection<PropertySourceItem>()));

        public ObservableCollection<PropertyPickValue> PickValues
        {
            get { return (ObservableCollection<PropertyPickValue>)GetValue(PickValuesProperty); }
            set { SetValue(PickValuesProperty, value); }
        }
        /// <summary>
        /// 默认为双向绑定依赖属性
        /// </summary>
        public static readonly DependencyProperty PickValuesProperty =
            DependencyProperty.Register("PickValues", typeof(ObservableCollection<PropertyPickValue>), typeof(PropertyPickerGroup),
                new FrameworkPropertyMetadata(new ObservableCollection<PropertyPickValue>(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //d.SetValue(e.Property, e.NewValue);       
        }




        public PropertyPickerGroup()
        {

            InitializeComponent();
            this.DataContext = this;

        }



        private void AddItem()
        {
            PickValues.Add(new PropertyPickValue() { PickerId = 0, PickerName = "请选择类型...", Value = string.Empty });
        }

        private void RemoveItem(PropertyPickValue item)
        {
            PickValues.Remove(item);
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as Button).DataContext as PropertyPickValue;
            RemoveItem(item);
        }

    }

    public enum PickerType
    {
        /// <summary>
        /// 
        /// </summary>
        Default = 0,
        /// <summary>
        /// 
        /// </summary>
        Text = 1,
        /// <summary>
        /// 
        /// </summary>
        DateTime = 2,
    }



    public class PickerTypeToVisible : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PickerType)value == PickerType.DateTime)
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class PickerTypeToHidden : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PickerType)value == PickerType.DateTime)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class StringToDateTime : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime.TryParse(value?.ToString(), out DateTime datetime);
            return datetime;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var datetime = System.Convert.ToDateTime(value);
            if (datetime == DateTime.MinValue)
            {
                return string.Empty;
            }
            else
            {
                return datetime.ToString("yyyy-dd-MM HH:mm");
            }

        }
    }


}
