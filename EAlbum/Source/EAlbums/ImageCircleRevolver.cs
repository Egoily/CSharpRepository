using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace EAlbums
{
    public class ImageCircleRevolver : IImageCircleRevolver
    {
        public List<ImageCircle> Circles { get; set; }
        public int CircleCapacity { get; set; }

        public int MaxCapacityInCircle { get; set; }
        public Point Radius { get; set; }
        /// <summary>
        /// the interval of Circle in vertical
        /// </summary>
        public int CircleVerInterval { get; set; }

        public Point OrginalCenter { get; set; }

        public float Alpha { get; set; }

        public Color BackgroundColor { get; set; }
        public ThumbElement SelectedObject { get; set; }

        public int CircleCount { get; protected set; }
        public ImageCircleRevolver()
        {
            Circles = new List<ImageCircle>();
            MaxCapacityInCircle = 64;
            Radius = new Point(400, 400);
        }

        public void Load(List<string> filePaths)
        {
            var imageCount = filePaths.Count;
            CircleCount =(int) Math.Ceiling(imageCount / (MaxCapacityInCircle * 1d));
            if (CircleCount > CircleCapacity)
            {
                CircleCount = CircleCapacity;
            }
            var startPoint = new Point(OrginalCenter.X, OrginalCenter.Y - (int)(CircleCount - 1) * CircleVerInterval / 2);
            for (var i = 0; i < CircleCount; i++)
            {
                var center = new Point(startPoint.X, startPoint.Y + i * CircleVerInterval);
                var circle = new ImageCircle()
                {
                    Index = i,
                    BackgroundColor = BackgroundColor,
                    HoverColor = Color.White,
                    Perspective =4,
                    CircleCenter = center,
                    Radius = Radius,
                    FixedAlphaAccel = 0.1f,
                    MaxCapacity = MaxCapacityInCircle,
                    RevolveType = RevolveTypes.Fixed,
                };
                Circles.Add(circle);

                var paths = new List<string>();
                for (int j = i * circle.MaxCapacity; j < (i + 1) * circle.MaxCapacity; j++)
                {
                    if (j >= filePaths.Count)
                        break;
                    paths.Add(filePaths[j]);
                }
                circle.Load(paths);

            }


        }

        public void SetAngleOffset()
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.SetAngleOffset();
            });
        }

        public void SetAlphaAccel(float alphaAccel)
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.AlphaAccel = alphaAccel;
            });
        }
        public void SetPerspective(float perspective)
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.Perspective = perspective;
            });
        }
        public void Refresh()
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.Refresh();
            });
        }

        public void ClearHover()
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.ClearHover();
            });
        }

        public void DrawImages(Graphics g)
        {
            foreach (var obj in Circles)
            {
                obj.DrawImages(g);
            }
        }

        public bool SelectHoverItem(Point location)
        {
            SelectedObject = null;
            Parallel.ForEach(Circles, obj =>
            {
                obj.SelectHoverItem(location);
            });
            SelectedObject = GetSelectedObject();
            return (SelectedObject != null);

        }

        public void SetRevolveType(RevolveTypes revolveType)
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.RevolveType = revolveType;
            });
        }

        public void SetOrginalCenter(Point orginalCenter)
        {
            this.OrginalCenter = orginalCenter;
            if (Circles.Any())
            {
                var startPoint = new Point(OrginalCenter.X, OrginalCenter.Y - (int)(CircleCount - 1) * CircleVerInterval / 2);
                for (var i = 0; i < CircleCount; i++)
                {
                    var center = new Point(startPoint.X, startPoint.Y + i * CircleVerInterval);
                    Circles[i].CircleCenter = center;
                }
            }

        }

        private ThumbElement GetSelectedObject()
        {
            var circle = Circles.FirstOrDefault(x => x.SelectedObject != null);
            return circle?.SelectedObject;
        }
    }
}
