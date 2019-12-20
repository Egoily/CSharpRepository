using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.Core.Wpf.Designs;
using ee.iLawyer.App.Wpf.Models;
using ee.iLawyer.App.Wpf.ViewModels.Base;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.ServiceProvider;
using PropertyChanged;
using System.Linq;
using System.Windows.Forms;

namespace ee.iLawyer.App.Wpf.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    [Ioc(null, false, true)]
    public class ProjectViewModel : DataManipulationViewModel<Project>
    {
        private ILawyerServiceProvider serviceProvider;

        public override string PermissionCodePrefix => "root.project.";


        public RelayCommand<object> AddTodoItemCommand => new RelayCommand<object>(ExecuteAddTodoItemCommand);
        public RelayCommand<object> EditTodoItemCommand => new RelayCommand<object>(ExecuteEditTodoItemCommand);
        public RelayCommand<object> RemoveTodoItemCommand => new RelayCommand<object>(ExecuteRemoveTodoItemCommand);
        public RelayCommand<object> AddProgressCommand => new RelayCommand<object>(ExecuteAddProgressCommand);
        public RelayCommand<object> EditProgressCommand => new RelayCommand<object>(ExecuteEditProgressCommand);
        public RelayCommand<object> RemoveProgressCommand => new RelayCommand<object>(ExecuteRemoveProgressCommand);

        public ProjectViewModel()
        {
            serviceProvider = new ILawyerServiceProvider();
        }


        public virtual void ExecuteAddTodoItemCommand(object o)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                MessengerInstance.Send(new ShowViewArg("NewEditTodoItem", null, true), "ShowView");
            });
        }
        public virtual void ExecuteEditTodoItemCommand(object o)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                MessengerInstance.Send(new ShowViewArg("NewEditTodoItem", o, true), "ShowView");
            });
        }
        public virtual void ExecuteRemoveTodoItemCommand(object o)
        {
            var dr = System.Windows.Forms.MessageBox.Show("确定要删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                return;
            }

        }
        public virtual void ExecuteAddProgressCommand(object o)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                MessengerInstance.Send(new ShowViewArg("NewEditProgress", null, true), "ShowView");
            });
        }
        public virtual void ExecuteEditProgressCommand(object o)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                MessengerInstance.Send(new ShowViewArg("NewEditProgress", o, true), "ShowView");
            });
        }
        public virtual void ExecuteRemoveProgressCommand(object o)
        {

        }

        public override BaseQueryResponse<Project> Query()
        {
            return serviceProvider.QueryProject(new QueryProjectRequest() { Name = SearchText });
        }

        public override BaseResponse Create()
        {
            var request = DtoConverter.ConvertToCreateProjectRequest(TreadObject);
            return serviceProvider.CreateProject(request);
        }
        public override BaseResponse Update()
        {
            var request = DtoConverter.ConvertToUpdateProjectRequest(TreadObject);
            return serviceProvider.UpdateProject(request);
        }

        public override BaseResponse Remove()
        {
            return serviceProvider.RemoveProject(new RemoveProjectRequest()
            {
                Ids = new int[] { TreadObject.Id },

            });
        }


  




        private void UpdateTodoItem(ProjectTodoItem item)
        {
            if (TreadObject.TodoList == null)
            {
                return;
            }

            var editObj = TreadObject.TodoList.FirstOrDefault(x => x.Id == item.Id);
            if (editObj != null)
            {
                editObj.Content = item.Content;
                editObj.CreateTime = item.CreateTime;
                editObj.ExpiredTime = item.ExpiredTime;
                editObj.CompletedTime = item.CompletedTime;
                editObj.IsSetRemind = item.IsSetRemind;
                editObj.Name = item.Name;
                editObj.Priority = item.Priority;
                editObj.RemindTime = item.RemindTime;
                editObj.Status = item.Status;

            }
        }

        private void RemoveTodoItem(ProjectTodoItem item)
        {
            if (item != null)
            {
                TreadObject.TodoList.Remove(item);

            }
        }
        private void UpdateProgress(ProjectProgress item)
        {
            if (TreadObject.Progresses == null)
            {
                return;
            }

            var editObj = TreadObject.Progresses.FirstOrDefault(x => x.Id == item.Id);
            if (editObj != null)
            {
                editObj.HandleTime = item.HandleTime;
                editObj.Content = item.Content;
                editObj.CreateTime = item.CreateTime;

            }
        }
        private void RemoveProgress(ProjectProgress item)
        {
            if (item != null)
            {
                TreadObject.Progresses.Remove(item);

            }
        }


















    }
}
