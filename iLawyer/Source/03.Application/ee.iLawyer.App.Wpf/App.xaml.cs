using ee.iLawyer.App.Wpf.Models;
using ee.iLawyer.App.Wpf.Views;
using System;
using System.Linq;
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

            Core.Framework.Messaging.Messenger.Default.Register<ShowViewArg>(this, "ShowView", ShowView);



            //var view = new MainWindow();
            var view = new LoginView();
            //var view = new Test.MessengerRegisterView();

            view.ShowDialog();

        }

        private void ShowView(ShowViewArg arg)
        {

            var window = CreateInstance<Window>(arg);
            if (arg.Topmost)
            {
                window?.ShowDialog();
            }
            else
            {
                window?.Show();
            }
        }

        private T CreateInstance<T>(ShowViewArg arg)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();

            Type type = null;
            ass.GetTypes().ToList().ForEach(i =>
            {
                if (i.IsClass && i.IsPublic && i.Name == arg.ShownerName)
                {
                    type = i;
                }
            });
            return (T)Activator.CreateInstance(type, arg.Parameter == null ? null : new object[] { arg.Parameter });
        }
    }
}
