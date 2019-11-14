
using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Designs;
using ee.iLawyer.App.Wpf.Test;
using ee.iLawyer.App.Wpf.ViewModels.Base;
using PropertyChanged;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class MessengerRegisterViewModel: CloseViewViewModel
    {
        public MessengerRegisterViewModel()
        {
            ///Messenger：信使
            ///Recipient：收件人
            Messenger.Default.Register<TestMessage>(this, "Message", ShowReceiveInfo);


            Messenger.Default.Register<string>(this, "CloseMainWindow", CloseMainWindow);
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



        private RelayCommand closeSenderWindow;

        public RelayCommand CloseSenderWindow
        {
            get
            {
                if (closeSenderWindow == null)
                {
                    closeSenderWindow = new RelayCommand(() => ExcuteCloseSenderWindow());
                }

                return closeSenderWindow;

            }
            set { closeSenderWindow = value; }
        }

        private void ExcuteCloseSenderWindow()
        {
            Messenger.Default.Send(
                  "", "CloseView");
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

        private void CloseMainWindow(string msg)
        {
            ExcuteCloseViewCommand();
        }
        #endregion
    }
}
