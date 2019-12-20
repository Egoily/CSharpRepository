using ee.Core.Wpf.Designs;
using ee.Core.Wpf.Enums;

namespace ee.Core.Wpf.ViewModels
{

    /// <summary>
    /// 功能按钮
    /// </summary>
    public class FuncButtonViewModel<T> : ViewModelBase where T : class, new()
    {
        private string _name;
        private string _icon;
        private bool _isHide = false;
        private bool _isVisibility;
        private string _permissionCode;
        private string _mark;
        private RelayCommand<T> command;
        private ActionMode _inModel;
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 是否隐藏在功能列表， 集成在表格中
        /// </summary>
        public bool IsVisibility
        {
            get { return _isVisibility; }
            set { _isVisibility = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 权限定义值
        /// </summary>
        public string PermissionCode
        {
            get { return _permissionCode; }
            set { _permissionCode = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 显示符号代码
        /// </summary>
        public string Mark
        {
            get { return _mark; }
            set { _mark = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 功能命令
        /// </summary>
        public RelayCommand<T> Command
        {
            get { return command; }
            set { command = value; RaisePropertyChanged(); }
        }

        public ActionMode InModel
        {
            get { return _inModel; }
            set { _inModel = value; RaisePropertyChanged(); }
        }


    }


    public class FuncButtonViewModel : ViewModelBase
    {
        private string _name;
        private string _icon;
        private bool _isHide = false;
        private bool _isVisibility;
        private string _permissionCode;
        private string _mark;
        private RelayCommand<object> command;
        private ActionMode _inModel;
        /// <summary>
        /// 功能名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon
        {
            get { return _icon; }
            set { _icon = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        public bool IsHide
        {
            get { return _isHide; }
            set { _isHide = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsVisibility
        {
            get { return _isVisibility; }
            set { _isVisibility = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 权限定义值
        /// </summary>
        public string PermissionCode
        {
            get { return _permissionCode; }
            set { _permissionCode = value; RaisePropertyChanged(); }
        }

        /// <summary>
        /// 显示符号代码
        /// </summary>
        public string Mark
        {
            get { return _mark; }
            set { _mark = value; RaisePropertyChanged(); }
        }


        /// <summary>
        /// 功能命令
        /// </summary>
        public RelayCommand<object> Command
        {
            get { return command; }
            set { command = value; RaisePropertyChanged(); }
        }

        public ActionMode InModel
        {
            get { return _inModel; }
            set { _inModel = value; RaisePropertyChanged(); }
        }


    }
}
