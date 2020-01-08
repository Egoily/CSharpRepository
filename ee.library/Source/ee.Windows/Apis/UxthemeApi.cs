using System;
using System.Runtime.InteropServices;

namespace ee.Windows.Apis
{
    public class UxthemeApi
    {
        #region Uxtheme.dll functions
        [DllImport("uxtheme.dll")]
        static public extern int SetWindowTheme(IntPtr hWnd, string appId, string classId);
        #endregion
    }
}
