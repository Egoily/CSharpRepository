using ee.iLawyer.App.Wpf.ViewModels;
using System.Windows;

namespace ee.iLawyer.App.Wpf.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();

   
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DialogResult = (DataContext as LoginViewModel).Success;
        }

   
    }
}
