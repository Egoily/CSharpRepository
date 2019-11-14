using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Framework.Schema;
using ee.Core.Wpf.Designs;
using ee.iLawyer.App.Wpf.ViewModels.Base;
using ee.iLawyer.Ops.Contact.Args.SystemManagement;
using ee.iLawyer.Ops.Contact.DTO.SystemManagement;
using ee.iLawyer.ServiceProvider;
using PropertyChanged;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class LoginViewModel : CloseViewViewModel
    {

        private ILawyerServiceProvider serviceProvider;
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
        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool IsRememberPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool SignInEnabled { get; set; } = true;
        /// <summary>
        /// 
        /// </summary>
        public bool? Success { get; set; }
        #endregion

        #region *命令(Binding Command)

        public virtual ICommand SignInCommand => new RelayCommand(() => LoginAsyn());
        public virtual ICommand ExitCommand => new RelayCommand(() => Exit());



        public LoginViewModel()
        {
            serviceProvider = new ILawyerServiceProvider();
        }



        #endregion

        #region Login/Exit

        /// <summary>
        /// 登陆系统
        /// </summary>
        public async void LoginAsyn()
        {
            try
            {


                if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
                {
                    this.Report = "请输入用户名密码";
                    Success = false;
                    return;
                }
                SignInEnabled = false;
                this.Report = "正在验证登录 . . .";

                var response = new BaseObjectResponse<User>();
                var LoginTask = Task.Run(() =>
                {
                    var request = new LoginRequest()
                    {
                        LoginName = UserName,
                        Password = Password,
                    };
                    response = serviceProvider.Login(request);
                });
                ;
                var timeouttask = Task.Delay(3000);
                var completedTask = await Task.WhenAny(LoginTask, timeouttask);
                if (completedTask == timeouttask)
                {
                    this.Report = "系统连接超时,请联系管理员!";
                }
                else
                {
                    //TODO:

                    if (response.IsOk())
                    {
                        //TODO: 
                        this.Report = "加载用户信息 . . .";
                        Cacher.Loginer = response.Object;
                        Success = true;
                        Messenger.Default.Send("MainWindow", "ShowView");
                        ExcuteCloseViewCommand();
                    }
                    else
                    {
                        Report = response.Message;
                    }

                }

            }
            catch (System.Exception ex)
            {
                this.Report = ex.Message;
            }
            finally
            {
                SignInEnabled = true;
            }

        }

        /// <summary>
        /// 关闭系统
        /// </summary>
        public void Exit()
        {
            Success = false;
            ExcuteCloseViewCommand();
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
