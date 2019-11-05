using EgoDevil.Utilities.ThumbnailCreator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EAlbums
{
    public class ImageCircle : IImageCircle
    {
        public ImageCircle()
        {
            MaxCapacity = 100;
            AlphaAccel = 0.0f;
            MaxImageLength = 120;
            Images = new List<ThumbElement>();
            DestinationSize = new Size(80, 120);
        }


        public int Index { get; set; }
        public float AngleOffset { get; set; }

        public float AlphaAccel { get; set; }

        public Color BackgroundColor { get; set; }

        public Point CircleCenter { get; set; }

        public float FixedAlphaAccel { get; set; }

        public Color HoverColor { get; set; }
        public Color SelectedColor { get; set; }
        [DefaultValue(120)]
        public int MaxImageLength { get; set; }

        public List<ThumbElement> Images { get; set; }

        [DefaultValue(100)]
        public int MaxCapacity { get; set; }

        public float Perspective { get; set; }
        public Point Radius { get; set; }
        public RevolveTypes RevolveType { get; set; }
        public ThumbElement SelectedObject { get; set; }

        public ScalingOptions ScalingOption { get; set; }
        public Size DestinationSize { get; set; }
        public void Clear()
        {
            Images?.Clear();
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
            var count = filePaths.Count;
            var thumbElements = new List<ThumbElement>();
            for (var i = 0; i < count; i++)
            {
                var filePath = filePaths[i];


                var thumbElement = new ThumbElement()
                {
                    FullPath = filePath,
                    Name = Path.GetFileNameWithoutExtension(filePaths[i]),
                    Description = string.Empty,
                };
                thumbElements.Add(thumbElement);
            }
            Load(thumbElements);
        }
        public void Load(List<ThumbElement> thumbElements)
        {
            if (thumbElements == null || !thumbElements.Any()) return;
            Clear();
            var count = thumbElements.Count;

            if (count > MaxCapacity)
            {
                count = MaxCapacity;
            }
            var thumbnailCreation = new ThumbnailCreation() { MaxImageLength = MaxImageLength };
            for (var i = 0; i < count; i++)
            {
                var item = thumbElements[i];

                Image bitmap = null;
                if (!string.IsNullOrEmpty(item.ImageBase64String))
                {
                    if (Images.Any(x => x.ImageBase64String == item.ImageBase64String))
                        continue;
                    bitmap = thumbnailCreation.CreateFromBase64String(item.ImageBase64String, ScalingOption, DestinationSize);
                }
                else
                {
                    if (Images.Any(x => x.FullPath == item.FullPath))
                        continue;
                    bitmap = thumbnailCreation.CreateThumbnailImage(item.FullPath, ScalingOption, DestinationSize);
                }

                if (bitmap == null)
                    continue;
                var angle = (double)((i * 360.0f) / count);
                var thumbImage = new ThumbImage()
                {
                    Name = item.Name,
                    ThumbOriginalBitmap = new Bitmap(bitmap),
                    OriginalAngle = angle,
                    CircleCenter = CircleCenter,
                    HoverColor = HoverColor,
                    SelectedColor = SelectedColor,
                    IsSelected = item.IsSelected,
                };
                bitmap.Dispose();
                var thumbElement = new ThumbElement()
                {
                    FullPath = item.FullPath,
                    Name = item.Name,
                    Description = item.Description,
                    ThumbImage = thumbImage,
                    IsSelected = item.IsSelected,
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
        public bool SelectHoverItem(string name)
        {
            SelectedObject = null;
            Parallel.ForEach(Images.Where(obj => obj.Name == name), obj =>
              {
                  obj.ThumbImage.IsSelected = true;
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