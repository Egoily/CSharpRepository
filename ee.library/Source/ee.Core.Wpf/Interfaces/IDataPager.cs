namespace ee.Core.Wpf.Interfaces
{
    /// <summary>
    /// 数据分页接口
    /// </summary>
    public interface IDataPager
    {
        /// <summary>
        /// 总行数
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        int PageCount { get; set; }

        /// <summary>
        /// 首页
        /// </summary>
        void HomePage();
        /// <summary>
        /// 尾页
        /// </summary>
        void EndPage();
        /// <summary>
        /// 上一页
        /// </summary>
        void PreviousPage();

        /// <summary>
        /// 下一页
        /// </summary>
        void NextPage();



        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pageIndex"></param>
        void Fetch(int pageIndex);
    }
}
