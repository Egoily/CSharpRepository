using ee.iLawyer.App.Wpf.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageProject.xaml
    /// </summary>
    public partial class ManageProject : UserControl
    {
        public ManageProject()
        {
            InitializeComponent();
        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            (DataContext as ProjectViewModel).DeleteItem(sender, eventArgs);
        }
    }


}
