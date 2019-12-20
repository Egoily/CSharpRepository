using ee.Core.Wpf.Enums;
using ee.Core.Wpf.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.UserControls
{
    /// <summary>
    /// Interaction logic for FuncToolBar.xaml
    /// </summary>
    public partial class FuncToolBar : UserControl
    {
        public FuncToolBar()
        {
            InitializeComponent();
        }

        public ObservableCollection<FuncButtonViewModel> ItemsSource
        {
            get { return (ObservableCollection<FuncButtonViewModel>)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<FuncButtonViewModel>), typeof(FuncToolBar),
                new PropertyMetadata(new ObservableCollection<FuncButtonViewModel>(), null));


        public ActionMode Mode
        {
            get { return (ActionMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register("Mode", typeof(ActionMode), typeof(FuncToolBar),
                new PropertyMetadata(ActionMode.None, OnPropertyChanged));


        private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as FuncToolBar).VisibleControls((ActionMode)e.NewValue);
        }

        public void VisibleControls(ActionMode model)
        {
            if (ItemsSource != null)
            {
                ItemsSource.ForEach(x => x.IsHide =(!x.IsVisibility)|| (x.InModel & model) == 0);
            }
        }

    }
}
