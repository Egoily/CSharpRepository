using System;
using System.Runtime.InteropServices;

namespace EgoDevil.Utilities.UI.MessageForm
{
    /// <summary>
    /// Win32 implementation to show / hide a window with animation.
    /// </summary>

    public class AnimateShowWnd
    {
        /// <summary>
        /// Animates the window from left to right. This flag can be used with roll or slide animation.
        /// </summary>
        public const int AW_HOR_POSITIVE = 0X1;//从左到右打开窗口

        /// <summary>
        /// Animates the window from right to left. This flag can be used with roll or slide animation.
        /// </summary>
        public const int AW_HOR_NEGATIVE = 0X2;//从右到左打开窗口

        /// <summary>
        /// Animates the window from top to bottom. This flag can be used with roll or slide animation.
        /// </summary>
        public const int AW_VER_POSITIVE = 0X4;//从上到下打开窗口

        /// <summary>
        /// Animates the window from bottom to top. This flag can be used with roll or slide animation.
        /// </summary>
        public const int AW_VER_NEGATIVE = 0X8;//从下到上打开窗口

        /// <summary>
        /// Makes the window appear to collapse inward if AW_HIDE is used or expand outward if the
        /// AW_HIDE is not used.
        /// </summary>
        public const int AW_CENTER = 0X10;//看不出任何效果

        /// <summary>
        /// Hides the window. By default, the window is shown.
        /// </summary>
        public const int AW_HIDE = 0X10000;//在窗体卸载时若想使用本函数就得加上此常量

        /// <summary>
        /// Activates the window.
        /// </summary>
        public const int AW_ACTIVATE = 0X20000;//在窗体通过本函数打开后，默认情况下会失去焦点，除非加上本常量

        /// <summary>
        /// Uses slide animation. By default, roll animation is used.
        /// </summary>
        public const int AW_SLIDE = 0X40000;//看不出任何效果

        /// <summary>
        /// Uses a fade effect. This flag can be used only if hwnd is a top-level window.
        /// </summary>
        public const int AW_BLEND = 0X80000;//淡入淡出效果

        /// <summary>
        /// Animates a window.
        /// </summary>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);
    }
}