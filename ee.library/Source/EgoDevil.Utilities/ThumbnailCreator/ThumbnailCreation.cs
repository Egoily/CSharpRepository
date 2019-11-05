using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using EgoDevil.Utilities.ThumbnailCreator.Exceptions;

namespace EgoDevil.Utilities.ThumbnailCreator
{
    /// <summary>
    /// Complete class which generates thumbnails in every way you want!
    /// </summary>
    public class ThumbnailCreation : IDisposable
    {
        #region [ Constructors ]

        /// <summary>
        /// Create an instance of the ThumbnailCreation object
        /// </summary>
        public ThumbnailCreation()
        {
            // Constructor of your class...
        }

        #endregion

        #region [ Variable Declarations ]

        private bool _IsDisposed = false;
        private ScalingOptions m_ScalingOption = ScalingOptions.MaintainAspect;
        private Size m_DestinationSize = Size.Empty;
        private int m_MaxLength = 120;
        private FileFormats m_FileFormat = FileFormats.JPEG;
        private long m_Quality = 100;

        #endregion

        #region [ Property Assignments ]

        /// <summary>
        /// Gets the thumbnail scaling option. In order to set the scaling option, use the
        /// SetScalingOption method.
        /// </summary>
        [DefaultValue(ScalingOptions.MaintainAspect)]
        [Description("Gets or sets the way to scale the image to thumbnail size")]
        public ScalingOptions ScalingOption
        {
            get { return m_ScalingOption; }
        }

        /// <summary>
        /// Gets or sets the maximum image length of the thumbnail
        /// </summary>
        [DefaultValue(120)]
        [Description("Gets or sets the maximum image length for the thumbnail")]
        public int MaxImageLength
        {
            get { return m_MaxLength; }
            set
            {
                if (value > 0)
                    m_MaxLength = value;
                else
                    throw new ArgumentOutOfRangeException("MaxImageLength", value, "The MaxImageLength value must be greater then zero");
            }
        }

        /// <summary>
        /// Gets or sets the file format. When the image is saved, this file format will be used
        /// </summary>
        [DefaultValue(FileFormats.JPEG)]
        [Description("Gets or sets the file format. When the image is saved, this file format will be used")]
        public FileFormats FileFormat
        {
            get { return m_FileFormat; }
            set { m_FileFormat = value; }
        }

        /// <summary>
        /// Gets or sets the image quality. Lowest quality is 0, highest is 100
        /// </summary>
        [Description("Gets or sets the image quality. Lowest quality is 0, highest is 100")]
        public long Quality
        {
            get { return m_Quality; }
            set
            {
                if ((value >= 0) && (value <= 100))
                    m_Quality = value;
                else
                    throw new ArgumentOutOfRangeException("Quality", value, "The Quality value must be greater then or equal to zero, and less then or equal to 100");
            }
        }

        /// <summary>
        /// Gets wether the object's Dispose() method was called. If the object is disposed all
        /// resources are destroyed.
        /// </summary>
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        #endregion

        #region [ Functions & Procedures ]

        #region [ Public's ]

        /// <summary>
        /// Sets the scaling option to the option passed using the Option argument
        /// </summary>
        /// <param name="Option">
        /// <see cref="HexMaster.Helper.ScalingOptions"/> containing the scaling option to set
        /// </param>
        public void SetScalingOption(ScalingOptions Option)
        {
            SetScalingOption(Option, Size.Empty);
        }

        /// <summary>
        /// Sets the scaling option to the option passed using the Option argument
        /// </summary>
        /// <param name="Option">
        /// <see cref="HexMaster.Helper.ScalingOptions"/> containing the scaling option to set
        /// </param>
        /// <param name="DestinationSize">
        /// <see cref="System.Drawing.Size"/> containing the size of the detination thumbnail
        /// </param>
        /// <remarks>
        /// The DestinationSize argument is only used when the scaling option <see
        /// cref="HexMaster.Helper.ScalingOptions.FixedSize"/> is passed.
        /// </remarks>
        /// <exception cref="System.ArgumentException">
        /// gets thrown when the Option argument was set to ScalingOptions.FixedSize, and no
        /// Destination size was passed
        /// </exception>
        public void SetScalingOption(ScalingOptions Option, Size DestinationSize)
        {
            if ((Option == ScalingOptions.FixedSize) &&
                ((DestinationSize == null) || (DestinationSize == Size.Empty)))
                throw new ArgumentException("If you set the ScalingOption to ScalingOptions.FixedSize, you need to provide a valid destination size", "DestinationSize");

            if (Option == ScalingOptions.FixedSize)
                m_DestinationSize = DestinationSize;

            m_ScalingOption = Option;
        }

