using log4net;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace WeddingGreeting
{
    internal static class Program
    {

        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                GlobalConfigMgr.Load();
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                MessageBox.Show("程序发生异常");
            }



        }
    }
}