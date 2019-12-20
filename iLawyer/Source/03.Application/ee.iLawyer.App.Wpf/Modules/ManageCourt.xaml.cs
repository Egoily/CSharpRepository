using ee.Core.ComponentModel;
using ee.Core.Framework.Messaging;
using ee.Core.Wpf.Extensions;
using ee.iLawyer.App.Wpf.UserControls.Pickers;
using ee.iLawyer.App.Wpf.ViewModels;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageCourt.xaml
    /// </summary>
    [BizModule(1, "Root", "法院管理", "module.court", "", typeof(CourtViewModel))]
    public partial class ManageCourt : UserControl
    {
        public ManageCourt()
        {
            InitializeComponent();

        }





    }
}
