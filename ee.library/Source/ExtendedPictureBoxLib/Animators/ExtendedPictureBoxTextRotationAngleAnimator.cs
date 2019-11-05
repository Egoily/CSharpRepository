using System;
using System.ComponentModel;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextRotationAngle"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxTextRotationAngleAnimator : ExtendedPictureBoxRotationAngleAnimator
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxTextRotationAngleAnimator(IContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxTextRotationAngleAnimator()
            : base()
        {
        }

        #endregion

        #region Overridden from ExtendedPictureBoxRotationAngleAnimator

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextRotationAngle"/> should be animated.
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
                    ExtendedPictureBoxInternal.TextRotationAngleChanged -= new EventHandler(OnCurrentValueChanged);

                ExtendedPictureBoxInternal = value;

                if (ExtendedPictureBoxInternal != null)
                    ExtendedPictureBoxInternal.TextRotationAngleChanged += new EventHandler(OnCurrentValueChanged);

                base.ResetValues();
            }
        }

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override object CurrentValueInternal
        {
            get { return ExtendedPictureBox == null ? (float)0 : ExtendedPictureBox.TextRotationAngle; }
            set
            {
                if (ExtendedPictureBox != null)
                    ExtendedPictureBox.TextRotationAngle = (float)value;
            }
        }

        #endregion
    }
}