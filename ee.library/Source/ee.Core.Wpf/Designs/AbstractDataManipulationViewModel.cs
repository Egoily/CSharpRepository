using ee.Core.Framework.Schema;
using ee.Core.Wpf.Designs;
using System;
using System.Windows.Input;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    public abstract class AbstractDataManipulationViewModel : IViewModel
    {
        public virtual ICommand QueryCommand => new RelayCommand<object>(ExecuteQueryCommand);
        public virtual ICommand NewCommand => new RelayCommand<object>(ExecuteNewCommandAsync);
        public virtual ICommand EditCommand => new RelayCommand<object>(ExecuteEditCommandAsync);
        public virtual ICommand DeleteCommand => new RelayCommand<object>(ExecuteDeleteCommand);

        public AbstractDataManipulationViewModel()
        {
        }


        public abstract void Query();
        public abstract BaseResponse Create();
        public abstract BaseResponse Update();
        public abstract BaseResponse Delete();

        public abstract void ExecuteNewCommandAsync(object o);
        public abstract void ExecuteEditCommandAsync(object o);
        public abstract void ExecuteDeleteCommand(object o);

        public abstract void DeleteItem(object sender, EventArgs eventArgs);

        public virtual void ExecuteQueryCommand(object o)
        {
            Query();
        }


    }
}
