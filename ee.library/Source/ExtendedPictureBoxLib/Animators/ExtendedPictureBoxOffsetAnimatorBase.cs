using System;
using System.ComponentModel;
using System.Drawing;

using Animations;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Base class inheriting <see cref="Animations.AnimatorBase"/> helping to animate thevone of
    /// the offset properties of an <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxOffsetAnimatorBase : AnimatorBase
    {
        #region Fields

        private ExtendedPictureBox _extendedPictureBox;
        private Point _startOffset;
        private Point _endOffset;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxOffsetAnimatorBase(IContainer container)
            : base(container)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxOffsetAnimatorBase()
        {
            Initialize();
        }

        private void Initialize()
        {
            _startOffset = DefaultOffset;
            _endOffset = DefaultOffset;
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets or sets the starting offset for the animation.
        /// </summary>
        [Category("Appearance"), Browsable(true)]
        [Description("Gets or sets the starting offset for the animation.")]
        public Point StartOffset
        {
            get { return _startOffset; }
            set
            {
                if (_startOffset == value)
                    return;

                _startOffset = value;

                OnStartValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the ending offset for the animation.
        /// </summary>
        [Category("Appearance"), Browsable(true)]
        [Description("Gets or sets the ending offset for the animation.")]
        public Point EndOffset
        {
            get { return _endOffset; }
            set
            {
                if (_endOffset == value)
                    return;

                _endOffset = value;

                OnEndValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see cref="ExtendedPictureBox"/>
        /// should be animated.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Behavior")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Gets or sets which ExtendedPictureBox should be animated.")]
        public virtual ExtendedPictureBox ExtendedPictureBox
        {
            get { return _extendedPictureBox; }
            set
            {
                if (_extendedPictureBox == value)
                    return;

                _extendedPictureBox = value;

                base.ResetValues();
            }
        }

        #endregion

        #region Protected interface

        /// <summary>
        /// Unsafe getter and setter for inheriting classes to match to the actual property.
        /// </summary>
        protected virtual Point CurrentOffset
        {
            get { return DefaultOffset; }
            set { }
        }

        /// <summary>
        /// Gets the default value for <see cref="StartOffset"/> and <see cref="EndOffset"/>.
        /// </summary>
        protected virtual Point DefaultOffset
        {
            get { return Point.Empty; }
        }

        /// <summary>
        /// Indicates the designer whether <see cref="StartOffset"/> needs to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeStartOffset()
        {
            return _startOffset == DefaultOffset;
        }

        /// <summary>
        /// Indicates the designer whether <see cref="EndOffset"/> needs to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeEndOffset()
        {
            return _endOffset == DefaultOffset;
        }

        #endregion

        #region Overridden from AnimatorBase

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override object CurrentValueInternal
        {
            get { return _extendedPictureBox == null ? DefaultOffset : CurrentOffset; }
            set
            {
                if (_extendedPictureBox != null)
                    CurrentOffset = (Point)value;
            }
        }

        /// <summary>
        /// Gets or sets the starting value for the animation.
        /// </summary>
        public override object StartValue
        {
            get { return StartOffset; }
            set { StartOffset = (Point)value; }
        }

        /// <summary>
        /// Gets or sets the ending value for the animation.
        /// </summary>
        public override object EndValue
        {
            get { return EndOffset; }
            set { EndOffset = (Point)value; }
        }

        /// <summary>
        /// Calculates an interpolated value between <see cref="StartValue"/> and <see
        /// cref="EndValue"/> for a given step in %. Giving 0 will return the <see
        /// cref="StartValue"/>. Giving 100 will return the <see cref="EndValue"/>.
        /// </summary>
        /// <param name="step">Animation step in %</param>
        /// <returns>Interpolated value for the given step.</returns>
        protected override object GetValueForStep(double step)
        {
            return InterpolatePoints(_startOffset, _endOffset, step);
        }

        #endregion
    }
}