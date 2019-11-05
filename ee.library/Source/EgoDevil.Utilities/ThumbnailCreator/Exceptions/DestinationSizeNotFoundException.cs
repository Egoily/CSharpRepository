using System;

namespace EgoDevil.Utilities.ThumbnailCreator.Exceptions
{

    /// <summary>
    /// This exception will be thrown when the system could not calculate the destination size
    /// of the image that is about to be produced. This destination size depends on the ScalingOption
    /// and may depend on the original image.
    /// </summary>
    public class DestinationSizeNotFoundException : Exception
    {

        internal DestinationSizeNotFoundException()
            : base(csMessage)
        {
        }

        private const string csMessage = "Failed to determine the destination size of the image";
    }
}
