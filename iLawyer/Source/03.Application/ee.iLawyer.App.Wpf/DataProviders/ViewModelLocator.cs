using ee.Core.Framework.ServiceLocation;
using ee.Core.ServiceLocation;
using ee.iLawyer.App.Wpf.ViewModels;

namespace ee.iLawyer.App.Wpf.DataProviders
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => DependencyLocator.Default);

            DependencyLocator.Default.Register();

        }

        #region * ViewModel instances

        public static AgendaViewModel Agenda => ServiceLocator.Current.GetInstance<AgendaViewModel>();
        public static ClientViewModel Client => ServiceLocator.Current.GetInstance<ClientViewModel>();
        public static CourtViewModel Court => ServiceLocator.Current.GetInstance<CourtViewModel>();
        public static HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public static JudgeViewModel Judge => ServiceLocator.Current.GetInstance<JudgeViewModel>();
        public static LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public static MainWindowViewModel MainWindow => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public static ProjectViewModel Project => ServiceLocator.Current.GetInstance<ProjectViewModel>();

        //For Test
        public static MessengerRegisterViewModel MessengerRegister => ServiceLocator.Current.GetInstance<MessengerRegisterViewModel>();
        public static MessengerSenderViewModel MessengerSender => ServiceLocator.Current.GetInstance<MessengerSenderViewModel>();


        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }


}
