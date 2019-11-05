using ee.Core.Framework.Messaging;
using System.Windows;

namespace ee.iLawyer.App.Wpf.Test
{
    /// <summary>
    /// Interaction logic for MessageRegister.xaml
    /// </summary>
    public partial class MessengerRegisterView : Window
    {
        public MessengerRegisterView()
        {
            InitializeComponent();
            //this.DataContext = new MessengerRegisterViewModel();
            //卸载当前(this)对象注册的所有MVVMLight消息
            this.Unloaded += (sender, e) => Messenger.Default.Unregister(this);
        }
    }
}
