using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media;

namespace ee.Utility.Player
{
    public class WpfAForgeVideoPlayer : AForgePlayer, IDisposable, IPlayer
    {
        /// <summary>
        /// 用于绑定WPF控件
        /// </summary>
        public InteropBitmap FrameBitmapSource { get; private set; }

        /// <summary>
        /// 图片非托管地址
        /// </summary>
        private IntPtr map;

        private IntPtr section;
        private int width = 0;
        private int height = 0;

        /// <summary>
        /// 初始化播放器
        /// </summary>
        public WpfAForgeVideoPlayer(bool isOpenFirstDevice = false) : base(isOpenFirstDevice)
        {
        }

        /// <summary>
        /// 初始化播放器,指定分辨率
        /// </summary>
        public WpfAForgeVideoPlayer(Size? customResolution, bool isOpenFirstDevice = false) : base(customResolution, isOpenFirstDevice)
        {
        }

        /// <summary>
        /// 更新帧
        /// </summary>
        /// <param name="image"></param>
        public void UpdateFrame(ref Bitmap image)
        {
            try
            {
                var tmpImage = new Bitmap(image);
                //帧序号
                if (FrameIndex < Int32.MaxValue)
                    FrameIndex++;
                else
                    FrameIndex = 0;
                if (map != IntPtr.Zero)
                {
                    int imgWidth = (int)tmpImage.Width;
                    int imgHeight = (int)tmpImage.Height;
                    System.Drawing.Imaging.BitmapData bmpData = tmpImage.LockBits(new System.Drawing.Rectangle(0, 0, imgWidth, imgHeight),
                        System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
                    /* Get the pointer to the pixels */
                    IntPtr pBmp = bmpData.Scan0;
                    int srcStride = bmpData.Stride;
                    int pSize = srcStride * imgHeight;
                    CopyMemory(map, pBmp, pSize);
                    tmpImage.UnlockBits(bmpData);
                    if (FrameBitmapSource != null)
                    {
                        FrameBitmapSource.Invalidate();
                    }
                }
                if (tmpImage != null)
                {
                    tmpImage.Dispose();
                    tmpImage = null;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 打开摄像头
        /// </summary>
        public override void OpenDevice(string deviceName)
        {
            base.OpenDevice(deviceName);
            //创建内存映射
            width = ActualResolution.Width;
            height = ActualResolution.Height;
            uint pcount = (uint)(this.width * this.height * PixelFormats.Bgr32.BitsPerPixel / 8.0);
            section = CreateFileMapping(new IntPtr(-1), IntPtr.Zero, 0x04, 0, pcount, null);
            map = MapViewOfFile(section, 0xF001F, 0, 0, pcount);
            FrameBitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromMemorySection(section, (int)this.width, (int)this.height, PixelFormats.Bgr32,
                (int)(this.width * PixelFormats.Bgr32.BitsPerPixel / 8), 0) as InteropBitmap;
        }

        public override void Dispose()
        {
            base.Dispose();
            bool result;
            result = UnmapViewOfFile(map);//释放句柄
            result = CloseHandle(section);//释放句柄
            map = IntPtr.Zero;
            section = IntPtr.Zero;
        }

        [DllImport("Kernel32.dll", EntryPoint = "RtlMoveMemory")]
        private static extern void CopyMemory(IntPtr Destination, IntPtr Source, int Length);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFileMapping(IntPtr hFile, IntPtr lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, uint dwNumberOfBytesToMap);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);
    }
}