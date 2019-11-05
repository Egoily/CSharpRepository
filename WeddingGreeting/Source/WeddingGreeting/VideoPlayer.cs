using Baidu.AI.Face;
using Baidu.AI.Face.Models;
using ee.Models;
using ee.Utility.Player;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WeddingGreeting
{
    public class VideoPlayer : IDisposable
    {
        public delegate void RecognisedHandler(Bitmap image, string userId, string userInfo);

        public delegate void NotRecognisedLongTimeHandler();

        public event RecognisedHandler FaceRecognised;

        public event NotRecognisedLongTimeHandler NotRecognisedLongTime;

        private AForgePlayer player;//播放器
        public PictureBox Container { get; private set; }
        public bool IsShownFace { get; set; } = true;

        public bool IsPlaying { get => player?.IsRunning ?? false; }

        private static bool isProcessing = false;
        private static long recognisedCount = 0;

        public VideoPlayer(PictureBox container)
        {
            InitPlayer();
            Container = container;
        }

        private void InitPlayer()
        {
            player = new AForgePlayer(new Size(640, 480), false);
            player.NewFrame += Player_NewFrame;
        }

        private void Player_NewFrame(ref Bitmap image, long frameIndex)
        {
            recognisedCount++;
            var img = new Bitmap(image);
            ThreadPool.QueueUserWorkItem(delegate
            {
                if (frameIndex % 4 == 0)
                    Processing(img, frameIndex);
            });
            if (Container.Image != null) Container.Image.Dispose();
            Container.Image = (Bitmap)(image.Clone());
            if (recognisedCount > 100)
            {
                NotRecognisedLongTime?.Invoke();
                recognisedCount = 0;
            }
        }

        private void Processing(Bitmap image, long frameIndex)
        {
            if (isProcessing) return;
            isProcessing = true;
            try
            {
                var rects = CheckFaces(ref image, out Bitmap maxFaceImage);
                if (rects != null && rects.Any())
                {
                    //if (frameIndex % 2 == 0)
                    //{
                    //    DrawFace(ref image, rects);
                    //}

                    Recognise(maxFaceImage, frameIndex);
                }
            }
            catch (Exception ex)
            {
            }
            isProcessing = false;
            image.Dispose();
        }

        private List<Rectangle> CheckFaces(ref Bitmap image, out Bitmap maxFaceImage)
        {
            return ee.Utility.OpenCv.FaceHandler.CheckFaces(ref image, out maxFaceImage);
        }

        private void DrawFace(ref Bitmap image, List<Rectangle> rects)
        {
            if (IsShownFace)
            {
                ee.Utility.OpenCv.FaceHandler.DrawFacesRect(ref image, rects, 200, 200, 1);
            }
        }

        private void Recognise(Bitmap maxFaceImage, long frameIndex)
        {
            var groupIds = new string[] { "EE" };
            var option = new SearchFaceOption()
            {
                Max_User_Num = 1,
            };
            var jObj = FaceApi.FaceSearch(maxFaceImage, groupIds, option);
            if (jObj != null && jObj.error_code == 0 && (jObj.result?.user_list?.Any() ?? false))
            {
                var target = jObj.result?.user_list.FirstOrDefault();
                if (target.score >= GlobalConfigs.Configurations.Threshold)
                {
                    FaceRecognised?.Invoke(maxFaceImage, target.user_id, target.user_info);

                    recognisedCount = 0;
                }
            }
        }

        public Dictionary<string, string> GetDevices()
        {
            return player?.Devices;
        }

        public void Play()
        {
            player?.Play();
        }
        public void Pause()
        {
            player?.Pause();
        }
        public void Stop()
        {
            player?.Stop();
        }
        public void SetVideoSource(string devName)
        {
            player.VideoSourceName = devName;
        }
        public void Dispose()
        {
            player?.Dispose();
        }
    }
}