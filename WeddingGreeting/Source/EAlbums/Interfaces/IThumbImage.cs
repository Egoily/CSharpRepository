using System.Drawing;

namespace EAlbums
{
    public interface IThumbImage
    {
        float AngleOffset { get; set; }
        Color BackgroundColor { get; set; }
        Point CircleCenter { get; set; }
        double DistanceFromScreen { get; set; }
        Color HoverColor { get; set; }
        bool IsHover { get; set; }
        double OriginalAngle { get; set; }
        Size Radius { get; set; }
        Bitmap ThumbFullBitmap { get; set; }
        Bitmap ThumbOriginalBitmap { get; set; }

        bool CheckIsHover(Point currectPoint);

        void DrawImage(Graphics g);

        void ReLocate();
    }
}