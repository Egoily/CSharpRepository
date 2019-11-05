using System;

namespace EgoDevil.Utilities.ThumbnailCreator.Exceptions
{
    /// <summary>
    /// Exception gets thrown when the actual draw of the thumbnail image fails
    /// </summary>
    public class ThumbCreationFailedException : Exception
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the ThumbCreationFailedException object
        /// </summary>
        internal ThumbCreationFailedException()
            : base(csMessage)
        {
        }

        /// <summary>
        /// Creates a new instance of the ThumbCreationFailedException object
        /// </summary>
        /// <param name="innerException">
        /// <see cref="System.Exception"/> containing the inner exception that occured
        /// </param>
        internal ThumbCreationFailedException(Exception innerException)
            : base(csMessage, innerException)
        {
        }

        #endregion

        private const string csMessage = "Failed to create thumbnail image (or resized image)";
    }
}