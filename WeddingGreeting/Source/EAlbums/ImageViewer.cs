using EgoDevil.Utilities.ThumbnailCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EAlbums
{
    public partial class ImageViewer : UserControl
    {
        public List<string> ImagePaths = new List<string>();



        private Image currentDisplayImage;

        private string currentDisplayImageFullPath = string.Empty;

        private Point currentLocation;

        private Rectangle desRect;



        //the scale by which the current image is zoomed
        private double scale = 1;

        private Rectangle srcRect;


        private IImageCircleRevolver Revolver { get; set; }
        [Browsable(true), Category("Custom")]
        public Color BackgroundColor { get; set; }
        [Browsable(true), Category("Custom")]
        /// <summary>
        /// gets or sets how the mouse behaves on the image control display area
        /// </summary>
        public PreviewMode ImagePreviewMode { get; set; }
        [Browsable(true), Category("Custom")]
        public ViewPatterns Pattern { get; set; }
        [Browsable(true), Category("Custom")]
        public string SourceFolder { get; set; }
        [Browsable(true), Category("Custom")]
        public int CircleCapacity
        {
            get => Revolver.CircleParameter.MaxCircleCapacity;
            set => Revolver.CircleParameter.MaxCircleCapacity = value;
        }
        [Browsable(true), Category("Custom")]
        public int MaxCapacityInCircle
        {
            get => Revolver.CircleParameter.MaxCapacityInCircle;
            set => Revolver.CircleParameter.MaxCapacityInCircle = value;
        }
        [Browsable(true), Category("Custom")]
        public int CircleVerInterval
        {
            get => Revolver.CircleParameter.CircleVerInterval;
            set => Revolver.CircleParameter.CircleVerInterval = value;
        }
        [Browsable(true), Category("Custom")]
        public int MaxImageLength
        {
            get => Revolver.CircleParameter.MaxImageLength;
            set => Revolver.CircleParameter.MaxImageLength = value;
        }
        [Browsable(true), Category("Custom")]
        public ScalingOptions ScalingOption
        {
            get => Revolver.CircleParameter.ScalingOption;
            set => Revolver.CircleParameter.ScalingOption = value;
        }
        [Browsable(true), Category("Custom")]
        public Size DestinationSize
        {
            get => Revolver.CircleParameter.DestinationSize;
            set => Revolver.CircleParameter.DestinationSize = value;
        }

        [Browsable(true), Category("Custom")]
        [DefaultValue(0.0F)]
        public float Alpha
        {
            get => Revolver.CircleParameter.Alpha;
            set
            {
                Revolver.CircleParameter.Alpha = value;
            }
        }
        //this is the coordinate of the center of the image displayed on the control in reference to
        //the coordinate system of the original image
        [Browsable(true), Category("Custom")]
        public Point DisplayCenterOffset { get; set; }
        [Browsable(true), Category("Custom")]
        public bool ShownTitle { get; set; }
        public ImageViewer()
        {
            InitializeComponent();

            this.SetStyle(
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.DoubleBuffer, true);
            this.MouseWheel += ImageViewer_MouseWheel;

            timer.Enabled = false;
            Pattern = ViewPatterns.Pending;

            BackgroundColor = Color.FromArgb(255, 0, 0, 0);


            Revolver = new ImageCircleRevolver()
            {
            };
            Revolver.CircleParameter.OrginalCenter = new Point(Width / 2, Height / 2);

        }


        public void Loading()
        {

            this.backgroundWorker.RunWorkerAsync();

            SetTimerEnabled(true);
        }



        public void LoadThumbs()
        {
            if (!string.IsNullOrEmpty(SourceFolder))
            {
                var dir = Path.Combine(Application.StartupPath, SourceFolder);
                LoadThumbs(dir);
            }

        }

        public void LoadingByThumbElements(List<ThumbElement> thumbElements)
        {
            Revolver.Load(thumbElements);
        }

        public void LoadThumbs(string dir)
        {
            if (!Directory.Exists(dir))
            {
                return;
            }
            var filePaths = Directory.GetFiles(dir);
            LoadThumbs(filePaths.ToList());
        }

        public void LoadThumbs(List<string> filePaths)
        {
            Revolver.Load(filePaths);
        }

        /// <summary>
        /// this method rotated the image
        /// </summary>
        /// <param name="clockWise">specifies if its clockwise or the other</param>
        public void RotateImage(bool clockWise)
        {
            if (currentDisplayImage != null)
            {
                currentDisplayImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
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
            Point center = new Point(this.Width / 2, this.Height / 2);
            int width = currentDisplayImage.Width;
            int height = currentDisplayImage.Height;
            switch (zoomMode)
            {
                case ZoomMode.ActualSize:
                    amount = 1;
                    break;

                case ZoomMode.FitHeight:
                    amount = (double)this.Height / height;
                    break;

                case ZoomMode.FitPage:
                    amount = (double)this.Height / height;
                    if ((double)this.Width / width < amount)
                        amount = (double)this.Width / width;
                    break;

                case ZoomMode.FitWidth:
                    amount = (double)this.Width / width;
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
            int width = this.Width;
            int height = this.Height;
            ZoomImage(amount, new Point(width / 2, height / 2));
        }

        /// <summary>
        /// this method draws the image with the zoom amount as well as the center of the image specified
        /// </summary>
        /// <param name="amount">the value by how much the image will be zoomed</param>
        /// <param name="center">the center coordinate of the image in reference to the original coordinate system</param>
        public void ZoomImage(double amount, Point center)
        {
            srcRect = new Rectangle(0, 0, currentDisplayImage.Width, currentDisplayImage.Height);
            desRect = new Rectangle(
                          center.X - (int)(currentDisplayImage.Width * amount / 2) - DisplayCenterOffset.X,
                          center.Y - (int)(currentDisplayImage.Height * amount / 2) - DisplayCenterOffset.Y,
                          (int)(currentDisplayImage.Width * amount),
                          (int)(currentDisplayImage.Height * amount));

            Refresh();
        }

        private static Rectangle GetImageZoomRect(Size srcSize, Size desSize)
        {
            int width;
            int height;
            if (srcSize.Width <= desSize.Width && srcSize.Height <= desSize.Height)
            {
                width = srcSize.Width;
                height = srcSize.Height;
            }
            else if (srcSize.Width <= desSize.Width && srcSize.Height > desSize.Height)
            {
                height = desSize.Height;
                width = srcSize.Width * height / srcSize.Height;
            }
            else if (srcSize.Width > desSize.Width && srcSize.Height <= desSize.Height)
            {
                width = desSize.Width;
                height = srcSize.Height * width / srcSize.Width;
            }
            else
            {
                width = desSize.Width;
                height = srcSize.Height * width / srcSize.Width;
                if (height > desSize.Height)
                {
                    height = desSize.Height;
                    width = srcSize.Width * height / srcSize.Height;
                }
            }
            return new Rectangle(x: (desSize.Width - width) / 2, y: (desSize.Height - height) / 2, width: width, height: height);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Pattern = ViewPatterns.Loading;
            LoadThumbs();
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Pattern = ViewPatterns.Ready;
        }


        private void ImageViewer_Load(object sender, EventArgs e)
        {
        }

        private Point tempMouseDownPoint;

        private bool isFirstShown = false;

        public void ShowSelectedItem()
        {
            if (Revolver.SelectedObject != null)
            {
                isFirstShown = true;
                SetTimerEnabled(false);

                if (!string.IsNullOrEmpty(Revolver.SelectedObject.FullPath)
                    && currentDisplayImageFullPath != Revolver.SelectedObject.FullPath)
                {
                    currentDisplayImageFullPath = Revolver.SelectedObject.FullPath;
                    currentDisplayImage = Image.FromFile(Revolver.SelectedObject.FullPath);
                }
                else if (!string.IsNullOrEmpty(Revolver.SelectedObject.ImageBase64String))
                {
                    byte[] bytes = Convert.FromBase64String((Revolver.SelectedObject.ImageBase64String));
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        currentDisplayImage = new System.Drawing.Bitmap(ms);
                    }

                }
                ZoomImage();

                Pattern = ViewPatterns.Browse;
            }
        }

        public bool ShowItem(string name)
        {
            var isHover = Revolver.SelectHoverItem(name);
            if (isHover)
            {
                Revolver.SetRevolveType(RevolveTypes.None);
                ShowSelectedItem();
            }
            else
            {
                Revolver.SetRevolveType(RevolveTypes.Fixed);
                SetTimerEnabled(true);
                Pattern = ViewPatterns.Ready;
                Invalidate();
            }
            return isHover;
        }
        public bool Select(string name)
        {
            var isHover = Revolver.SelectHoverItem(name);
            if (isHover)
            {
                Revolver.SetRevolveType(RevolveTypes.None);
            }
            return isHover;
        }
        public void Select(Point point)
        {
            var isHover = Revolver.SelectHoverItem(point);
            if (isHover)
            {
                Cursor = Cursors.Hand;
                Revolver.SetRevolveType(RevolveTypes.None);
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }
        private void ImageViewer_MouseDown(object sender, MouseEventArgs e)
        {
            switch (Pattern)
            {
                case ViewPatterns.Pending:
                    break;

                case ViewPatterns.Loading:
                    break;

                case ViewPatterns.Ready:
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        ShowSelectedItem();
                    }
                    break;

                case ViewPatterns.Browse:
                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        tempMouseDownPoint = e.Location;
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ImageViewer_MouseUp(object sender, MouseEventArgs e)
        {
            switch (Pattern)
            {
                case ViewPatterns.Pending:
                    break;

                case ViewPatterns.Loading:
                    break;

                case ViewPatterns.Ready:

                    break;

                case ViewPatterns.Browse:
                    if (this.Cursor != Cursors.Default)
                    {
                        this.Cursor = Cursors.Default;
                    }
                    Refresh();

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ImageViewer_MouseMove(object sender, MouseEventArgs e)
        {
            switch (Pattern)
            {
                case ViewPatterns.Pending:
                    break;

                case ViewPatterns.Loading:
                    break;

                case ViewPatterns.Ready:
                    {
                        Revolver.SetRevolveType(RevolveTypes.Fixed);
                        var x0 = Width / 2;
                        var y0 = Height / 2;
                        //设置加速步长
                        Revolver.SetAlphaAccel((((float)(e.X - x0)) * 3.0f) / ((float)x0));

                        if ((e.Button & MouseButtons.Right) == MouseButtons.Right)
                        {
                            Revolver.SetPerspective(12 - (((float)(-e.Y + y0)) * 10.0f) / ((float)y0));
                        }
                        else if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                        {
                            Revolver.SetRevolveType(RevolveTypes.Transformable);
                        }
                        Select(e.Location);

                    }
                    break;

                case ViewPatterns.Browse:
                    this.currentLocation = e.Location;

                    if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
                    {
                        this.Cursor = Cursors.Hand;
                        if (isFirstShown)
                        {
                            isFirstShown = false;
                        }
                        else
                        {
                            desRect.Offset(new Point(e.X - tempMouseDownPoint.X, e.Y - tempMouseDownPoint.Y));
                            tempMouseDownPoint = e.Location;
                            Refresh();
                        }
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ImageViewer_MouseWheel(object sender, MouseEventArgs e)
        {
            switch (Pattern)
            {
                case ViewPatterns.Pending:
                    break;

                case ViewPatterns.Loading:
                    break;

                case ViewPatterns.Ready:

                    break;

                case ViewPatterns.Browse:
                    if (e.Delta > 0)
                    {
                        this.scale = this.scale + 0.1;
                        if (this.scale > 10)
                        {
                            this.scale = 10;
                        }
                    }
                    else
                    {
                        this.scale = this.scale - 0.1;
                        if (this.scale < 0.1)
                        {
                            this.scale = 0.1;
                        }
                    }
                    //ZoomImage(this.scale, this.displayCenter);
                    ZoomImage(this.scale);

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void ImageViewer_DoubleClick(object sender, EventArgs e)
        {
            switch (Pattern)
            {
                case ViewPatterns.Pending:
                    break;

                case ViewPatterns.Loading:
                    break;

                case ViewPatterns.Ready:
                    break;

                case ViewPatterns.Browse:
                    SetTimerEnabled(true);
                    Pattern = ViewPatterns.Ready;
                    Invalidate();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ImageViewer_Paint(object sender, PaintEventArgs e)
        {
            PaintAlbumView(e.Graphics);
        }

        private void PaintAlbumView(Graphics g)
        {
            //draw background
            g.FillRectangle(new SolidBrush(BackgroundColor), 0, 0, Width, Height);
            switch (Pattern)
            {
                case ViewPatterns.Pending:
                    PaintPendingPattern(g);
                    break;

                case ViewPatterns.Loading:
                    PaintLoadingPattern(g);
                    break;

                case ViewPatterns.Ready:
                    PaintReadyPattern(g);
                    break;

                case ViewPatterns.Browse:
                    PaintBrowsePattern(g);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void PaintBrowsePattern(Graphics g)
        {
            if (Revolver.SelectedObject != null)
            {
                var units = GraphicsUnit.Pixel;

                g.DrawImage(currentDisplayImage, desRect, srcRect, units);

                Revolver.ClearHover();
                FontFamily fontFamily = new FontFamily("Arial");
                Font font = new Font(fontFamily, 36, FontStyle.Regular, GraphicsUnit.Pixel);
                if (ShownTitle)
                {

                    g.DrawString(Revolver.SelectedObject.Name, font, Brushes.Red, new PointF(0, 0));
                }
                //显示信息
                var message = Revolver.SelectedObject.Description;

                if (!string.IsNullOrEmpty(message))
                {
                    g.DrawString(message, font, Brushes.BlueViolet,
                        new PointF(Width * 3 / 4, Height / 3));
                }
            }
        }

        private void PaintLoadingPattern(Graphics g)
        {
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(fontFamily, 36, FontStyle.Regular, GraphicsUnit.Pixel);
            g.DrawString("Loading...", font, Brushes.Red, new PointF(0, 0));
        }

        private void PaintPendingPattern(Graphics g)
        {
        }

        private void PaintReadyPattern(Graphics g)
        {
            Revolver.SetOrginalCenter(new Point(Width / 2, Height / 2));
            Revolver.DrawImages(g);
        }

        private void RefreshImages()
        {
            if (Pattern == ViewPatterns.Ready)
            {
                Revolver.SetAngleOffset();
                Revolver.Refresh();
                Invalidate();
            }
        }

        private bool SetTimerEnabled(bool enabled)
        {
            if (enabled)
            {
                if (!timer.Enabled)
                    timer.Enabled = true;
            }
            else
            {
                if (timer.Enabled)
                    timer.Enabled = false;
            }
            return timer.Enabled;
        }

        private void TimerTick(object sender, EventArgs e)
        {
            RefreshImages();
        }

        public void ResetOrginalCenter()
        {
            Revolver.SetOrginalCenter(new Point(Width / 2, Height / 2));
            Revolver.Refresh();
        }

        public void SetAlphaAccel(float value)
        {
            Revolver.SetFixedAlphaAccel(value);
            Revolver.Refresh();
        }
    }
}