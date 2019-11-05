using System;
using System.ComponentModel;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.BackColorGradientRotationAngle"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxBackColorGradientRotationAngleAnimator : ExtendedPictureBoxRotationAngleAnimator
    {
        #region (* Constructors *)

        /// <summary>
        /// Creates a new instance.
        /// </summary>

        public ExtendedPictureBoxBackColorGradientRotationAngleAnimator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxBackColorGradientRotationAngleAnimator(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region (* Overridden from ExtendedPictureBoxRotationAngleAnimator *)

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.BackColorGradientRotationAngle"/> should
        /// be animated.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Behavior")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Gets or sets which ExtendedPictureBox should be animated.")]
        public override ExtendedPictureBox ExtendedPictureBox
        {
            get { return ExtendedPictureBoxInternal; }
            set
            {
                if (ExtendedPictureBoxInternal == value)
                    return;

                if (ExtendedPictureBoxInternal != null)
                    ExtendedPictureBoxInternal.BackColorGradientRotationAngleChanged -= new EventHandler(OnCurrentValueChanged);

                ExtendedPictureBoxInternal = value;

                if (ExtendedPictureBoxInternal != null)
                    ExtendedPictureBoxInternal.BackColorGradientRotationAngleChanged += new EventHandler(OnCurrentValueChanged);

                base.ResetValues();
            }
        }

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override object CurrentValueInternal
        {
            get { return ExtendedPictureBox == null ? (float)0 : ExtendedPictureBox.BackColorGradientRotationAngle; }
            set
            {
                if (ExtendedPictureBox != null)
                    ExtendedPictureBox.BackColorGradientRotationAngle = (float)value;
            }
        }

        #endregion
    }
}