        /// <summary>
        /// Creates a thumbnail image
        /// </summary>
        /// <param name="Source"><see cref="System.Drawing.Bitmap"/> containing te source image</param>
        /// <param name="Option">
        /// <see cref="HexMaster.Helper.ScalingOptions"/> containing the scaling options flag
        /// </param>
        /// <param name="DestinationSize">
        /// <see cref="System.Drawing.Size"/> containing the destination size instructions
        /// </param>
        /// <returns>
        /// <see cref="System.Drawing.Image"/> containing the result of the thumbnail creation
        /// </returns>
        public Image CreateThumbnailImage(Bitmap Source, ScalingOptions Option, Size DestinationSize)
        {
            SetScalingOption(Option, DestinationSize);
            return CreateThumbnail(Source);
        }

        /// <summary>
        /// Creates a thumbnail image
        /// </summary>
        /// <param name="Source"><see cref="System.Drawing.Bitmap"/> containing te source image</param>
        /// <param name="Option">
        /// <see cref="HexMaster.Helper.ScalingOptions"/> containing the scaling options flag
        /// </param>
        /// <returns>
        /// <see cref="System.Drawing.Image"/> containing the result of the thumbnail creation
        /// </returns>
        public Image CreateThumbnailImage(Bitmap Source, ScalingOptions Option)
        {
            return CreateThumbnailImage(Source, Option, m_DestinationSize);
        }

        /// <summary>
        /// Creates a thumbnail image
        /// </summary>
        /// <param name="Source"><see cref="System.Drawing.Bitmap"/> containing te source image</param>
        /// <returns>
        /// <see cref="System.Drawing.Image"/> containing the result of the thumbnail creation
        /// </returns>
        public Image CreateThumbnailImage(Bitmap Source)
        {
            return CreateThumbnailImage(Source, m_ScalingOption, m_DestinationSize);
        }

        /// <summary>
        /// Creates a thumbnail image
        /// </summary>
        /// <param name="SourceFile">
        /// <see cref="System.String"/> containing the full file path to the source image
        /// </param>
        /// <param name="Option">
        /// <see cref="HexMaster.Helper.ScalingOptions"/> containing the scaling options flag
        /// </param>
        /// <param name="DestinationSize">
        /// <see cref="System.Drawing.Size"/> containing the destination size instructions
        /// </param>
        /// <returns>
        /// <see cref="System.Drawing.Image"/> containing the result of the thumbnail creation
        /// </returns>
        public Image CreateThumbnailImage(string SourceFile, ScalingOptions Option, Size DestinationSize)
        {
            Image imgThumbnail = null;
            if (File.Exists(SourceFile))
            {
                using (Bitmap bmpSource = new Bitmap(SourceFile))
                    imgThumbnail = CreateThumbnailImage(bmpSource, Option, DestinationSize);
            }
            return imgThumbnail;
        }

        /// <summary>
        /// Creates a thumbnail image
        /// </summary>
        /// <param name="SourceFile">
        /// <see cref="System.String"/> containing the full file path to the source image
        /// </param>
        /// <param name="Option">
        /// <see cref="HexMaster.Helper.ScalingOptions"/> containing the scaling options flag
        /// </param>
        /// <returns>
        /// <see cref="System.Drawing.Image"/> containing the result of the thumbnail creation
        /// </returns>
        public Image CreateThumbnailImage(string SourceFile, ScalingOptions Option)
        {
            return CreateThumbnailImage(SourceFile, Option, m_DestinationSize);
        }

