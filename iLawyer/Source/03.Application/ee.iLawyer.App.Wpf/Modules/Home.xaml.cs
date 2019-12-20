using ee.Core.ComponentModel;
using ee.iLawyer.App.Wpf.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    [BizModule(0, "Root", "首页", "module.home", "", typeof(HomeViewModel))]
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();

        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true))
            {
                return;
            }

            System.Diagnostics.Debug.Write("Do Delete.");

        }



        private void btnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        
        }

      
    }
}
