using System.Windows;

namespace ee.iLawyer.App.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var view = new Test.MessengerRegisterView();
            view.ShowDialog();
        }
    }
}
