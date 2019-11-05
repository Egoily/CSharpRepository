using System.ComponentModel;

using Animations;

namespace ExtendedPictureBoxLib.Animators
{
    /// <summary>
    /// Class inheriting <see cref="Animations.AnimatorBase"/> to animate the <see
    /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.State"/> of a <see
    /// cref="ExtendedPictureBox"/>. It can be altered by setting which parts of the state should be animated.
    /// </summary>
    public partial class ExtendedPictureBoxStateAnimator : DummyAnimator
    {
        #region Fields

        private ExtendedPictureBoxAlphaAnimator _alphaAnimator;
        private ControlBackColorAnimator _backColorAnimator;
        private ExtendedPictureBoxBackColor2Animator _backColor2Animator;
        private ExtendedPictureBoxBackColorGradientRotationAngleAnimator _backColorGradientRotationAngleAnimator;
        private ExtendedPictureBoxRotationAngleAnimator _rotationAngleAnimator;
        private ExtendedPictureBoxExtraImageRotationAngleAnimator _extraImageRotationAngleAnimator;
        private ExtendedPictureBoxZoomAnimator _zoomAnimator;
        private ControlForeColorAnimator _foreColorAnimator;
        private ExtendedPictureBoxTextHaloColorAnimator _textHaloColorAnimator;
        private ExtendedPictureBoxTextRotationAngleAnimator _textRotationAngleAnimator;
        private ExtendedPictureBoxTextHaloWidthAnimator _textHaloWidthAnimator;
        private ExtendedPictureBoxTextZoomAnimator _textZoomAnimator;
        private ExtendedPictureBoxShadowOffsetAnimator _shadowOffsetAnimator;
        private ExtendedPictureBoxImageOffsetAnimator _imageOffsetAnimator;
        private ExtendedPictureBoxTextOffsetAnimator _textOffsetAnimator;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="container">Container the new instance should be added to.</param>
        public ExtendedPictureBoxStateAnimator(IContainer container)
            : base(container)
        {
            Initialize();
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public ExtendedPictureBoxStateAnimator()
        {
            Initialize();
        }

        private void Initialize()
        {
            components = new Container();
            _alphaAnimator = new ExtendedPictureBoxAlphaAnimator(components);
            _backColorAnimator = new ControlBackColorAnimator(components);
            _backColor2Animator = new ExtendedPictureBoxBackColor2Animator(components);
            _backColorGradientRotationAngleAnimator = new ExtendedPictureBoxBackColorGradientRotationAngleAnimator(components);
            _rotationAngleAnimator = new ExtendedPictureBoxRotationAngleAnimator(components);
            _extraImageRotationAngleAnimator = new ExtendedPictureBoxExtraImageRotationAngleAnimator(components);
            _zoomAnimator = new ExtendedPictureBoxZoomAnimator(components);
            _foreColorAnimator = new ControlForeColorAnimator(components);
            _textHaloColorAnimator = new ExtendedPictureBoxTextHaloColorAnimator(components);
            _textRotationAngleAnimator = new ExtendedPictureBoxTextRotationAngleAnimator(components);
            _textHaloWidthAnimator = new ExtendedPictureBoxTextHaloWidthAnimator(components);
            _textZoomAnimator = new ExtendedPictureBoxTextZoomAnimator(components);
            _shadowOffsetAnimator = new ExtendedPictureBoxShadowOffsetAnimator(components);
            _imageOffsetAnimator = new ExtendedPictureBoxImageOffsetAnimator(components);
            _textOffsetAnimator = new ExtendedPictureBoxTextOffsetAnimator(components);
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets or sets the starting state for the animation.
        /// </summary>
        [Category("Appearance")]
        [Browsable(true)]
        [Description("Gets or sets the starting state for the animation.")]
        public PictureBoxState StartState
        {
            get
            {
                return new PictureBoxState(_alphaAnimator.StartAlpha,
                    _rotationAngleAnimator.StartRotationAngle, _zoomAnimator.StartZoom,
                    _extraImageRotationAngleAnimator.StartRotationAngle,
                    _backColorGradientRotationAngleAnimator.StartRotationAngle,
                    _backColorAnimator.StartColor, _backColor2Animator.StartColor,
                    _foreColorAnimator.StartColor, _textHaloColorAnimator.StartColor,
                    _textHaloWidthAnimator.StartWidth, _textRotationAngleAnimator.StartRotationAngle,
                    _textZoomAnimator.StartZoom, _shadowOffsetAnimator.StartOffset,
                    _imageOffsetAnimator.StartOffset, _textOffsetAnimator.StartOffset);
            }
            set
            {
                _alphaAnimator.StartAlpha = value.Alpha;
                _rotationAngleAnimator.StartRotationAngle = value.RotationAngle;
                _zoomAnimator.StartZoom = value.Zoom;
                _extraImageRotationAngleAnimator.StartRotationAngle = value.ExtraImageRotationAngle;
                _backColorGradientRotationAngleAnimator.StartRotationAngle = value.BackColorGradientRotationAngle;
                _backColorAnimator.StartColor = value.BackColor;
                _backColor2Animator.StartColor = value.BackColor2;
                _foreColorAnimator.StartColor = value.ForeColor;
                _textHaloColorAnimator.StartColor = value.TextHaloColor;
                _textRotationAngleAnimator.StartRotationAngle = value.RotationAngle;
                _textHaloWidthAnimator.StartWidth = value.TextHaloWidth;
                _textZoomAnimator.StartZoom = value.TextZoom;
                _shadowOffsetAnimator.StartOffset = value.ShadowOffset;
                _imageOffsetAnimator.StartOffset = value.ImageOffset;
                _textOffsetAnimator.StartOffset = value.TextOffset;
            }
        }

        /// <summary>
        /// Gets or sets the ending state for the animation.
        /// </summary>
        [Category("Appearance")]
        [Browsable(true)]
        [Description("Gets or sets the ending state for the animation.")]
        public PictureBoxState EndState
        {
            get
            {
                return new PictureBoxState(_alphaAnimator.EndAlpha,
                    _rotationAngleAnimator.EndRotationAngle, _zoomAnimator.EndZoom,
                    _extraImageRotationAngleAnimator.EndRotationAngle,
                    _backColorGradientRotationAngleAnimator.EndRotationAngle,
                    _backColorAnimator.EndColor, _backColor2Animator.EndColor,
                    _foreColorAnimator.EndColor, _textHaloColorAnimator.EndColor,
                    _textHaloWidthAnimator.EndWidth, _textRotationAngleAnimator.EndRotationAngle,
                    _textZoomAnimator.EndZoom, _shadowOffsetAnimator.EndOffset,
                    _imageOffsetAnimator.EndOffset, _textOffsetAnimator.EndOffset);
            }
            set
            {
                _alphaAnimator.EndAlpha = value.Alpha;
                _rotationAngleAnimator.EndRotationAngle = value.RotationAngle;
                _zoomAnimator.EndZoom = value.Zoom;
                _extraImageRotationAngleAnimator.EndRotationAngle = value.ExtraImageRotationAngle;
                _backColorGradientRotationAngleAnimator.EndRotationAngle = value.BackColorGradientRotationAngle;
                _backColorAnimator.EndColor = value.BackColor;
                _backColor2Animator.EndColor = value.BackColor2;
                _foreColorAnimator.EndColor = value.ForeColor;
                _textHaloColorAnimator.EndColor = value.TextHaloColor;
                _textRotationAngleAnimator.EndRotationAngle = value.TextRotationAngle;
                _textHaloWidthAnimator.EndWidth = value.TextHaloWidth;
                _textZoomAnimator.EndZoom = value.TextZoom;
                _shadowOffsetAnimator.EndOffset = value.ShadowOffset;
                _imageOffsetAnimator.EndOffset = value.ImageOffset;
                _textOffsetAnimator.EndOffset = value.TextOffset;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="ExtendedPictureBox"/> which <see
        /// cref="ExtendedPictureBoxLib.ExtendedPictureBox.State"/> should be animated.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Behavior")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Description("Gets or sets which ExtendedPictureBox should be animated.")]
        public ExtendedPictureBox ExtendedPictureBox
        {
            get { return _alphaAnimator.ExtendedPictureBox; }
            set
            {
                _alphaAnimator.ExtendedPictureBox = value;
                _backColorAnimator.Control = value;
                _backColor2Animator.ExtendedPictureBox = value;
                _backColorGradientRotationAngleAnimator.ExtendedPictureBox = value;
                _rotationAngleAnimator.ExtendedPictureBox = value;
                _extraImageRotationAngleAnimator.ExtendedPictureBox = value;
                _zoomAnimator.ExtendedPictureBox = value;
                _foreColorAnimator.Control = value;
                _textHaloColorAnimator.ExtendedPictureBox = value;
                _textRotationAngleAnimator.ExtendedPictureBox = value;
                _textHaloWidthAnimator.ExtendedPictureBox = value;
                _textZoomAnimator.ExtendedPictureBox = value;
                _shadowOffsetAnimator.ExtendedPictureBox = value;
                _imageOffsetAnimator.ExtendedPictureBox = value;
                _textOffsetAnimator.ExtendedPictureBox = value;
            }
        }

        /// <summary>
        /// Sets or gets which properties of a given <see cref="ExtendedPictureBox"/> should be animated.
        /// </summary>
        public PictureBoxStateProperties AnimatedProperties
        {
            get
            {
                PictureBoxStateProperties result = PictureBoxStateProperties.None;
                if (_alphaAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.Alpha;
                if (_backColorAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.BackColor;
                if (_backColor2Animator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.BackColor2;
                if (_backColorGradientRotationAngleAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.BackColorGradientRotationAngle;
                if (_rotationAngleAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.RotationAngle;
                if (_extraImageRotationAngleAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.ExtraImageRotationAngle;
                if (_zoomAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.Zoom;
                if (_foreColorAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.ForeColor;
                if (_textHaloColorAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.TextHaloColor;
                if (_textRotationAngleAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.TextRotationAngle;
                if (_textHaloWidthAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.TextHaloWidth;
                if (_textZoomAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.TextZoom;
                if (_shadowOffsetAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.ShadowOffset;
                if (_imageOffsetAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.ImageOffset;
                if (_textOffsetAnimator.ParentAnimator == this)
                    result |= PictureBoxStateProperties.TextOffset;
                return result;
            }
            set
            {
                _alphaAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.Alpha) ? this : null;
                _backColorAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.BackColor) ? this : null;
                _backColor2Animator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.BackColor2) ? this : null;
                _backColorGradientRotationAngleAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.BackColorGradientRotationAngle) ? this : null;
                _rotationAngleAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.RotationAngle) ? this : null;
                _extraImageRotationAngleAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.ExtraImageRotationAngle) ? this : null;
                _zoomAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.Zoom) ? this : null;
                _foreColorAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.ForeColor) ? this : null;
                _textHaloColorAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.TextHaloColor) ? this : null;
                _textRotationAngleAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.TextRotationAngle) ? this : null;
                _textHaloWidthAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.TextHaloWidth) ? this : null;
                _textZoomAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.TextZoom) ? this : null;
                _shadowOffsetAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.ShadowOffset) ? this : null;
                _imageOffsetAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.ImageOffset) ? this : null;
                _textOffsetAnimator.ParentAnimator = PictureBoxState.IsPropertySet(value, PictureBoxStateProperties.TextOffset) ? this : null;
            }
        }

        /// <summary>
        /// Gets or sets whether a given <see cref="PictureBoxStateProperties"/> is set in <see cref="AnimatedProperties"/>.
        /// </summary>
        public bool this[PictureBoxStateProperties property]
        {
            get { return PictureBoxState.IsPropertySet(this.AnimatedProperties, property); }
            set
            {
                if (value)
                    this.AnimatedProperties = this.AnimatedProperties | property;
                else
                    this.AnimatedProperties = this.AnimatedProperties & ~property;
            }
        }

        #endregion
    }
}