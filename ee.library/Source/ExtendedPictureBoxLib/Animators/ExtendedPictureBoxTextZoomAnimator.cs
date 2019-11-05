using System;
using System.ComponentModel;

using Animations;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextZoom"/> of a <see cref="ExtendedPictureBox"/>.
    /// </summary>
    public partial class ExtendedPictureBoxTextZoomAnimator : AnimatorBase
    {
        #region Fields

        private const float DEFAULT_ZOOM = 100f;

        private ExtendedPictureBox _extendedPictureBox;
        private float _startZoom;
        private float _endZoom;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxTextZoomAnimator(IContainer container)
            : base(container)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxTextZoomAnimator()
        {
            Initialize();
        }

        private void Initialize()
        {
            _startZoom = DEFAULT_ZOOM;
            _endZoom = DEFAULT_ZOOM;
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets or sets the starting zoom for the animation.
        /// </summary>
        [Category("Appearance"), DefaultValue(DEFAULT_ZOOM)]
        [Browsable(true)]
        [Description("Gets or sets the starting zoom for the animation.")]
        public float StartZoom
        {
            get { return _startZoom; }
            set
            {
                if (_startZoom == value)
                    return;

                _startZoom = value;

                OnStartValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the ending zoom for the animation.
        /// </summary>
        [Category("Appearance"), DefaultValue(DEFAULT_ZOOM)]
        [Browsable(true)]
        [Description("Gets or sets the ending zoom for the animation.")]
        public float EndZoom
        {
            get { return _endZoom; }
            set
            {
                if (_endZoom == value)
                    return;

                _endZoom = value;

                OnEndValueChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.TextZoom"/> should be animated.
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
                    _extendedPictureBox.TextZoomChanged -= new EventHandler(OnCurrentValueChanged);

                _extendedPictureBox = value;

                if (_extendedPictureBox != null)
                    _extendedPictureBox.TextZoomChanged += new EventHandler(OnCurrentValueChanged);

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
            get { return _extendedPictureBox == null ? (float)0 : _extendedPictureBox.TextZoom; }
            set
            {
                if (_extendedPictureBox != null)
                    _extendedPictureBox.TextZoom = (float)value;
            }
        }

        /// <summary>
        /// Gets or sets the starting value for the animation.
        /// </summary>
        public override object StartValue
        {
            get { return StartZoom; }
            set { StartZoom = (float)value; }
        }

        /// <summary>
        /// Gets or sets the ending value for the animation.
        /// </summary>
        public override object EndValue
        {
            get { return EndZoom; }
            set { EndZoom = (float)value; }
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
            float result = (float)InterpolateDoubleValues(_startZoom, _endZoom, step);
            return (float)InterpolateDoubleValues(_startZoom, _endZoom, step);
        }

        #endregion
    }
}