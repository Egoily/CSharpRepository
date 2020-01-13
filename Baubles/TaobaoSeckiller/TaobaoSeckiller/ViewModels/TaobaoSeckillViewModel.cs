using ee.Core.Wpf.Designs;
using ee.Core.Wpf.ViewModels;
using OpenQA.Selenium;
using System;

namespace TaobaoSeckiller.ViewModels
{
    public class TaobaoSeckillViewModel : ViewModelBase, IDisposable
    {

        private IWebDriver _driver;

        public RelayCommand<object> LoginCommand => new RelayCommand<object>(ExecuteLoginCommand);
        public RelayCommand<object> LogoutCommand => new RelayCommand<object>(ExecuteLogoutCommand);
        public RelayCommand<object> SettlementCommand => new RelayCommand<object>(ExecuteSettlementCommand);

        private DateTime? _triggerTime;
        public DateTime? TriggerTime
        {
            get => _triggerTime;
            set { _triggerTime = value; RaisePropertyChanged(); }
        }

        public TaobaoSeckillViewModel()
        {

        }

        public virtual void ExecuteLoginCommand(object o)
        {
            if (_driver == null)
            {
                _driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            }
            LoginCart(_driver);
        }

        public virtual void ExecuteLogoutCommand(object o)
        {
            Dispose();
        }
        public virtual void ExecuteSettlementCommand(object o)
        {
            if (_driver == null)
            {
                _driver = new OpenQA.Selenium.Firefox.FirefoxDriver();
            }
            ClearCart(_driver, _triggerTime);
        }


        private void LoginCart(IWebDriver driver, string url = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = @"https://cart.taobao.com/cart.htm";
            }
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(3000);
        }
        private void ClearCart(IWebDriver driver, DateTime? time, int timeout = 10 * 60)
        {

            while (true)
            {
                var now = DateTime.Now;
                if (!time.HasValue || (time.HasValue && now > time.Value))
                {
                    try
                    {
                        SelectAll(driver);
                        Settlement(driver, timeout);
                    }
                    catch (NullCartException)
                    {
                        return;
                    }

                }
                else
                {
                    System.Threading.Thread.Sleep(1);
                }
            }


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
        private bool Settlement(IWebDriver driver, int timeout = 10 * 60)
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
                    if (DateTime.Now > opTime.AddMinutes(timeout))
                    {
                        throw new TimeoutException();
                    }
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
                        if (DateTime.Now > opTime.AddMinutes(timeout))
                        {
                            throw new TimeoutException();
                        }
                    }
                }

            }

        }

        public void Dispose()
        {
            _driver?.Close();
            _driver?.Dispose();
        }
    }
}
