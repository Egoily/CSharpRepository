using System.Collections.Generic;
using System.Drawing;

namespace EAlbums
{
    public interface IImageCircle
    {
        int Index { get; set; }
        float AngleOffset { get; set; }
        float AlphaAccel { get; set; }
        Color BackgroundColor { get; set; }
        Point CircleCenter { get; set; }
        float FixedAlphaAccel { get; set; }
        Color HoverColor { get; set; }
        List<ThumbElement> Images { get; set; }
        int MaxCapacity { get; set; }
        float Perspective { get; set; }
        Point Radius { get; set; }
        RevolveTypes RevolveType { get; set; }
        ThumbElement SelectedObject { get; set; }

        void Clear();

        void DrawImages(Graphics g);

        void Load(List<string> filePaths);

        void ClearHover();

        void Refresh();

        bool SelectHoverItem(Point location);

        void SetAngleOffset();
    }
}