        /// <summary>
        /// Creates a thumbnail image
        /// </summary>
        /// <param name="SourceFile">
        /// <see cref="System.String"/> containing the full file path to the source image
        /// </param>
        /// <returns>
        /// <see cref="System.Drawing.Image"/> containing the result of the thumbnail creation
        /// </returns>
        public Image CreateThumbnailImage(string SourceFile)
        {
            return CreateThumbnailImage(SourceFile, m_ScalingOption, m_DestinationSize);
        }


        public Image CreateFromBase64String(string ImageBase64String, ScalingOptions Option, Size DestinationSize)
        {
            Image imgThumbnail = null;
            if (!string.IsNullOrEmpty(ImageBase64String))
            {

                byte[] bytes = Convert.FromBase64String(ImageBase64String);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    using (Bitmap bmpSource = new System.Drawing.Bitmap(ms))
                    {
                        imgThumbnail = CreateThumbnailImage(bmpSource, Option, DestinationSize);
                    }
                }
            }
            return imgThumbnail;
        }
        public Image CreateFromBase64String(string ImageBase64String, ScalingOptions Option)
        {
            return CreateFromBase64String(ImageBase64String, Option, m_DestinationSize);
        }
        public Image CreateFromBase64String(string ImageBase64String)
        {
            return CreateFromBase64String(ImageBase64String, m_ScalingOption, m_DestinationSize);
        }

        /// <summary>
        /// Creates a thumbnail of the source image and stores the result in the destination file.
        /// </summary>
        /// <param name="SourceFile">
        /// <see cref="System.String"/> containing the full file path to the source image
        /// </param>
        /// <param name="DestinationFile">
        /// <see cref="System.String"/> containing the full file path to the destination image
        /// </param>
        /// <exception cref="System.Exception">Thrown when the destination file already exists</exception>
        public void StoreThumbnail(string SourceFile, string DestinationFile)
        {
            StoreThumbnail(SourceFile, DestinationFile, false);
        }

        /// <summary>
        /// Creates a thumbnail of the source image and stores the result in the destination file.
        /// </summary>
        /// <param name="SourceFile">
        /// <see cref="System.String"/> containing the full file path to the source image
        /// </param>
        /// <param name="DestinationFile">
        /// <see cref="System.String"/> containing the full file path to the destination image
        /// </param>
        /// <param name="Overwrite">
        /// <see cref="System.Boolean"/> containing wether to overwrite the destination image if the
        /// file already exists
        /// </param>
        /// <exception cref="System.Exception">
        /// Thrown when the Overwrite flag was set to false, but the destination file does exist
        /// </exception>
        public void StoreThumbnail(string SourceFile, string DestinationFile, bool Overwrite)
        {
            if (File.Exists(DestinationFile))
            {
                if (Overwrite)
                    File.Delete(DestinationFile);
                else
                    throw new Exception("The file '" + DestinationFile + "' already exists!");
            }

            Image imgThumbnail = CreateThumbnailImage(SourceFile, m_ScalingOption, m_DestinationSize);
            WriteImageToDisc(imgThumbnail, DestinationFile);
        }

        #endregion

        #region [ Private's ]

        private Image CreateThumbnail(Bitmap Source)
        {
            Bitmap bmpDestination = null;

            if (Source == null)
                throw new NullReferenceException("The source image is not an instance of an object");

            Rectangle rectDestination = DetermineDestinationRectangle(Source);
            if ((m_DestinationSize == null) || (m_DestinationSize == Size.Empty))
                throw new DestinationSizeNotFoundException();

            try
            {
                bmpDestination = new Bitmap(m_DestinationSize.Width, m_DestinationSize.Height);
                using (Graphics grphDestination = Graphics.FromImage(bmpDestination))
                {
                    Rectangle rectThumbnail = DetermineDestinationRectangle(Source);
                    Rectangle rectSource = DetermineSourceRectangle(Source);

                    grphDestination.DrawImage(Source, rectDestination, rectSource, GraphicsUnit.Pixel);
                }
            }
            catch (Exception ex)
            {
                throw new ThumbCreationFailedException(ex);
            }
            return bmpDestination;
        }

