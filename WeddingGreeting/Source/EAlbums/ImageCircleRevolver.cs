using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EAlbums
{
    public class ImageCircleRevolver : IImageCircleRevolver
    {
        public List<ImageCircle> Circles { get; set; }
        public ImageCircleParameter CircleParameter { get; set; }
        public ThumbElement SelectedObject { get; set; }

        public int CircleCount { get; protected set; }
        public int CapacityInCircle { get; protected set; }
        public ImageCircleRevolver()
        {
            Circles = new List<ImageCircle>();
            CircleParameter = new ImageCircleParameter()
            {
                Alpha = 0.0F,
                MaxCircleCapacity = 10,
                MaxCapacityInCircle = 360,
                Radius = new Point(480, 120),
                MaxImageLength = 120,
                DestinationSize = new Size(80, 120),
            };

        }

        public void Load(List<string> filePaths)
        {
            var imageCount = filePaths.Count;
            var thumbElements = new List<ThumbElement>();
            for (var i = 0; i < imageCount; i++)
            {
                thumbElements.Add(new ThumbElement()
                {
                    FullPath = filePaths[i],
                    Name = Path.GetFileNameWithoutExtension(filePaths[i]),
                });
            }
            Load(thumbElements);
        }

        public void Load(List<ThumbElement> thumbElements)
        {
            if (thumbElements == null || !thumbElements.Any()) return;
            Circles.Clear();

            var imageCount = thumbElements.Count;
            CircleCount = (int)Math.Ceiling(imageCount / (CircleParameter.MaxCapacityInCircle * 1d));
            if (CircleCount > CircleParameter.MaxCircleCapacity)
            {
                CircleCount = CircleParameter.MaxCircleCapacity;
            }
            CapacityInCircle = (int)Math.Ceiling(imageCount / (CircleCount * 1d));
            if (CapacityInCircle > CircleParameter.MaxCapacityInCircle)
            {
                CapacityInCircle = CircleParameter.MaxCapacityInCircle;
            }
            var startPoint = new Point(CircleParameter.OrginalCenter.X, CircleParameter.OrginalCenter.Y - (int)(CircleCount - 1) * CircleParameter.CircleVerInterval / 2);

            for (var i = 0; i < CircleCount; i++)
            {
                var center = new Point(startPoint.X, startPoint.Y + i * CircleParameter.CircleVerInterval);
                var circle = new ImageCircle()
                {

                    Index = i,
                    HoverColor = Color.White,
                    SelectedColor = Color.GreenYellow,
                    Perspective = 4,
                    CircleCenter = center,
                    MaxCapacity = CapacityInCircle,
                    RevolveType = RevolveTypes.Fixed,
                    Radius = CircleParameter.Radius,
                    FixedAlphaAccel = CircleParameter.Alpha,
                    BackgroundColor = CircleParameter.BackgroundColor,
                    MaxImageLength = CircleParameter.MaxImageLength,
                    ScalingOption = CircleParameter.ScalingOption,
                    DestinationSize = CircleParameter.DestinationSize,
                };
                Circles.Add(circle);

                circle.Load(thumbElements);
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
        public void SetFixedAlphaAccel(float alphaAccel)
        {
            Parallel.ForEach(Circles, obj =>
            {
                obj.FixedAlphaAccel = alphaAccel;
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
            //foreach (var obj in Circles)
            //{
            //    obj.DrawImages(g);
            //}

            for (int i = Circles.Count() - 1; i >= 0; i--)
            {
                Circles[i].DrawImages(g);
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
        public bool SelectHoverItem(string name)
        {
            SelectedObject = null;
            Parallel.ForEach(Circles, obj =>
            {
                obj.SelectHoverItem(name);
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
            lock (this)
            {
                CircleParameter.OrginalCenter = orginalCenter;
                if (Circles.Any())
                {
                    var startPoint = new Point(CircleParameter.OrginalCenter.X, CircleParameter.OrginalCenter.Y - (int)(CircleCount - 1) * CircleParameter.CircleVerInterval / 2);

                    for (var i = 0; i < Circles.Count; i++)
                    {
                        var center = new Point(startPoint.X, startPoint.Y + i * CircleParameter.CircleVerInterval);
                        Circles[i].CircleCenter = center;
                        Circles[i].Radius = new Point(center.X * 4 / 5, center.Y * 1 / 5);
                    }
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