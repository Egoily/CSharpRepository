using ee.Core.ComponentModel;
using ee.iLawyer.App.Wpf.ViewModels;
using System.Windows.Controls;

namespace ee.iLawyer.App.Wpf.Modules
{
    /// <summary>
    /// Interaction logic for Agenda.xaml
    /// </summary>
    [BizModule(5, "Root", "日程", "module.agenda", "", typeof(AgendaViewModel))]
    public partial class Agenda : UserControl
    {
        public Agenda()
        {
            InitializeComponent();
        }
        private void DisplayMonthChanged(UserControls.Agenda.MonthChangedEventArgs e)
        {

        }
    }
}
