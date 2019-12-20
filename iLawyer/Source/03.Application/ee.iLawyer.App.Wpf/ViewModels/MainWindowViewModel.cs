using ee.Core.ComponentModel;
using ee.Core.Wpf.Designs;
using ee.iLawyer.ServiceProvider;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [Ioc(null, false, true)]
    public class MainWindowViewModel : IViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null)
            {
                throw new ArgumentNullException(nameof(snackbarMessageQueue));
            }
            //if (Cacher.Loginer != null && Cacher.Loginer.IsAdmin)
            //{
            //    BizModules = BizModuleManager.Default.GetCallingAssemblyBizModules();
            //}
            //else
            {
                BizModules = BizModuleManager.Default.GetCallingAssemblyBizModules(Cacher.Loginer?.Resources?.ToArray());
            }
        }


        public IList<BizModule> BizModules { get; private set; }
    }
}