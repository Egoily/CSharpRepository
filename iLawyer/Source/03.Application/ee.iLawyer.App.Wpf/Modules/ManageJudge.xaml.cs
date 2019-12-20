using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Extensions;
using ee.iLawyer.App.Wpf.ViewModels;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageJudge.xaml
    /// </summary>
    [BizModule(2, "Root", "法官管理", "module.judge", "", typeof(JudgeViewModel))]
    public partial class ManageJudge : UserControl
    {

        public ManageJudge()
        {
            InitializeComponent();
        }



    }
}
