using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.Core.Wpf.Designs;
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


        public Schedule CurrentProjectTodoItem { get; set; }
        public ProjectProgress CurrentProjectProgress { get; set; }
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
            CurrentProjectTodoItem = new Schedule();
        }
        public virtual void ExecuteEditTodoItemCommand(object o)
        {
            CurrentProjectTodoItem = o as Schedule;
        }
        public virtual void ExecuteRemoveTodoItemCommand(object o)
        {
            var dr = System.Windows.Forms.MessageBox.Show("确定要删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                return;
            }
            RemoveTodoItem(o as Schedule);
        }
        public virtual void ExecuteAddProgressCommand(object o)
        {
            CurrentProjectProgress = new ProjectProgress();
        }
        public virtual void ExecuteEditProgressCommand(object o)
        {
            CurrentProjectProgress = o as ProjectProgress;
        }
        public virtual void ExecuteRemoveProgressCommand(object o)
        {
            var dr = System.Windows.Forms.MessageBox.Show("确定要删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                return;
            }
            RemoveProgress(o as ProjectProgress);
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







        private void UpdateTodoItem(Schedule item)
        {
            if (CurrentObject.TodoList == null)
            {
                return;
            }

            var editObj = CurrentObject.TodoList.FirstOrDefault(x => x.Id == item.Id);
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

        private void RemoveTodoItem(Schedule item)
        {
            if (item != null)
            {
                CurrentObject.TodoList.Remove(item);

            }
        }
        private void UpdateProgress(ProjectProgress item)
        {
            if (CurrentObject.Progresses == null)
            {
                return;
            }

            var editObj = CurrentObject.Progresses.FirstOrDefault(x => x.Id == item.Id);
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
                CurrentObject.Progresses.Remove(item);

            }
        }


















    }
}
