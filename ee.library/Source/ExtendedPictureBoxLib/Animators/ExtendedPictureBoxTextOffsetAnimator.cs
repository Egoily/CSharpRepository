using System;
using System.ComponentModel;
using System.Drawing;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextOffset"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxTextOffsetAnimator : ExtendedPictureBoxOffsetAnimatorBase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxTextOffsetAnimator(IContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxTextOffsetAnimator()
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
                    base.ExtendedPictureBox.TextOffsetChanged -= new EventHandler(OnCurrentValueChanged);

                base.ExtendedPictureBox = value;

                if (base.ExtendedPictureBox != null)
                    base.ExtendedPictureBox.TextOffsetChanged += new EventHandler(OnCurrentValueChanged);
            }
        }

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override Point CurrentOffset
        {
            get { return base.ExtendedPictureBox.TextOffset; }
            set { base.ExtendedPictureBox.TextOffset = value; }
        }

        #endregion
    }
}