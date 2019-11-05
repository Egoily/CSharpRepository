using ee.Core.ComponentModel;
using ee.Core.Wpf.Designs;
using ee.iLawyer.App.Wpf.Modules;
using PropertyChanged;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [BizModule(0,"Root","首页","Home","",typeof(Home))]
    [Ioc(null, false, true)]
    public class HomeViewModel: IViewModel
    {




    }



}
