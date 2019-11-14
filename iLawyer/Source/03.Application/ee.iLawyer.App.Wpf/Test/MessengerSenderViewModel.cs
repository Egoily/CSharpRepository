using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Designs;
using ee.iLawyer.App.Wpf.Test;
using ee.iLawyer.App.Wpf.ViewModels.Base;
using PropertyChanged;
using System;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class MessengerSenderViewModel: CloseViewViewModel
    {
        private string sender;
        public MessengerSenderViewModel()
        {
            sender = Guid.NewGuid().ToString();
            Messenger.Default.Register<string>(this, "CloseView", Close);
        }

        private void Close(string msg)
        {
            ExcuteCloseViewCommand();
        }

        public void SetSender(string sender)
        {
            this.sender = sender;
        }

        #region 属性
        /// <summary>
        /// 发送消息
        /// </summary>
        public string SendInfo { get; set; }



        #endregion



        #region 命令

        private RelayCommand sendCommand;
        /// <summary>
        /// 发送命令
        /// </summary>
        public RelayCommand SendCommand
        {
            get
            {
                if (sendCommand == null)
                {
                    sendCommand = new RelayCommand(() => ExcuteSendCommand());
                }

                return sendCommand;

            }
            set { sendCommand = value; }
        }


   
        private void ExcuteSendCommand()
        {
            Messenger.Default.Send(
                new TestMessage
                {
                    DateTime = DateTime.Now,
                    Message = SendInfo,
                    Sender = sender
                }, "Message");
        }

      

        private RelayCommand closeMainWindowCommand;
        /// <summary>
        /// 关闭主窗口
        /// </summary>
        public RelayCommand CloseMainWindowCommand
        {
            get
            {
                
                if (closeMainWindowCommand == null)
                {
                    closeMainWindowCommand = new RelayCommand(() => ExcuteCloseMainWindowCommand());
                }

                return closeMainWindowCommand;

            }
            set { closeMainWindowCommand = value; }
        }

        private void ExcuteCloseMainWindowCommand()
        {
            Messenger.Default.Send(
                "", "CloseMainWindow");
        }



        #endregion
    }
}
