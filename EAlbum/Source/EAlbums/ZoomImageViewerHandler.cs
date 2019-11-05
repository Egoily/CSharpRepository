using System;
using System.Collections.Generic;
using System.Drawing;

namespace EAlbums
{

    public class ZoomImageViewerHandler
    {
        #region member fields


        //this is the coordinate of the center of the image displayed on the control in reference to 
        //the coordinate system of the original image
        Point displayCenter;

        //a bitmap that is used to draw the requested portion of the image on to the display
        Bitmap displayImage;



        //the amount by which the current image is zoomed
        double zoomAmount;

        public int Height { get; set; }

        public int Width { get; set; }
        #endregion

        #region member properties

        /// <summary>
        /// returns the image that is displayed on the screen
        /// </summary>
        public Image CurrentDisplayedImage
        {
            get { return displayImage; }
        }

        /// <summary>
        /// get the original image that is currently being displayed
        /// the entire image even though it is not fully displayed
        /// </summary>
        public Image OriginalImage { get; set; }

        /// <summary>
        /// gets or sets how the mouse behaves on the image control display area
        /// </summary>
        public PreviewMode ImagePreviewMode { get; set; }

        #endregion

        public ZoomImageViewerHandler(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            zoomAmount = 1;
            displayImage = new Bitmap(this.Width, this.Height);
        }
        public ZoomImageViewerHandler(Image image, int width, int height)
        {
            this.OriginalImage = image;
            this.Width = width;
            this.Height = height;
            zoomAmount = 1;
            displayImage = new Bitmap(this.Width, this.Height);
        }

        #region member methods

        /// <summary>
        /// this method rotated the image 
        /// </summary>
        /// <param name="clockWise">specifies if its clockwise or the other</param>
        public void RotateImage(bool clockWise)
        {
            if (OriginalImage != null)
            {
                OriginalImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
                ZoomImage(ZoomMode.FitPage);
            }
        }



        public void ZoomImage()
        {
            ZoomImage(ZoomMode.FitPage);
        }
        /// <summary>
        /// this method draws the image with the image mode specified
        /// </summary>
        /// <param name="zoomMode">describes how the original image will appear in the control</param>
        public void ZoomImage(ZoomMode zoomMode)
        {
            var amount = 1d;
            Point center;
            switch (zoomMode)
            {
                case ZoomMode.ActualSize:
                    amount = 1;
                    center = new Point(OriginalImage.Width / 2, OriginalImage.Height / 2);
                    break;
                case ZoomMode.FitHeight:
                    amount = (double)this.Height / OriginalImage.Height;
                    center = new Point(OriginalImage.Width / 2, OriginalImage.Height / 2);
                    break;
                case ZoomMode.FitPage:
                    amount = (double)this.Height / OriginalImage.Height;
                    if ((double)this.Width / OriginalImage.Width < amount)
                        amount = this.Width / OriginalImage.Width;
                    center = new Point(OriginalImage.Width / 2, OriginalImage.Height / 2);
                    break;
                case ZoomMode.FitWidth:
                    amount = (double)this.Width / OriginalImage.Width;
                    center = new Point(OriginalImage.Width / 2, OriginalImage.Height / 2);
                    break;
                default:
                    throw new Exception("Invalid zoom request!!");
            }
            ZoomImage(amount, center);
        }
        /// <summary>
        /// this is an overload that retains the center of the image and zooms the image with the value specified
        /// </summary>
        /// <param name="amount">the value by how much the image will be zoomed</param>
        public void ZoomImage(double amount)
        {
            ZoomImage(amount, new Point(OriginalImage.Width / 2, OriginalImage.Height / 2));
        }
        /// <summary>
        /// this method draws the image with the zoom amount as well as the center of the image specified
        /// </summary>
        /// <param name="amount">the value by how much the image will be zoomed</param>
        /// <param name="center">the center coordinate of the image in reference to the original coordinate system</param>
        public void ZoomImage(double amount, Point center)
        {
            //provided both the center and the zoom value 
            zoomAmount = amount;
            this.displayCenter = center;
            DrawImage();
        }
        /// <summary>
        /// this is an over load that retains both the center of the image as well as the zoom value 
        /// mostly used to redraw the image, in cases like resizing the control
        /// </summary>
        private void DrawImage()
        {
            //using the private members zoomvalue and display center 
            //compute the source rectangel from the original image
            Rectangle rect = new Rectangle(displayCenter.X - (int)(this.Width / (2 * zoomAmount)),
                          displayCenter.Y - (int)(this.Height / (2 * zoomAmount)),
                          (int)((double)this.Width / zoomAmount),
                          (int)((double)this.Height / zoomAmount));

            //call the drawimage function to extract the selected rectangel
            DrawImage(rect);
        }


        /// <summary>
        /// a method used by the class to draw the final region of the the original image 
        /// </summary>
        /// <param name="rect">the requested region of the image</param>
        private void DrawImage(Rectangle rect)
        {
            using (Graphics g = Graphics.FromImage(displayImage))
            {
                //first draw a white back ground to clear all the drawings performed earlier
                g.FillRectangle(Brushes.White, new Rectangle(0, 0, displayImage.Width, displayImage.Height));
                //draw the selected rectangel on to the displayedimage bitmap

                g.DrawImage(OriginalImage, new Rectangle(0, 0, displayImage.Width, displayImage.Height), rect, GraphicsUnit.Pixel);

            }
        }

        #endregion

    }
}


