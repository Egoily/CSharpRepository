using ee.Core.Wpf.Designs;
using PropertyChanged;

namespace ee.iLawyer.App.Wpf.ViewModels.Base
{
    [AddINotifyPropertyChangedInterface]
    public abstract class CloseViewViewModel : IViewModel
    {

        /// <summary>
        /// 是否关闭View
        /// </summary>
        public bool IsCloseView { get; set; }

        private RelayCommand closeView;
        /// <summary>
        /// 关闭View
        /// </summary>
        public RelayCommand CloseView
        {
            get
            {
                if (closeView == null)
                {
                    closeView = new RelayCommand(() => ExcuteCloseViewCommand());
                }

                return closeView;

            }
            set { closeView = value; }
        }

        protected void ExcuteCloseViewCommand()
        {
            IsCloseView = true;
        }


        /// <summary>
        /// 是否隐藏View
        /// </summary>
        public bool IsHideView { get; set; }

        private RelayCommand hideView;
        /// <summary>
        /// 隐藏View
        /// </summary>
        public RelayCommand HideView
        {
            get
            {
                if (hideView == null)
                {
                    hideView = new RelayCommand(() => ExcuteHideViewCommand());
                }

                return hideView;

            }
            set { hideView = value; }
        }

        protected void ExcuteHideViewCommand()
        {
            IsHideView = true;
        }
    }
}
