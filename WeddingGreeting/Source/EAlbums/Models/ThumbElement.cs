namespace EAlbums
{
    public class ThumbElement
    {
        public virtual string Name { get; set; }
        public virtual string FullPath { get; set; }
        public virtual string ImageBase64String { get; set; }
        public virtual string Description { get; set; }

        public virtual bool IsSelected { get; set; }
        public virtual ThumbImage ThumbImage { get; set; }
    }
}