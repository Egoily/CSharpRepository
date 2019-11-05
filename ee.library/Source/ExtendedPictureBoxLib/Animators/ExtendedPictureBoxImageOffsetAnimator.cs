using System;
using System.ComponentModel;
using System.Drawing;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.ImageOffset"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxImageOffsetAnimator : ExtendedPictureBoxOffsetAnimatorBase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxImageOffsetAnimator(IContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxImageOffsetAnimator()
        {
        }

        #endregion

        #region Overridden from AnimatorBase

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see cref="ExtendedPictureBox"/>
        /// should be animated.
        /// </summary>
        public override ExtendedPictureBox ExtendedPictureBox
        {
            get { return base.ExtendedPictureBox; }
            set
            {
                if (base.ExtendedPictureBox != null)
                    base.ExtendedPictureBox.ImageOffsetChanged -= new EventHandler(OnCurrentValueChanged);

                base.ExtendedPictureBox = value;

                if (base.ExtendedPictureBox != null)
                    base.ExtendedPictureBox.ImageOffsetChanged += new EventHandler(OnCurrentValueChanged);
            }
        }

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override Point CurrentOffset
        {
            get { return base.ExtendedPictureBox.ImageOffset; }
            set { base.ExtendedPictureBox.ImageOffset = value; }
        }

        #endregion
    }
}