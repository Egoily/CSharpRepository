using System;

namespace ee.Core.Wpf.Enums
{
    /// <summary>
    /// 操作类型
    /// </summary>
    [Flags]
    public enum ActionMode
    {
        None = 0x0,
        Main = 1,
        Add = 1 << 1,
        Edit = 1 << 2,
        Remove = 1 << 3,
        DetailView = 1 << 4,

    }
}
