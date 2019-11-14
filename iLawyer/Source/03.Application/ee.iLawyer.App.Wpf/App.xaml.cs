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

            Core.Framework.Messaging.Messenger.Default.Register<string>(this, "ShowView", ShowView);



            //var view = new MainWindow();
            var view = new LoginView();
            //var view = new Test.MessengerRegisterView();

            view.ShowDialog();

        }

        private void ShowView(string windowName)
        {

            var window = CreateInstance<Window>(windowName);

            window?.ShowDialog();
        }

        private T CreateInstance<T>(string typeName)
        {
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();

            Type type = null;
            ass.GetTypes().ToList().ForEach(i =>
            {
                if (i.IsClass && i.IsPublic && i.Name == typeName)
                {
                    type = i;
                }
            });
            return (T)Activator.CreateInstance(type);
        }
    }
}
