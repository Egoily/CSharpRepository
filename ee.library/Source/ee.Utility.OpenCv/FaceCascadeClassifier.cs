using Emgu.CV;
using Emgu.CV.Structure;
using System.Collections.Generic;
using System.Drawing;

namespace ee.Utility.OpenCv
{
    public static class FaceCascadeClassifier
    {
        //人脸检测器
        private static CascadeClassifier faceClassifier = new CascadeClassifier(@"cascades/haarcascade_frontalface_default.xml");

        private static Size minSize = new Size(30, 30);

        public static void Init(string fileName, Size size)
        {
            faceClassifier = new CascadeClassifier(@fileName);
            minSize = size;
        }

        /// <summary>
        /// 获取人脸框
        /// </summary>
        /// <param name="img">要获取人脸的相片</param>
        /// <returns></returns>
        public static List<Rectangle> GetImageFaces(Bitmap img)
        {
            List<Rectangle> resultFaceImgInfos = new List<Rectangle>();

            if (img != null)
            {
                using (Image<Gray, byte> bgrframe = new Image<Gray, byte>(img))
                {
                    resultFaceImgInfos.AddRange(faceClassifier.DetectMultiScale(bgrframe, 1.15, 8, minSize));
                }
            }
            return resultFaceImgInfos;
        }
    }
}