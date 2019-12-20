using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ee.iLawyer.App.Wpf.UserControls.Pickers
{
    /// <summary>
    /// PropertyPicker.xaml 的交互逻辑
    /// </summary>
    public partial class PropertyPicker : UserControl
    {

        public ObservableCollection<PropertySourceItem> ItemsSource
        {
            get { return (ObservableCollection<PropertySourceItem>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<PropertySourceItem>), typeof(PropertyPicker),
                new PropertyMetadata(new ObservableCollection<PropertySourceItem>(), OnPropertyChanged));

        public PropertyPickValue PickValue
        {
            get { return (PropertyPickValue)GetValue(PickValueProperty); }
            set { SetValue(PickValueProperty, value); }
        }
        /// <summary>
        /// 默认为双向绑定依赖属性
        /// </summary>
        public static readonly DependencyProperty PickValueProperty =
            DependencyProperty.Register("PickValue", typeof(PropertyPickValue), typeof(PropertyPicker),
                new FrameworkPropertyMetadata(new PropertyPickValue(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPropertyChanged));

        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
            (d as PropertyPicker).SetPickerNameAndIcon();
        }




        public PropertyPicker()
        {
            InitializeComponent();
            DataContext = this;
            this.LostFocus += CategoryTextBox_LostFocus;
            popup.CustomPopupPlacementCallback += new CustomPopupPlacementCallback(PopupRepositioning);
        }

        public void SetPickerNameAndIcon()
        {
            if (PickValue != null && PickValue.PickerId > 0)
            {
                var pickers = ItemsSource.SelectMany(x => x.Children).FirstOrDefault(x => x.Id == PickValue.PickerId);
                if (pickers != null)
                {
                    if (pickers.Name != PickValue.PickerName)
                    {
                        PickValue.PickerName = pickers.Name;
                    }
                    MaterialDesignThemes.Wpf.PackIconKind kind = MaterialDesignThemes.Wpf.PackIconKind.Record;
                    bool succ = Enum.TryParse(pickers.Icon?.ToString(), out kind);
                    icon.Kind = kind;
                }

            }
        }
        private CustomPopupPlacement[] PopupRepositioning(Size popupSize, Size targetSize, Point offset)
        {
            Point p = Mouse.GetPosition(this);
            var item = this.InputHitTest(p);
            if (item == null)
            {
                popup.IsOpen = false;
            }

            return new CustomPopupPlacement[] {
                new CustomPopupPlacement(new Point(0.01 - offset.X, grid.ActualHeight - offset.Y), PopupPrimaryAxis.Horizontal) };
        }
        private void CategoryTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!this.popup.IsKeyboardFocusWithin && !this.tvCategories.IsMouseOver)
            {
                this.popup.IsOpen = false;
            }
        }

        private void PathButton_Click(object sender, RoutedEventArgs e)
        {

            if (!popup.IsOpen)
            {
                this.popup.IsOpen = true;
                this.popup.UpdateWindow();
            }
            else
            {
                this.popup.IsOpen = false;
            }
        }


        private void tvCategories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (this.popup.IsKeyboardFocusWithin)
            {

                var selectNode = tvCategories.SelectedItem as PropertySourceItem;


                if (selectNode != null)
                {
                    if (selectNode.Children != null && selectNode.Children.Any())
                    {
                        this.popup.UpdateWindow();

                        return;
                    }
                    if (PickValue == null)
                    {
                        PickValue = new PropertyPickValue();
                    }
                    PickValue.PickerId = selectNode.Id;
                    PickValue.PickerName = selectNode.Name;
                    txtCategoryName.Text = PickValue.PickerName;
                    MaterialDesignThemes.Wpf.PackIconKind kind = MaterialDesignThemes.Wpf.PackIconKind.Record;
                    bool succ = Enum.TryParse(selectNode.Icon?.ToString(), out kind);
                    icon.Kind = kind;
                }

                this.popup.IsOpen = false;
            }
        }

        private void CategoryTextBox_Collapsed(object sender, RoutedEventArgs e)
        {
            this.popup.UpdateWindow();
        }

        private void CategoryTextBox_Expanded(object sender, RoutedEventArgs e)
        {
            this.popup.UpdateWindow();
        }


        private void popup_PreviewMouseDownHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.popup.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (PickValue == null)
            {
                PickValue = new PropertyPickValue() { PickerName = "请选择类型..." };
            }
        }



    }
}
