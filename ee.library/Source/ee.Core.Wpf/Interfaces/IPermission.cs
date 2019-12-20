namespace ee.Core.Wpf.Interfaces
{
    /// <summary>
    ///权限操作接口
    /// </summary>
    public interface IPermission
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool ValidatePermission(string code);

        /// <summary>
        /// 加载模板权限
        /// </summary>
        void LoadPermissions();

        /// <summary>
        /// 权限值
        /// </summary>
        string PermissionCode { get; set; }

    }
}
