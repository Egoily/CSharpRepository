using ee.iLawyer.Ops.Contact.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
namespace ee.iLawyer.App.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for AreaSelector.xaml
    /// </summary>
    public partial class AreaSelector : UserControl, INotifyPropertyChanged
    {

      
        public static readonly DependencyProperty SelectedProvinceProperty = DependencyProperty.Register("SelectedProvince", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SelectedCityProperty = DependencyProperty.Register("SelectedCity", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SelectedCountyProperty = DependencyProperty.Register("SelectedCounty", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SelectedProvinceCodeProperty = DependencyProperty.Register("SelectedProvinceCode", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SelectedCityCodeProperty = DependencyProperty.Register("SelectedCityCode", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));
        public static readonly DependencyProperty SelectedCountyCodeProperty = DependencyProperty.Register("SelectedCountyCode", typeof(string), typeof(AreaSelector), new FrameworkPropertyMetadata(string.Empty));




        public AreaSelector()
        {
            InitializeComponent();
            this.grid.DataContext = this;
            this.PropertyChanged += SelectedArea_PropertyChanged;
        }


        private void SelectedArea_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine(SelectedProvince + " " + SelectedCity + " " + SelectedCounty);
        }


        //private ObservableCollection<Area> _itemsSource = new ObservableCollection<Area>();

        //public ObservableCollection<Area> ItemsSource
        //{
        //    get
        //    {
        //        return _itemsSource;
        //    }
        //    set
        //    {
        //        _itemsSource.Clear();
        //        if (value != null)
        //        {
        //            foreach (var province in value)
        //            {
        //                _itemsSource.Add(province);
        //            }
        //        }
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged.Invoke(this, new PropertyChangedEventArgs("ItemsSource"));
        //        }
        //    }
        //}




        public ObservableCollection<Area> ItemsSource
        {
            get { return (ObservableCollection<Area>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<Area>), typeof(AreaSelector),
                new PropertyMetadata(new ObservableCollection<Area>(), OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
    
        }







        private string _selectedProvince;
        private string _selectedCity;
        private string _selectedCounty;

        private string _selectedProvinceCode;
        private string _selectedCityCode;
        private string _selectedCountyCode;

        public string SelectedProvince
        {
            get { return _selectedProvince; }
            set
            {
                _selectedProvince = value;
                _selectedCity = null;

                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedProvince"));
                }
            }
        }
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                _selectedCounty = null;

                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedCity"));
                }
            }
        }
        public string SelectedCounty
        {
            get { return _selectedCounty; }
            set
            {
                _selectedCounty = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedCounty"));
                }
            }
        }


        public string SelectedProvinceCode
        {
            get { return _selectedProvinceCode; }
            set
            {
                _selectedProvinceCode = value;
                _selectedCityCode = string.Empty;


                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedProvinceCode"));
                }
            }
        }
        public string SelectedCityCode
        {
            get { return _selectedCityCode; }
            set
            {
                _selectedCityCode = value;
                _selectedCountyCode = string.Empty;

                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedCityCode"));
                }
            }
        }
        public string SelectedCountyCode
        {
            get { return _selectedCountyCode; }
            set
            {
                _selectedCountyCode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SelectedCountyCode"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }



}
