using ee.Core.ComponentModel;
using ee.Core.Wpf.Designs;
using PropertyChanged;
using System.Windows;
using System.Windows.Input;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class LoginViewModel: IViewModel
    {
        #region * Properties

        /// <summary>
        /// 数据库访问类型
        /// </summary>
        public string ServerType { get; set; }
        /// <summary>
        /// 皮肤样式
        /// </summary>
        public string SkinName { get; set; }
        /// <summary>
        /// 进度报告
        /// </summary>
        public string Report { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        public bool IsRememberMe { get; set; }

        public bool SignInEnabled { get; set; } = true;


        #endregion

        #region *命令(Binding Command)

        public virtual ICommand SignInCommand => new RelayCommand(() => LoginAsyn());
        public virtual ICommand ExitCommand => new RelayCommand(() => ApplicationShutdown());




        public LoginViewModel()
        {

        }



        #endregion

        #region Login/Exit

        /// <summary>
        /// 登陆系统
        /// </summary>
        public async void LoginAsyn()
        {

            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                this.Report = "请输入用户名密码";
                return;
            }
            //TODO:

            var view = new Views.MainWindow();
            view.ShowDialog();
        }

        /// <summary>
        /// 关闭系统
        /// </summary>
        public void ApplicationShutdown()
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region 记住密码

        /// <summary>
        /// 读取本地配置信息
        /// </summary>
        public void ReadConfigInfo()
        {
            //TODO:
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        private void SaveLoginInfo()
        {
            //TODO:
        }

        #endregion
    }
}
