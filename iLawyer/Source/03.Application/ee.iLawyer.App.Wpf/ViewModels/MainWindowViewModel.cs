using ee.Core.ComponentModel;
using ee.Core.Wpf.Designs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [Ioc(null, false, true)]
    public class MainWindowViewModel: IViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null)
            {
                throw new ArgumentNullException(nameof(snackbarMessageQueue));
            }

            BizModules = BizModuleManager.Default.GetCallingAssemblyBizModules();

        }


        public IList<BizModule> BizModules { get; private set; }
    }
}