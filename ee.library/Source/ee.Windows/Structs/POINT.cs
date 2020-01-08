using System.Runtime.InteropServices;

namespace ee.Windows.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}
