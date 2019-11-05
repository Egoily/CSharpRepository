
using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Designs;
using ee.iLawyer.App.Wpf.Test;
using PropertyChanged;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class MessengerRegisterViewModel
    {
        public MessengerRegisterViewModel()
        {
            ///Messenger：信使
            ///Recipient：收件人
            Messenger.Default.Register<TestMessage>(this, "Message", ShowReceiveInfo);
        }


        #region 属性
        /// <summary>
        /// 接收到信使传递过来的值
        /// </summary>
        public string ReceiveInfo { get; set; }


        #endregion


        #region 启动新窗口

        private RelayCommand showSenderWindow;

        public RelayCommand ShowSenderWindow
        {
            get
            {
                if (showSenderWindow == null)
                {
                    showSenderWindow = new RelayCommand(() => ExcuteShowSenderWindow());
                }

                return showSenderWindow;

            }
            set { showSenderWindow = value; }
        }

        private void ExcuteShowSenderWindow()
        {
            MessengerSenderView sender = new MessengerSenderView();
            
            sender.Show();
        }

        #endregion 




        #region 辅助函数
        /// <summary>
        /// 显示收件的信息
        /// </summary>
        /// <param name="msg"></param>
        private void ShowReceiveInfo(TestMessage msg)
        {
            ReceiveInfo += $"{msg.Sender} [{msg.DateTime}]:{msg.Message}" + "\n";
        }
        #endregion
    }
}