        private Rectangle DetermineSourceRectangle(Bitmap Source)
        {
            Rectangle rectSource = Rectangle.Empty;
            switch (m_ScalingOption)
            {
                case ScalingOptions.CenterImage:
                    int iWidth = Source.Width / 2;
                    int iHeight = Source.Height / 2;
                    int iLeft = iWidth / 2;
                    int iTop = iHeight / 2;
                    rectSource = new Rectangle(iLeft, iTop, iWidth, iHeight);
                    break;

                default:
                    rectSource = new Rectangle(0, 0, Source.Width, Source.Height);
                    break;
            }
            return rectSource;
        }

        private Rectangle DetermineDestinationRectangle(Bitmap Source)
        {
            Rectangle rectDestination = Rectangle.Empty;
            switch (m_ScalingOption)
            {
                case ScalingOptions.MaintainAspect:
                case ScalingOptions.CenterImage:
                    DetermineSizeByRatio(Source);
                    rectDestination = new Rectangle(0, 0, m_DestinationSize.Width, m_DestinationSize.Height);
                    break;

                case ScalingOptions.FixedSize:
                    if ((m_DestinationSize == null) || (m_DestinationSize == Size.Empty))
                        throw new ArgumentException("If you set the ScalingOption to ScalingOptions.FixedSize, you need to provide a valid destination size", "DestinationSize");
                    rectDestination = new Rectangle(0, 0, m_DestinationSize.Width, m_DestinationSize.Height);
                    break;
            }
            return rectDestination;
        }

        private void DetermineSizeByRatio(Bitmap Source)
        {
            // This method will calculate the thumnail's image size depending on the original
            // image's size.

            decimal dcRectangle = (decimal)Source.Width / (decimal)Source.Height;
            int iWidth = m_MaxLength;
            int iHeight = m_MaxLength;

            if (Source.Width >= Source.Height)
            {
                iWidth = m_MaxLength;
                iHeight = int.Parse(decimal.Round((decimal)iWidth / dcRectangle, 0).ToString());
            }
            else
            {
                iHeight = m_MaxLength;
                iWidth = int.Parse(decimal.Round((decimal)iHeight * dcRectangle, 0).ToString());
            }
            m_DestinationSize = new Size(iWidth, iHeight);
        }

        private void WriteImageToDisc(Image Image, string Destination)
        {
            ImageCodecInfo myImageCodecInfo;
            Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;

            // Get an ImageCodecInfo object that represents the JPEG codec.
            switch (m_FileFormat)
            {
                case FileFormats.PNG:
                    myImageCodecInfo = GetEncoderInfo("image/png");
                    break;

                default:
                    myImageCodecInfo = GetEncoderInfo("image/jpeg");
                    break;
            }

            // Create an Encoder object based on the GUID for the Quality parameter category.
            myEncoder = Encoder.Quality;

            // Create an EncoderParameters object. An EncoderParameters object has an array of
            // EncoderParameter objects. In this case, there is only one EncoderParameter object in
            // the array.
            myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap file with quality level 25.
            myEncoderParameter = new EncoderParameter(myEncoder, m_Quality);
            myEncoderParameters.Param[0] = myEncoderParameter;
            Image.Save(Destination, myImageCodecInfo, myEncoderParameters);
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        #endregion

        #endregion

        #region [ IDisposable Members ]

        /// <summary>
        /// Object's finalizer. If objects are still in use, make sure the're released
        /// </summary>
        ~ThumbnailCreation()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose all resources used by the object and tell the Garbage Collector to free system
        /// resources in use by our object.
        /// </summary>
        public void Dispose()
        {
            // Destroy all resources used by our object
            Dispose(true);

            // Prevent the Garbage Collector to call finalize on our object. This is not required
            // anymore because the Dispose() method called above will automaticly destroy resources required.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Actually dispose all resources of our class
        /// </summary>
        /// <param name="Disposing"></param>
        protected virtual void Dispose(bool Disposing)
        {
            if ((Disposing) && (!_IsDisposed))
            {
                // Destroy managed resources used by your object
                throw new Exception("This method is not implemented yet. I forgot to dispose resources!");
            }

            // Destroy unmanaged resources used by your object
            _IsDisposed = true;
        }

        #endregion
    }
}