using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Animations;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.BackColor2"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxBackColor2Animator : ControlBackColorAnimator
    {
        #region (* Fields *)

        private ExtendedPictureBox _extendedPictureBox;

        #endregion

        #region (* Constructors *)

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxBackColor2Animator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxBackColor2Animator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region (* Public interface *)

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.BackColor2"/> should be animated.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Behavior")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Gets or sets which ExtendedPictureBox should be animated.")]
        public ExtendedPictureBox ExtendedPictureBox
        {
            get { return _extendedPictureBox; }
            set
            {
                if (_extendedPictureBox == value)
                    return;

                if (_extendedPictureBox != null)
                    _extendedPictureBox.BackColor2Changed -= new EventHandler(OnCurrentValueChanged);

                _extendedPictureBox = value;

                if (_extendedPictureBox != null)
                    _extendedPictureBox.BackColor2Changed += new EventHandler(OnCurrentValueChanged);

                _extendedPictureBox = value;

                base.ResetValues();
            }
        }

        #endregion

        #region (* Overridden from ControlBackColorAnimator *)

        /// <summary>
        /// Gets or sets the <see cref="Control"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.BackColor2"/> should be animated.
        /// </summary>
        [Browsable(false), Category("Behavior")]
        [DefaultValue(null), RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gets or sets which ExtendedPictureBox should be animated.")]
        public override Control Control
        {
            get { return _extendedPictureBox; }
            set { ExtendedPictureBox = (ExtendedPictureBox)value; }
        }

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override object CurrentValueInternal
        {
            get { return _extendedPictureBox == null ? Color.Empty : _extendedPictureBox.BackColor2; }
            set
            {
                if (_extendedPictureBox != null)
                    _extendedPictureBox.BackColor2 = (Color)value;
            }
        }

        #endregion
    }
}