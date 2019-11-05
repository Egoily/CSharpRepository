using ee.Core.Framework.ServiceLocation;
using ee.Core.ServiceLocation;
using ee.iLawyer.App.Wpf.ViewModels;

namespace ee.iLawyer.App.Wpf
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


        public ClientViewModel Client => ServiceLocator.Current.GetInstance<ClientViewModel>();
        public CourtViewModel Court => ServiceLocator.Current.GetInstance<CourtViewModel>();
        public HomeViewModel Home => ServiceLocator.Current.GetInstance<HomeViewModel>();
        public JudgeViewModel Judge => ServiceLocator.Current.GetInstance<JudgeViewModel>();
        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();
        public MainWindowViewModel MainWindow => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public ProjectViewModel Project => ServiceLocator.Current.GetInstance<ProjectViewModel>();

        //For Test
        public MessengerRegisterViewModel MessengerRegister => ServiceLocator.Current.GetInstance<MessengerRegisterViewModel>();
        public MessengerSenderViewModel MessengerSender => ServiceLocator.Current.GetInstance<MessengerSenderViewModel>();


        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }


}
