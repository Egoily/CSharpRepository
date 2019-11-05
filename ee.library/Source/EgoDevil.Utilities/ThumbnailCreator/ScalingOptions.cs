namespace EgoDevil.Utilities.ThumbnailCreator
{
    /// <summary>
    /// This enum is used to let you decide which way to scale the image into a thumbnail.
    /// </summary>
    public enum ScalingOptions
    {
        /// <summary>
        /// Maintains the aspect ratio (default)
        /// </summary>
        MaintainAspect,

        /// <summary>
        /// Picks a small piece of the image's center and scale that to a thumbnail
        /// </summary>
        CenterImage,

        /// <summary>
        /// Create a fixed size thumbnail, scale the original image to the given size
        /// </summary>
        FixedSize
    }
}