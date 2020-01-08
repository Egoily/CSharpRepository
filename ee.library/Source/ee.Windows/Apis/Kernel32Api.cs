using System.Runtime.InteropServices;

namespace ee.Windows.Apis
{
    public class Kernel32Api
    {
        #region Kernel32.dll functions
        [DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetCurrentThreadId();
        #endregion
    }
}
