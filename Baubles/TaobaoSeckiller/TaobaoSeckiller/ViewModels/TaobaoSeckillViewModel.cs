using ee.Core.Wpf.Designs;
using ee.Core.Wpf.ViewModels;
using OpenQA.Selenium;
using System;

namespace TaobaoSeckiller.ViewModels
{
    public class TaobaoSeckillViewModel : ViewModelBase, IDisposable
    {
        private IWebDriver _driver;

        private string _reportText;
        private int _timeoutSeconds = 10 * 60;
        private DateTime? _triggerTime;
        private string _url = @"https://cart.taobao.com/cart.htm";

        public RelayCommand<object> LoginCommand => new RelayCommand<object>(ExecuteLoginCommand);

        public RelayCommand<object> LogoutCommand => new RelayCommand<object>(ExecuteLogoutCommand);

        public string ReportText
        {
            get => _reportText;
            set { _reportText = value; RaisePropertyChanged(); }
        }

        public RelayCommand<object> SettlementCommand => new RelayCommand<object>(ExecuteSettlementCommand);

        public int TimeoutSeconds
        {
            get => _timeoutSeconds;
            set { _timeoutSeconds = value; RaisePropertyChanged(); }
        }

        public DateTime? TriggerTime
        {
            get => _triggerTime;
            set { _triggerTime = value; RaisePropertyChanged(); }
        }

        public string Url
        {
            get => _url;
            set { _url = value; RaisePropertyChanged(); }
        }

        public TaobaoSeckillViewModel()
        {
        }
        private void ClearCart(IWebDriver driver, DateTime? time, int timeout)
        {
            ReportText = "";
            while (true)
            {
                var now = DateTime.Now;
                if (!time.HasValue || (time.HasValue && now > time.Value))
                {
                    try
                    {
                        if (driver.Url != Url)
                        {
                            ReportText = "还未打开淘宝购物车页面,请先登录帐号!";
                            return;
                        }
                        SelectAll(driver);
                        var suc = Settlement(driver, timeout);
                        if (suc)
                        {
                            ReportText = "抢购成功!";
                        }
                        else
                        {
                            ReportText = "抢购失败!";
                        }
                    }
                    catch (NullCartException ex)
                    {
                        ReportText = "购物车为空!";
                        return;
                    }
                    catch (TimeoutException ex)
                    {
                        ReportText = "超时了!";
                        return;
                    }
                    catch (Exception ex)
                    {
                        ReportText = $"程序错误:{ex.Message}";
                        return;
                    }

                    return;
                }
                else
                {
                    System.Threading.Thread.Sleep(1);
                }
            }
        }

        private void InitDriver()
        {
            if (_driver == null)
            {
                _driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            }
        }

        private void LoginCart(IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(3000);
        }

        private void SelectAll(IWebDriver driver)
        {
            try
            {
                var selectAll = driver.FindElement(By.Id("J_SelectAll1"));
                if (selectAll != null)
                {
                    selectAll.Click();
                }
            }
            catch (Exception)
            {
                throw new NullCartException();
            }
        }

        private bool Settlement(IWebDriver driver, int timeout)
        {
            var opTime = DateTime.Now;
            while (true)
            {
                try
                {
                    #region 疯狂点击<结算>按钮

                    var go = driver.FindElement(By.Id("J_Go"));
                    go?.Click();

                    #endregion

                }

                catch (Exception)
                {
                    var suc = SubmitOrder(driver);
                    if (suc)
                    {
                        return true;
                    }
                }
                if (DateTime.Now > opTime.AddSeconds(timeout))
                {
                    throw new TimeoutException();
                }
            }
        }

        private bool SubmitOrder(IWebDriver driver)
        {
            try
            {
                // 界面跳转后，点击<提交订单>按钮
                var submit = driver.FindElement(By.LinkText("提交订单"));
                if (submit != null)
                {
                    submit.Click();
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public void Dispose()
        {
            try
            {
                _driver?.Close();
                _driver?.Dispose();
            }
            catch (Exception)
            {
            }
        }

        public virtual void ExecuteLoginCommand(object o)
        {
            InitDriver();
            LoginCart(_driver, Url);
        }

        public virtual void ExecuteLogoutCommand(object o)
        {
            Dispose();
        }

        public virtual void ExecuteSettlementCommand(object o)
        {
            InitDriver();
            ClearCart(_driver, TriggerTime, TimeoutSeconds);
        }
    }
}