using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace EAlbums
{
    [Serializable]
    public class ThumbImage : IThumbImage
    {
        private const double PiFact = Math.PI / 180.0f;//Circle angle

        private double actualAngle;

        private Rectangle fullRect;
        private Rectangle mainRect;
        private Rectangle shadowRect;
        private Bitmap thumbOriginalBitmap;

        public Size BitmapSize = new Size(64, 64);
        public int MaxThumbSize = 200;
        public int PixelSize = 4;

        public ThumbImage(Bitmap bitmap)
        {
            AngleOffset = 0;
            CircleCenter = new Point(0, 0);
            ThumbOriginalBitmap = bitmap;
        }

        public ThumbImage(Bitmap bitmap, Point circleCenter, double angle)
        {
            AngleOffset = 0;
            CircleCenter = circleCenter;
            this.OriginalAngle = angle;
            ThumbOriginalBitmap = bitmap;
        }

        public float AngleOffset { get; set; }

        public Color BackgroundColor { get; set; }

        public Point CircleCenter { get; set; }

        public double DistanceFromScreen { get; set; }
        public Color HoverColor { get; set; }

        public bool IsHover { get; set; }


        public double OriginalAngle { get; set; }

        public Size Radius { get; set; }
        public Bitmap ThumbFullBitmap { get; set; }

        public Bitmap ThumbOriginalBitmap
        {
            get { return thumbOriginalBitmap; }
            set
            {
                thumbOriginalBitmap = value;
                if (thumbOriginalBitmap == null)
                {
                    return;
                }
                var nWidth = MaxThumbSize;
                var nHeight = thumbOriginalBitmap.Height * MaxThumbSize / thumbOriginalBitmap.Width;

                if (nHeight > MaxThumbSize)
                {
                    nHeight = MaxThumbSize;
                    nWidth = thumbOriginalBitmap.Width * MaxThumbSize / thumbOriginalBitmap.Height;
                }
                BitmapSize = new Size(nWidth, nHeight);
                ThumbFullBitmap = new Bitmap(BitmapSize.Width, BitmapSize.Height * 2);
                DrawFullBitmap();
            }
        }


        public bool CheckIsHover(Point currectPoint)
        {
            IsHover = mainRect.Contains(currectPoint);
            return IsHover;
        }

        public void DrawImage(Graphics g)
        {
            DrawImageFullBitmap(g);
            DrawHoverBitmap(g);

        }
        public void ReLocate()
        {
            // angle and distance from screen
            actualAngle = (OriginalAngle + AngleOffset) * PiFact;

            DistanceFromScreen = 10 + 10 * Math.Cos(actualAngle);

            // rectangles
            var x = CircleCenter.X + (int)(Radius.Width * Math.Sin(actualAngle));
            var y = CircleCenter.Y - (int)(Radius.Height * Math.Cos(actualAngle));

            var dSize = (float)(80 - 20 * Math.Cos(actualAngle));

            dSize = dSize / 100;

            mainRect.X = (int)(x - (BitmapSize.Width * dSize) / 2);
            mainRect.Y = (int)(y - (BitmapSize.Height * dSize));
            mainRect.Width = (int)(BitmapSize.Width * dSize);
            mainRect.Height = (int)(BitmapSize.Height * dSize);

            shadowRect.X = (int)(x - (BitmapSize.Width * dSize) / 2);
            shadowRect.Y = (int)y;
            shadowRect.Width = (int)(BitmapSize.Width * dSize);
            shadowRect.Height = (int)(BitmapSize.Height * dSize);

            var left = mainRect.X < shadowRect.X ? mainRect.X : shadowRect.X;
            var top = mainRect.Y < shadowRect.Y ? mainRect.Y : shadowRect.Y;
            var right = mainRect.Right > shadowRect.Right ? mainRect.Right : shadowRect.Right;
            var bottom = mainRect.Bottom > shadowRect.Bottom ? mainRect.Bottom : shadowRect.Bottom;
            fullRect = new Rectangle(left, top, right - left, bottom - top);
        }

        private void DrawFullBitmap()
        {
            using (Graphics g = Graphics.FromImage(ThumbFullBitmap))
            {
                g.DrawImage(thumbOriginalBitmap, 0, 0, BitmapSize.Width, BitmapSize.Height);
                //draw shadow
                g.DrawImage(DrawShadowBitmap(thumbOriginalBitmap), 0, BitmapSize.Height, BitmapSize.Width, BitmapSize.Height);
            }

        }

        private void DrawHoverBitmap(Graphics g)
        {
            if (IsHover)
            {
                var pen = new Pen(HoverColor);
                g.DrawRectangle(pen, mainRect);
                pen.Dispose();
            }
            else
            {
                var dTrasp = (float)(100 + 100 * Math.Cos(actualAngle));
                var sb = new SolidBrush(Color.FromArgb((int)dTrasp, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B));
                g.FillRectangle(sb, mainRect);
                g.FillRectangle(sb, shadowRect);
                sb.Dispose();
            }
        }

        private void DrawImageFullBitmap(Graphics g)
        {
            g.DrawImage(ThumbFullBitmap, fullRect);
        }
        private unsafe Bitmap DrawShadowBitmap(Bitmap bitmap)
        {
            bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            BitmapData bmd = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            byte* row = (byte*)bmd.Scan0;

            for (var y = 0; y < bmd.Height; y++)
            {
                byte trasp = (byte)(100 * ((bitmap.Height - y)) / bitmap.Height);

                int xx = 3;

                for (var x = 0; x < bmd.Width; x++)
                {
                    row[xx] = trasp; //Alpha

                    xx += PixelSize;
                }
                row += bmd.Stride;
            }
            bitmap.UnlockBits(bmd);
            return bitmap;
        }
    }
}