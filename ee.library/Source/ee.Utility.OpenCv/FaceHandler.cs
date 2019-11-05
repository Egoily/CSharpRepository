using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ee.Utility.OpenCv
{
    public static class FaceHandler
    {
        /// <summary>
        /// 检测识别人脸区域
        /// </summary>
        /// <param name="image"></param>
        /// <param name="maxFaceImage"></param>
        /// <returns></returns>
        public static List<Rectangle> CheckFaces(ref Bitmap image, out Bitmap maxFaceImage)
        {
            var imgWidth = image.Width;
            var imgHeight = image.Height;
            var facerect = FaceCascadeClassifier.GetImageFaces(image).Select(x => ConvertFaceRect(x, imgWidth, imgHeight)).ToList();
            var maxFaceRect = GetMaxFaceRect(facerect);
            maxFaceImage = CutFacesRect(image, maxFaceRect);
            return facerect;
        }

        public static Rectangle GetMaxFace(ref Bitmap image, out Bitmap maxFaceImage)
        {
            var imgWidth = image.Width;
            var imgHeight = image.Height;
            var facerect = FaceCascadeClassifier.GetImageFaces(image).Select(x => ConvertFaceRect(x, imgWidth, imgHeight)).ToList();
            var maxFaceRect = GetMaxFaceRect(facerect);
            maxFaceImage = CutFacesRect(image, maxFaceRect);
            return maxFaceRect;
        }

        /// <summary>
        /// 识别所有人脸
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static List<Bitmap> RecogniseFaces(ref Bitmap image)
        {
            List<Bitmap> images = new List<Bitmap>();
            var imgWidth = image.Width;
            var imgHeight = image.Height;
            var facerect = FaceCascadeClassifier.GetImageFaces(image).Select(x => ConvertFaceRect(x, imgWidth, imgHeight)).ToList();

            foreach (var rect in facerect)
            {
                images.Add(CutFacesRect(image, rect));
            }
            return images;
        }

        /// <summary>
        /// 图片框人脸
        /// </summary>
        /// <param name="image"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="lineWidth"></param>
        public static List<Rectangle> DrawFaces(ref Bitmap image, int width, int height, int lineWidth)
        {
            Bitmap maxFaceImage = null;
            var facerect = CheckFaces(ref image, out maxFaceImage);
            DrawFacesRect(ref image, facerect, width, height, lineWidth);
            return facerect;
        }

        #region **人脸检测

        /// <summary>
        /// 转换人脸框变成框选整个头部
        /// </summary>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <returns></returns>
        public static Rectangle ConvertFaceRect(Rectangle rect, int width, int height)
        {
            //计算高
            int y = Convert.ToInt32(rect.Y - rect.Height * 0.3 / 2);
            y = y > 0 ? y : 0;
            int h = Convert.ToInt32(rect.Height * 1.3);
            h = y + h > height ? height - y : h;

            //计算宽
            int x = Convert.ToInt32(rect.X - rect.Width * 0.3 / 2);
            x = x > 0 ? x : 0;
            int w = Convert.ToInt32(rect.Width * 1.3);
            w = x + w > width ? width - x : w;

            Rectangle resultRect = new Rectangle(x, y, w, h);
            return resultRect;
        }

        /// <summary>
        /// 转换人脸框变成框选整个头部
        /// </summary>
        /// <returns></returns>
        public static List<Rectangle> ConvertFaceRect(List<Rectangle> rectList, int width, int height)
        {
            List<Rectangle> resultRects = new List<Rectangle>();
            foreach (var rect in rectList)
            {
                resultRects.Add(ConvertFaceRect(rect, width, height));
            }
            return resultRects;
        }

        /// <summary>
        /// 按实际宽度绘制人脸线框
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rectList"></param>
        /// <param name="width">图片显示控件宽度</param>
        /// <param name="height">图片显示控件高度</param>
        /// <param name="lineWidth">相对线宽</param>
        /// <returns></returns>
        public static void DrawFacesRect(ref Bitmap img, List<Rectangle> rectList, int width, int height, int lineWidth)
        {
            //取图片变化的倍率
            double imgRate = img.Width * 1.0 / width > img.Height * 1.0 / height ? width * 1.0 / img.Width : height * 1.0 / img.Height;
            int imgLineWidth = Convert.ToInt32(lineWidth / imgRate);//按控件大小与图片比例计算线宽
            if (imgLineWidth < 1)
                imgLineWidth = 1;
            if (rectList != null && rectList.Count > 0)
            {
                Graphics g = Graphics.FromImage(img);
                Pen pen = new Pen(Color.Red, imgLineWidth);
                foreach (var rect in rectList)
                {
                    g.DrawRectangle(pen, rect);
                }
                pen.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        /// 按实际宽度绘制人脸线框
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rectList"></param>
        /// <param name="width">图片显示控件宽度</param>
        /// <param name="height">图片显示控件高度</param>
        /// <param name="lineWidth">相对线宽</param>
        /// <returns></returns>
        public static void DrawFacesRect(ref Bitmap img, Rectangle rect, int width, int height, int lineWidth)
        {
            //取图片变化的倍率
            double imgRate = img.Width * 1.0 / width > img.Height * 1.0 / height ? width * 1.0 / img.Width : height * 1.0 / img.Height;
            int imgLineWidth = Convert.ToInt32(lineWidth / imgRate);//按控件大小与图片比例计算线宽
            if (imgLineWidth < 1)
                imgLineWidth = 1;
            if (rect != null)
            {
                Graphics g = Graphics.FromImage(img);
                Pen pen = new Pen(Color.Red, imgLineWidth);
                g.DrawRectangle(pen, rect);
                g.Dispose();
            }
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <returns></returns>
        public static List<Bitmap> CutFacesRect(Bitmap img, List<Rectangle> rectList)
        {
            List<Bitmap> resultImgs = new List<Bitmap>();
            foreach (var rect in rectList)
            {
                Bitmap newImg = CutFacesRect(img, rect);
                resultImgs.Add(newImg);
            }
            return resultImgs;
        }

        /// <summary>
        /// 裁剪图片
        /// </summary>
        /// <returns></returns>
        public static Bitmap CutFacesRect(Bitmap img, Rectangle rect)
        {
            if (rect.IsEmpty) return null;
            Bitmap resultImg = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(resultImg);
            g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect.X, rect.Y, rect.Width, rect.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return resultImg;
        }

        /// <summary>
        /// 获取最大人脸框
        /// </summary>
        /// <returns></returns>
        public static Rectangle GetMaxFaceRect(List<Rectangle> rectList)
        {
            Rectangle resultRect = new Rectangle();
            if (rectList != null && rectList.Any())
            {
                resultRect = rectList[0];
                foreach (var rect in rectList)
                {
                    if (resultRect.Width * resultRect.Height < rect.Width * rect.Height)
                        resultRect = rect;
                }
            }
            return resultRect;
        }

        #endregion **人脸检测
    }
}