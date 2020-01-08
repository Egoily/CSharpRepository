using ee.Core.Data;
using ee.Core.Framework.Schema;
using ee.Core.Framework.Validations;
using ee.Core.Wpf.Designs;
using ee.Core.Wpf.Enums;
using ee.Core.Wpf.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ee.Core.Wpf.ViewModels
{
    public abstract class AbstractDataManipulationViewModel<T> : ViewModelBase, IViewModel, IDataPager, IValidationExceptionHandler
        where T : CloneableObject, new()
    {
        public AbstractDataManipulationViewModel()
        {
            DataSource = new ObservableCollection<T>();
            ButtonsInToolBar = new ObservableCollection<FuncButtonViewModel>();
            ButtonsInOpBar = new ObservableCollection<FuncButtonViewModel>();
            ButtonsInDetail = new ObservableCollection<FuncButtonViewModel>();

            SetDefaultButton(); //默认功能按钮
            SetPermissions();
        }

        public abstract string PermissionCodePrefix { get; }

        public virtual string PermissionCodeFormatter(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return string.Empty;
            }

            return PermissionCodePrefix + code;
        }





        private T _currentObject;
        private ObservableCollection<T> _dataSource;
        protected T TreadObject { get; set; }

        public ActionMode _mode = ActionMode.None;
        public ActionMode Mode
        {
            get { return _mode; }
            set { _mode = value; RaisePropertyChanged(); }
        }
        public T CurrentObject
        {
            get { return _currentObject; }
            set { _currentObject = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<T> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; RaisePropertyChanged(); }
        }



        #region 
        private int tabpageIndex;
        private string _searchText = string.Empty;

        private ObservableCollection<FuncButtonViewModel> _buttonsInToolBar;
        private ObservableCollection<FuncButtonViewModel> _buttonsInOpBar;
        private ObservableCollection<FuncButtonViewModel> _buttonsInDetail;


        public int TabPageIndex
        {
            get { return tabpageIndex; }
            set { tabpageIndex = value; RaisePropertyChanged(); }
        }

        public string SearchText
        {
            get { return _searchText; }
            set { _searchText = value; RaisePropertyChanged(); }
        }

        public ObservableCollection<FuncButtonViewModel> ButtonsInToolBar
        {
            get { return _buttonsInToolBar; }
            set { _buttonsInToolBar = value; RaisePropertyChanged(); }
        }
        public ObservableCollection<FuncButtonViewModel> ButtonsInOpBar
        {
            get { return _buttonsInOpBar; }
            set { _buttonsInOpBar = value; RaisePropertyChanged(); }
        }


        public ObservableCollection<FuncButtonViewModel> ButtonsInDetail
        {
            get { return _buttonsInDetail; }
            set { _buttonsInDetail = value; RaisePropertyChanged(); }
        }

        #endregion



        #region  * Commands

        public RelayCommand<object> AddCommand => new RelayCommand<object>(ExecuteAddCommand);
        public RelayCommand<object> EditCommand => new RelayCommand<object>(ExecuteEditCommand);
        public RelayCommand<object> RemoveCommand => new RelayCommand<object>(ExecuteRemoveCommand);
        public RelayCommand<object> QueryCommand => new RelayCommand<object>(ExecuteQueryCommand);

        public RelayCommand<object> SaveCommand => new RelayCommand<object>(ExecuteSaveCommand);
        public RelayCommand<object> CancelCommand => new RelayCommand<object>(ExecuteCancelCommand);
        public RelayCommand<object> ResetCommand => new RelayCommand<object>(ExecuteResetCommand);

        #endregion
        public abstract BaseResponse Create();
        public abstract BaseResponse Update();
        public abstract BaseResponse Remove();
        public abstract BaseQueryResponse<T> Query();
        public virtual void ExecuteAddCommand(object o)
        {
            CurrentObject = new T();
            Mode = ActionMode.Add;
            TabPageIndex = 1;

        }
        public virtual void ExecuteEditCommand(object o)
        {
            TreadObject = o as T;
            CurrentObject = TreadObject.DeepClone() as T;
            Mode = ActionMode.Edit;
            TabPageIndex = 1;

        }
        public virtual void ExecuteRemoveCommand(object o)
        {
            CurrentObject = o as T;
            Mode = ActionMode.Remove;
            DataManipulation();
            Mode = ActionMode.Main;

        }
        public virtual void ExecuteQueryCommand(object o)
        {
            Fetch(PageIndex);
        }
        public virtual void ExecuteSaveCommand(object o)
        {

            DataManipulation();

        }

        public virtual void DataManipulationCompleted(ref BaseResponse response)
        {
            if (response != null && response.IsOk())
            {
                Mode = ActionMode.Main;
                TabPageIndex = 0;
                Fetch(PageIndex);
            }
        }
        private BaseResponse DataManipulation()
        {
            BaseResponse response = null;
            try
            {
                if (CurrentObject == null)
                {
                    return new BaseResponse() { Code = Framework.ErrorCodes.NullParameter, Message = "对象为空." };
                }
                switch (Mode)
                {
                    case ActionMode.None:
                        break;
                    case ActionMode.Add:
                        TreadObject = CurrentObject;
                        response = Create();
                        break;
                    case ActionMode.Edit:
                        if (!TreadObject.AreEqual(CurrentObject))
                        {
                            TreadObject = CurrentObject;
                            response = Update();
                        }
                        else
                        {
                            return new BaseResponse() { Code = 0 };
                        }

                        break;
                    case ActionMode.Remove:
                        TreadObject = CurrentObject;
                        response = Remove();
                        break;
                    case ActionMode.DetailView:
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                response = new BaseResponse() { Code = Framework.ErrorCodes.UnknownError, Message = "未知错误" };
            }
            DataManipulationCompleted(ref response);
            return response;
        }
        public virtual void ExecuteCancelCommand(object o)
        {
            CurrentObject = null;
            Mode = ActionMode.Main;
            TabPageIndex = 0;

        }
        public virtual void ExecuteResetCommand(object o)
        {
            SearchText = string.Empty;
        }

        //public virtual void UpdateSource()
        //{
        //    MessengerInstance.Send("", "UpdateSource");
        //}


        private void SetPermissions()
        {
            var permissions = LoadPermissions();//加载模块权限

            if (permissions != null && permissions.Any())
            {
                if (ButtonsInToolBar != null && ButtonsInToolBar.Any())
                {
                    for (int i = 0; i < ButtonsInToolBar.Count; i++)
                    {
                        ButtonsInToolBar[i].IsVisibility = string.IsNullOrEmpty(ButtonsInToolBar[i].PermissionCode) || permissions.Contains(ButtonsInToolBar[i].PermissionCode);
                        if (!ButtonsInToolBar[i].IsVisibility)
                        {
                            ButtonsInToolBar[i].IsHide = true;
                        }
                    }
                }
                if (ButtonsInOpBar != null && ButtonsInOpBar.Any())
                {
                    for (int i = 0; i < ButtonsInOpBar.Count; i++)
                    {
                        ButtonsInOpBar[i].IsVisibility = string.IsNullOrEmpty(ButtonsInOpBar[i].PermissionCode) || permissions.Contains(ButtonsInOpBar[i].PermissionCode);
                        if (!ButtonsInOpBar[i].IsVisibility)
                        {
                            ButtonsInOpBar[i].IsHide = true;
                        }
                    }
                }
                if (ButtonsInDetail != null && ButtonsInDetail.Any())
                {
                    for (int i = 0; i < ButtonsInDetail.Count; i++)
                    {
                        ButtonsInDetail[i].IsVisibility = string.IsNullOrEmpty(ButtonsInDetail[i].PermissionCode) || permissions.Contains(ButtonsInDetail[i].PermissionCode);
                        if (!ButtonsInDetail[i].IsVisibility)
                        {
                            ButtonsInDetail[i].IsHide = true;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 设置默认按钮
        /// </summary>
        public virtual void SetDefaultButton()
        {
            Mode = ActionMode.Main;
        }




        #region * Permissions


        /// <summary>
        /// 加载权限
        /// </summary>
        public abstract IList<string> LoadPermissions();



        #endregion

        #region IDataPager

        public RelayCommand HomePageCommand => new RelayCommand(() => HomePage());
        public RelayCommand EndPageCommand => new RelayCommand(() => EndPage());
        public RelayCommand PreviousPageCommand => new RelayCommand(() => PreviousPage());
        public RelayCommand NextPageCommand => new RelayCommand(() => NextPage());



        private int totalCount = 0;
        private int pageSize = 4;
        private int pageIndex = 1;
        private int pageCount = 0;

        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
            set { totalCount = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 当前页大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 分页总数
        /// </summary>
        public int PageCount
        {
            get { return pageCount; }
            set { pageCount = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 首页
        /// </summary>
        public virtual void HomePage()
        {
            if (this.PageIndex == 1)
            {
                return;
            }

            PageIndex = 1;

            Fetch(PageIndex);
        }
        /// <summary>
        /// 尾页
        /// </summary>
        public virtual void EndPage()
        {
            this.PageIndex = PageCount;

            Fetch(PageCount);
        }
        /// <summary>
        /// 上一页
        /// </summary>
        public virtual void PreviousPage()
        {
            if (this.PageIndex == 1)
            {
                return;
            }

            PageIndex--;

            Fetch(PageIndex);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public virtual void NextPage()
        {
            if (this.PageIndex == PageCount)
            {
                return;
            }

            PageIndex++;

            Fetch(PageIndex);
        }



        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        public virtual void Fetch(int pageIndex)
        {
            DataSource = new ObservableCollection<T>();
            var response = Query();
            if (response != null && response.IsOk() && (response.QueryList?.Any() ?? false))
            {
                response.QueryList.ToList().ForEach(x => DataSource.Add(x));
            }

        }

        /// <summary>
        /// 设置页数
        /// </summary>
        public virtual void SetPageCount()
        {
            PageCount = Convert.ToInt32(Math.Ceiling((double)TotalCount / (double)PageSize));
        }

        #endregion



        #region IValidationExceptionHandler

        private bool isValid;
        private bool isClear;

        /// <summary>
        /// 实体验证是否通过
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 清除异常错误
        /// </summary>
        public bool IsClear
        {
            get { return isClear; }
            set { isClear = value; RaisePropertyChanged(); }
        }

        #endregion
    }
}
