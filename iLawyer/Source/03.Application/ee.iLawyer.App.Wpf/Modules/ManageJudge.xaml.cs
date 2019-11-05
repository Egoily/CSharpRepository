using ee.iLawyer.App.Wpf.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for ManageJudge.xaml
    /// </summary>
    public partial class ManageJudge : UserControl
    {

        public ManageJudge()
        {
            InitializeComponent();

        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            (DataContext as JudgeViewModel).DeleteItem(sender, eventArgs);
        }
    }
}
