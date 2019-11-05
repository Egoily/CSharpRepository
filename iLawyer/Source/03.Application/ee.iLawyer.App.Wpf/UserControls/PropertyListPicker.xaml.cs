using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for PropertyListPicker.xaml
    /// </summary>
    public partial class PropertyListPicker : UserControl
    {
        public ObservableCollection<PropertyListItem> DataSource
        {
            get { return (ObservableCollection<PropertyListItem>)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(ObservableCollection<PropertyListItem>), typeof(PropertyListPicker), new PropertyMetadata(OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }


        public PropertyListPicker()
        {
            InitializeComponent();
            this.DataContext = this;


        }

    }
}
