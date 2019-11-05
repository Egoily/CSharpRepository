using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using EgoDevil.Utilities.ThumbnailCreator;
using System.Diagnostics;

namespace EAlbums
{

    public class ImageCircle : IImageCircle
    {
        public ImageCircle()
        {
            MaxCapacity = 10;
            AlphaAccel = 0.0f;
            Images = new List<ThumbElement>();
        }

        public ImageCircle(Point circleCenter)
        {
            MaxCapacity = 10;
            AlphaAccel = 0.0f;
            Images = new List<ThumbElement>();
            CircleCenter = circleCenter;
        }

        public int Index { get; set; }
        public float AngleOffset { get; set; }

        public float AlphaAccel { get; set; }

        public Color BackgroundColor { get; set; }

        public Point CircleCenter { get; set; }

        public float FixedAlphaAccel { get; set; }

        public Color HoverColor { get; set; }

        public List<ThumbElement> Images { get; set; }

        [DefaultValue(10)]
        public int MaxCapacity { get; set; }

        public float Perspective { get; set; }
        public Point Radius { get; set; }
        public RevolveTypes RevolveType { get; set; }
        public ThumbElement SelectedObject { get; set; }

        public void Clear()
        {
            if (Images != null)
            {
                Images.Clear();
            }

        }

        public void DrawImages(Graphics g)
        {
            foreach (var obj in Images)
            {
                obj.ThumbImage.DrawImage(g);
            }
        }

        public void Load(List<string> filePaths)
        {
            Clear();
            var count = filePaths.Count;

            if (count > MaxCapacity)
            {
                count = MaxCapacity;
            }
            var thumbnailCreation = new ThumbnailCreation() { MaxImageLength = 400 };
            for (var i = 0; i < count; i++)
            {
                var filePath = filePaths[i];


                var bitmap = thumbnailCreation.CreateThumbnailImage(filePath);

                Debug.WriteLine($"({bitmap.Width},{bitmap.Height})");

                var angle = (double)((i * 360.0f) / count);
                var thumbImage = new ThumbImage(new Bitmap(bitmap), CircleCenter, angle)
                {
                    HoverColor = HoverColor
                };
                bitmap.Dispose();
                var thumbElement = new ThumbElement()
                {
                    FullPath = filePath,
                    Name = Path.GetFileNameWithoutExtension(filePaths[i]),
                    Description = string.Empty,
                    ThumbImage = thumbImage,
                };
                Images.Add(thumbElement);
            }
        }

        public void ClearHover()
        {
            foreach (var item in Images.Where(x => x.ThumbImage.IsHover))
            {
                item.ThumbImage.IsHover = false;
            }
        }
        public void Refresh()
        {
            Parallel.ForEach(Images, obj =>
            {
                obj.ThumbImage.AngleOffset = AngleOffset;
                obj.ThumbImage.Radius = new Size(Radius.X, (int)(Radius.Y / (Perspective == 0 ? 1 : Perspective)));
                obj.ThumbImage.CircleCenter = CircleCenter;
                obj.ThumbImage.ReLocate();
            });

            Images.Sort((p1, p2) => p2.ThumbImage.DistanceFromScreen.CompareTo(p1.ThumbImage.DistanceFromScreen));

        }

        public bool SelectHoverItem(Point location)
        {
            SelectedObject = null;
            Parallel.ForEach(Images.Where(obj => obj.ThumbImage.CheckIsHover(location)), obj =>
            {
                SelectedObject = obj;
            });
            return (SelectedObject != null);

        }

        public void SetAngleOffset()
        {
            AngleOffset += GetAlphaAccel();

            if (AngleOffset > 360)
            {
                AngleOffset -= 360;
            }

            if (AngleOffset < 0)
            {
                AngleOffset += 360;
            }
        }

        private float GetAlphaAccel()
        {
            var result = 0.0f;
            switch (RevolveType)
            {
                case RevolveTypes.None:
                    result = 0;
                    break;
                case RevolveTypes.Fixed:
                    result = FixedAlphaAccel;
                    break;
                case RevolveTypes.Transformable:
                    result = AlphaAccel;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }

        private void InitOriginalAngle()
        {
            for (var i = 0; i < Images.Count; i++)
            {
                Images[i].ThumbImage.OriginalAngle = (float)i * (360.0f) / (float)Images.Count;
            }

        }
    }
}
