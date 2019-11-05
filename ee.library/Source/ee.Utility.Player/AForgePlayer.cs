using AForge.Controls;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ee.Utility.Player
{
    public class AForgePlayer : IPlayer
    {
        /// <summary>
        /// 视频源
        /// </summary>
        private VideoCaptureDevice VideoSource { get; set; }

        /// <summary>
        /// 播放器
        /// </summary>
        private VideoSourcePlayer Player { get; set; }

        private Size actualResolution;

        /// <summary>
        /// 真实视频分辨率
        /// </summary>
        public Size ActualResolution
        {
            get
            {
                return actualResolution;
            }
            set
            {
                actualResolution = value;
            }
        }

        /// <summary>
        ///自定义分辨率
        /// </summary>
        public Size? CustomResolution { get; set; } = null;

        /// <summary>
        /// 是否正在播放（播放插件自带属性）
        /// </summary>
        public bool IsRunning
        {
            get { return VideoSource?.IsRunning ?? false; }
        }

        /// <summary>
        /// 播放状态
        /// </summary>
        public Enums.PlayerStates PlayerState { get; protected set; }

        /// <summary>
        /// 帧索引
        /// </summary>
        public int FrameIndex { get; protected set; }

        public string VideoSourceName { get; set; }

        /// <summary>
        /// 播放新帧的委托
        /// </summary>
        /// <param name="image"></param>
        /// <param name="frameIndex"></param>
        public delegate void NewFrameHandler(ref Bitmap image, long frameIndex);

        /// <summary>
        /// 新帧调用事件
        /// </summary>
        public event NewFrameHandler NewFrame;

        /// <summary>
        /// 设备列表
        /// </summary>
        public Dictionary<string, string> Devices { get; private set; }

        /// <summary>
        /// 初始化播放器
        /// </summary>
        public AForgePlayer(bool isOpenFirstDevice = false)
        {
            Init(isOpenFirstDevice);
        }

        /// <summary>
        /// 初始化播放器,指定分辨率
        /// </summary>
        public AForgePlayer(Size? customResolution, bool isOpenFirstDevice = false)
        {
            CustomResolution = customResolution;
            Init(isOpenFirstDevice);
        }

        private void Init(bool isOpenFirstDevice = false)
        {
            GetDevices();
            var firstDev = Devices?.FirstOrDefault();
            VideoSourceName = firstDev?.Key ?? null;
            if (isOpenFirstDevice)
            {
                OpenFirstDevice();
            }
        }

        /// <summary>
        /// 新帧处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="image"></param>
        private void OnNewFrame(object sender, ref Bitmap image)
        {
            if (FrameIndex > int.MaxValue) FrameIndex = 0;
            ++FrameIndex;
            NewFrame?.Invoke(ref image, FrameIndex);
        }

        /// <summary>
        /// 获取最大分辨率
        /// </summary>
        private VideoCapabilities GetMaxVideoCapabilities()
        {
            if (VideoSource.VideoCapabilities == null || !VideoSource.VideoCapabilities.Any())
            {
                return null;
            }
            VideoCapabilities maxVideoCapabilitie = null;
            foreach (var item in VideoSource.VideoCapabilities)
            {
                maxVideoCapabilitie = Compare(maxVideoCapabilitie, item);
            }
            return maxVideoCapabilitie;
        }

        private VideoCapabilities GetSuitableVideoCapabilities()
        {
            if (VideoSource.VideoCapabilities == null || !VideoSource.VideoCapabilities.Any())
            {
                return null;
            }
            var index = VideoSource.VideoCapabilities.Count() / 2;
            return VideoSource.VideoCapabilities[index];
        }

        /// <summary>
        /// 获取自定义分辨率
        /// </summary>
        private VideoCapabilities GetCustomVideoCapabilities(Size size)
        {
            var capabilities = VideoSource.VideoCapabilities.FirstOrDefault(x => x.FrameSize == size);
            if (capabilities == null)
            {
                capabilities = GetSuitableVideoCapabilities();
            }

            return capabilities;
        }

        /// <summary>
        /// 获取设备名称
        /// </summary>
        private static Dictionary<string, string> GetVideoDeviceNames()
        {
            Dictionary<string, string> listName = new Dictionary<string, string>();
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for (int i = 0; i < videoDevices.Count; i++)
            {
                listName.Add(videoDevices[i].MonikerString, videoDevices[i].Name);
            }
            return listName;
        }

        private static VideoCapabilities Compare(VideoCapabilities source, VideoCapabilities target)
        {
            if (source == null)
            {
                return target;
            }
            if (source.FrameSize.Width * source.FrameSize.Height < target.FrameSize.Width * target.FrameSize.Height)
            {
                return target;
            }
            else
            {
                return source;
            }
        }

        /// <summary>
        /// 获取设备列表
        /// </summary>
        public virtual void GetDevices()
        {
            Devices = GetVideoDeviceNames();
        }

        /// <summary>
        /// 打开第一个设备
        /// </summary>
        public virtual void OpenFirstDevice()
        {
            if (Devices.Count > 0)
            {
                var firstDev = Devices.FirstOrDefault();
                OpenDevice(firstDev.Key);//打开第一个设备
            }
            else
            {
                throw new Exception("Not found any camera device.");
            }
        }

        /// <summary>
        /// 打开摄像头
        /// </summary>
        public virtual void OpenDevice(string device)
        {
            Console.WriteLine($">>>>Open device:[{device}]");
            if (String.IsNullOrEmpty(device))
                throw new Exception("Open with null device name.");
            if (VideoSource != null)
            {
                VideoSource.Stop();
                VideoSource = null;
            }
            //重置播放器
            if (Player != null)
                Player.Dispose();
            Player = new VideoSourcePlayer();
            Player.NewFrame += OnNewFrame;
            VideoSource = new VideoCaptureDevice(device);
            VideoSourceName = device;
            VideoCapabilities videoResolution;
            if (CustomResolution == null)
            {
                videoResolution = GetMaxVideoCapabilities();//获取最大分辨率
                if (videoResolution == null)
                {
                    VideoSourceName = null;
                    throw new Exception("Failed to get video resolution,please set it in configuration manually");
                }
                VideoSource.VideoResolution = videoResolution;
                actualResolution = videoResolution.FrameSize;
            }
            else
            {
                videoResolution = GetCustomVideoCapabilities(CustomResolution.Value);
                if (videoResolution != null)
                {
                    VideoSource.VideoResolution = videoResolution;
                    actualResolution = videoResolution.FrameSize;
                }
                else
                {
                    actualResolution = new Size(0, 0);
                }
            }
        }

        /// <summary>
        /// 开始播放
        /// </summary>
        /// <param name="device"></param>
        public virtual void Play(string device = null)
        {

            FrameIndex = 0;
            if (string.IsNullOrEmpty(device))
            {
                OpenFirstDevice();
            }
            else
            {
                OpenDevice(device);
            }
            Player.VideoSource = VideoSource;
            Player.Start();
            PlayerState = Enums.PlayerStates.Play;
        }
        /// <summary>
        /// 开始播放
        /// </summary>
        /// <param name="device"></param>
        public virtual void Play()
        {
            Play(VideoSourceName);
        }
        /// <summary>
        /// 暂停播放，用于拍照，isOpening = true，继续播放调用Play()
        /// </summary>
        public virtual void Pause()
        {
            if (Player == null)
                return;
            Player.SignalToStop();
            PlayerState = Enums.PlayerStates.Pause;
        }

        /// <summary>
        /// 停止播放
        /// </summary>
        /// <param name="player"></param>
        public virtual void Stop()
        {
            if (Player == null)
                return;


            Player?.SignalToStop();
            VideoSource?.Stop();
            VideoSource = null;
            PlayerState = Enums.PlayerStates.Stop;
        }

        public virtual void Dispose()
        {
            if (Player != null && !Player.IsDisposed)
            {
                Player.NewFrame -= OnNewFrame;
                Player.SignalToStop();
                Player.WaitForStop();
                Player.Dispose();
            }
        }
    }
}