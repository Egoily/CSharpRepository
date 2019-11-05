using System;
using System.ComponentModel;

using Animations;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextHaloWidth"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxTextHaloWidthAnimator : AnimatorBase
    {
        #region Fields

        private const int DEFAULT_WIDTH = 255;

        private ExtendedPictureBox _extendedPictureBox;
        private float _startWidth;
        private float _endWidth;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxTextHaloWidthAnimator(IContainer container)
            : base(container)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxTextHaloWidthAnimator()
        {
            Initialize();
        }

        private void Initialize()
        {
            _startWidth = DEFAULT_WIDTH;
            _endWidth = DEFAULT_WIDTH;
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets or sets the starting width for the animation.
        /// </summary>
        [Category("Appearance"), DefaultValue(DEFAULT_WIDTH)]
        [Browsable(true)]
        [Description("Gets or sets the starting width for the animation.")]
        public float StartWidth
        {
            get { return _startWidth; }
            set
            {
                if (_startWidth == value)
                    return;

                _startWidth = value;

                OnStartValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the ending width for the animation.
        /// </summary>
        [Category("Appearance"), DefaultValue(DEFAULT_WIDTH)]
        [Browsable(true)]
        [Description("Gets or sets the ending width for the animation.")]
        public float EndWidth
        {
            get { return _endWidth; }
            set
            {
                if (_endWidth == value)
                    return;

                _endWidth = value;

                OnEndValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextHaloWidth"/> should be animated.
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
                    _extendedPictureBox.TextHaloWidthChanged -= new EventHandler(OnCurrentValueChanged);

                _extendedPictureBox = value;

                if (_extendedPictureBox != null)
                    _extendedPictureBox.TextHaloWidthChanged += new EventHandler(OnCurrentValueChanged);

                base.ResetValues();
            }
        }

        #endregion

        #region Overridden from AnimatorBase

        /// <summary>
        /// Gets or sets the currently shown value.
        /// </summary>
        protected override object CurrentValueInternal
        {
            get { return _extendedPictureBox == null ? 0 : _extendedPictureBox.TextHaloWidth; }
            set
            {
                if (_extendedPictureBox != null)
                    _extendedPictureBox.TextHaloWidth = (float)value;
            }
        }

        /// <summary>
        /// Gets or sets the starting value for the animation.
        /// </summary>
        public override object StartValue
        {
            get { return StartWidth; }
            set { StartWidth = (float)value; }
        }

        /// <summary>
        /// Gets or sets the ending value for the animation.
        /// </summary>
        public override object EndValue
        {
            get { return EndWidth; }
            set { EndWidth = (float)value; }
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
            return (float)InterpolateDoubleValues(_startWidth, _endWidth, step);
        }

        #endregion
    }
}