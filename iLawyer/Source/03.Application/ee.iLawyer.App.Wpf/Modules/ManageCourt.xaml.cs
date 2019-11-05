using ee.iLawyer.App.Wpf.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageCourt.xaml
    /// </summary>
    public partial class ManageCourt : UserControl
    {
        public ManageCourt()
        {
            InitializeComponent();
        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            (DataContext as CourtViewModel).DeleteItem(sender, eventArgs);
        }
    }
}
