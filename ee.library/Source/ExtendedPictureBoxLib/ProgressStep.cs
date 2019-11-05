using System.ComponentModel;
using System.Drawing;

using ExtendedPictureBoxLib.Design;

namespace ExtendedPictureBoxLib
{
    /// <summary>
    /// Class describing on step in a <see cref="AnimatedPicturesProgressBar"/>.
    /// </summary>
    [TypeConverter(typeof(ProgressStepConverter))]
    public class ProgressStep
    {
        #region Fields

        private Image _image;
        private string _name;
        private string _text;
        private string _description;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ProgressStep()
        {
            _image = null;
            _name = "StepName";
            _text = "StepText";
            _description = "Processing step '{0}' ({1}/{2})...";
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="image">Image applied to the step.</param>
        /// <param name="name">Name applied to the step.</param>
        /// <param name="text">Text applied to the step.</param>
        /// <param name="description">Description applied to the step.</param>
        public ProgressStep(Image image, string name, string text, string description)
        {
            _image = image;
            _name = name;
            _text = text;
            _description = description;
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets or sets the image applied to the step.
        /// </summary>
        [Category("Appearance")]
        [DefaultValue(null)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image Image
        {
            get { return _image; }
            set { _image = value; }
        }

        /// <summary>
        /// Gets or sets the name applied to the step.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the text applied to the step.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        /// <summary>
        /// Gets or sets the description applied to the step. The <see
        /// cref="AnimatedPicturesProgressBar"/> will replace some keywords before showing the
        /// description anyhwere. The replacements are the following: {0} -&gt; Name of the step.
        /// {1} -&gt; Index of the step. {2} -&gt; Total number of steps.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion
    }
}