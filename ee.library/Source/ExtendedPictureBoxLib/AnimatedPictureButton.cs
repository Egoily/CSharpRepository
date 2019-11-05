using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ExtendedPictureBoxLib
{
    /// <summary>
    /// Defines the states an <see cref="AnimatedPictureButton"/> can be in.
    /// </summary>
    public enum AnimatedButtonState
    {
        /// <summary>
        /// <see cref="AnimatedPictureButton.StartState"/> is shown (mouse is not hovering the button).
        /// </summary>
        Start,

        /// <summary>
        /// <see cref="AnimatedPictureButton.EndState"/> is shown (mouse is hovering the button).
        /// </summary>
        End,

        /// <summary>
        /// <see cref="AnimatedPictureButton.PushedState"/> is shown (mouse button is currently down
        /// on button).
        /// </summary>
        Pushed
    }

    /// <summary>
    /// Control further extending the <see cref="AnimatedPictureBox"/> by defining a <see
    /// cref="StartState"/> and an <see cref="EndState"/>. It animtes itself between those two
    /// states when the mouse moves over or leaves the control.
    /// </summary>
    public partial class AnimatedPictureButton : AnimatedPictureBox
    {
        #region (* Events *)

        /// <summary>
        /// Event which gets fired when <see cref="StartState"/> has changed.
        /// </summary>
        public event EventHandler StartStateChanged;

        /// <summary>
        /// Event which gets fired when <see cref="EndState"/> has changed.
        /// </summary>
        public event EventHandler EndStateChanged;

        /// <summary>
        /// Event which gets fired when <see cref="PushedState"/> has changed.
        /// </summary>
        public event EventHandler PushedStateChanged;

        /// <summary>
        /// Event which gets fired when <see cref="PushedState"/> has changed.
        /// </summary>
        public event EventHandler PushedPropertiesChanged;

        /// <summary>
        /// Event which gets fired when <see cref="ButtonState"/> has changed.
        /// </summary>
        public event EventHandler ButtonStateChanged;

        #endregion

        #region Fields

        private const ShadowMode DEFAULT_SHADOW_MODE = ShadowMode.OffsetFromCenter;
        private const PictureBoxStateProperties DEFAULT_PUSH_PROPERTIES = PictureBoxStateProperties.ImageProperties;

        private PictureBoxState _startState;
        private PictureBoxState _endState;
        private PictureBoxState _pushedState;
        private PictureBoxStateProperties _pushProperties = DEFAULT_PUSH_PROPERTIES;

        private AnimatedButtonState _buttonState = AnimatedButtonState.Start;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public AnimatedPictureButton()
        {
            InitializeComponent();

            _startState = DefaultStartState;
            _endState = DefaultEndState;
            _pushedState = DefaultPushedState;
            base.ShadowMode = DEFAULT_SHADOW_MODE;

            base.State = _startState;

            UpdateSettings();
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets the state of the button.
        /// </summary>
        [Browsable(false)]
        public AnimatedButtonState ButtonState
        {
            get { return _buttonState; }
        }

        /// <summary>
        /// Gets or sets the state of the button when the mouse is not over it.
        /// </summary>
        [Browsable(true), Category("Appearance")]
        [Description("Gets or sets the state of the button when the mouse does not over it.")]
        public PictureBoxState StartState
        {
            get { return _startState; }
            set
            {
                if (_startState == value)
                    return;

                _startState = value;

                OnStartStateChanged(EventArgs.Empty);

                UpdateSettings();
            }
        }

        /// <summary>
        /// Gets or sets the state of the button when the mouse is over it.
        /// </summary>
        [Browsable(true), Category("Appearance")]
        [Description("Gets or sets the state of the button when the mouse is over it.")]
        public PictureBoxState EndState
        {
            get { return _endState; }
            set
            {
                if (_endState == value)
                    return;

                _endState = value;

                OnEndStateChanged(EventArgs.Empty);

                UpdateSettings();
            }
        }

        /// <summary>
        /// Gets or sets the state of the button when the mouse is over it.
        /// </summary>
        [Browsable(true), Category("Appearance")]
        [Description("Gets or sets the state of the button when the mouse is over it.")]
        public PictureBoxState PushedState
        {
            get { return _pushedState; }
            set
            {
                if (_pushedState == value)
                    return;

                _pushedState = value;

                OnPushedStateChanged(EventArgs.Empty);

                UpdateSettings();
            }
        }

        /// <summary>
        /// Gets or sets which properties of the <see cref="PushedState"/> should be applied when
        /// the button is clicked.
        /// </summary>
        [Browsable(true), Category("Appearance")]
        [Description("Gets or sets which properties of the PushedState should be applied when the button is clicked..")]
        public PictureBoxStateProperties PushedProperties
        {
            get { return _pushProperties; }
            set
            {
                if (_pushProperties == value)
                    return;

                _pushProperties = value;

                OnPushedPropertiesChanged(EventArgs.Empty);

                UpdateSettings();
            }
        }

        /// <summary>
        /// Animates the control to its <see cref="StartState"/>.
        /// </summary>
        public void AnimateToStart()
        {
            if (_buttonState == AnimatedButtonState.Pushed)
                Release();
            base.Animate(_startState);
            SetState(AnimatedButtonState.Start);
        }

        /// <summary>
        /// Animates the control to its <see cref="EndState"/>.
        /// </summary>
        public void AnimateToEnd()
        {
            base.Animate(_endState);
            SetState(AnimatedButtonState.End);
        }

        /// <summary>
        /// Sets the buttons state to <see cref="PushedState"/> (no animation).
        /// </summary>
        public void Push()
        {
            base.StopAnimation();
            ApplyPushedState();
            SetState(AnimatedButtonState.Pushed);
        }

        /// <summary>
        /// Sets the buttons state to <see cref="EndState"/> (no animation).
        /// </summary>
        public void Release()
        {
            base.StopAnimation();
            base.State = _endState;
            SetState(AnimatedButtonState.End);
        }

        #endregion

        #region Protected interface

        #region Defaults

        /// <summary>
        /// Gets the default value for <see cref="StartState"/>.
        /// </summary>
        protected virtual PictureBoxState DefaultStartState
        {
            get
            {
                return new PictureBoxState(100, 180f, 50f, -180f, 90f,
                    Color.LightGreen, Color.LightBlue, Color.Black,
                    Color.White, 0, 0f, 100f, new Point(2, 2), Point.Empty,
                    Point.Empty);
            }
        }

        /// <summary>
        /// Gets the default value for <see cref="EndState"/>.
        /// </summary>
        protected virtual PictureBoxState DefaultEndState
        {
            get
            {
                return new PictureBoxState(255, 0f, 100f, 0f, 0f,
                    Color.LightGreen, Color.LightBlue, Color.Black,
                    Color.White, 0, 0f, 100f, new Point(2, 2), Point.Empty,
                    Point.Empty);
            }
        }

        /// <summary>
        /// Gets the default value for <see cref="PushedState"/>.
        /// </summary>
        protected virtual PictureBoxState DefaultPushedState
        {
            get
            {
                PictureBoxState result = DefaultEndState;
                result.ImageOffset = new Point(2, 2);
                return result;
            }
        }

        #endregion

        #region ShouldSerialize

        /// <summary>
        /// Indicates the designer whether <see cref="StartState"/> needs to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeStartState()
        {
            return _startState != DefaultStartState;
        }

        /// <summary>
        /// Indicates the designer whether <see cref="EndState"/> needs to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeEndState()
        {
            return _endState != DefaultEndState;
        }

        /// <summary>
        /// Indicates the designer whether <see cref="PushedState"/> needs to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializePushedState()
        {
            return _pushedState != DefaultPushedState;
        }

        #endregion

        #region Eventraiser

        /// <summary>
        /// Raises the <see cref="StartStateChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnStartStateChanged(EventArgs eventArgs)
        {
            if (StartStateChanged != null)
                StartStateChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="EndStateChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnEndStateChanged(EventArgs eventArgs)
        {
            if (EndStateChanged != null)
                EndStateChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="PushedStateChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnPushedStateChanged(EventArgs eventArgs)
        {
            if (PushedStateChanged != null)
                PushedStateChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="PushedPropertiesChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnPushedPropertiesChanged(EventArgs eventArgs)
        {
            if (PushedPropertiesChanged != null)
                PushedPropertiesChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ButtonStateChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnButtonStateChanged(EventArgs eventArgs)
        {
            if (ButtonStateChanged != null)
                ButtonStateChanged(this, eventArgs);
        }

        #endregion

        #endregion

        #region Privates

        private void SetState(AnimatedButtonState newState)
        {
            if (_buttonState == newState)
                return;

            _buttonState = newState;

            OnButtonStateChanged(EventArgs.Empty);
        }

        private void ApplyPushedState()
        {
            PictureBoxState state = _endState;
            state.Apply(_pushedState, _pushProperties);
            base.State = state;
        }

        private void UpdateSettings()
        {
            base.StopAnimation();
            switch (_buttonState)
            {
                case AnimatedButtonState.Pushed:
                    ApplyPushedState();
                    base.State = _pushedState;

                    base.UpdateStartValues(_startState);
                    base.UpdateEndValues(_endState);
                    break;

                case AnimatedButtonState.Start:
                    base.State = _startState;

                    base.UpdateStartValues(_endState);
                    base.UpdateEndValues(_startState);
                    break;

                case AnimatedButtonState.End:
                    base.State = _endState;

                    base.UpdateStartValues(_startState);
                    base.UpdateEndValues(_endState);
                    break;
            }
        }

        #endregion

        #region Overridden from AnimatedPictureBox

        #region Unwanted base properties

        /// <summary>
        /// Gets or sets the alpha value which should be applied to the image. The alpha value is
        /// calcualted on a per pixel basis and pixels already having an alpha value less then 255
        /// will be reduced further. The effect is that transparent parts of an image will remain
        /// transparent. Overridden to disable designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override byte Alpha
        {
            get { return base.Alpha; }
            set { base.Alpha = value; }
        }

        /// <summary>
        /// Gets or sets the rotation angle of the main image in degrees. Overridden to disable
        /// designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float RotationAngle
        {
            get { return base.RotationAngle; }
            set { base.RotationAngle = value; }
        }

        /// <summary>
        /// Gets or sets the zoom factor with which the main image should be drawn. Overridden to
        /// disable designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float Zoom
        {
            get { return base.Zoom; }
            set { base.Zoom = value; }
        }

        /// <summary>
        /// Angle of the <see cref="ExtendedPictureBox.ExtraImage"/> in degrees. Overridden to
        /// disable designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float ExtraImageRotationAngle
        {
            get { return base.ExtraImageRotationAngle; }
            set { base.ExtraImageRotationAngle = value; }
        }

        /// <summary>
        /// Angle of the background gradient in degrees. Overridden to disable designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float BackColorGradientRotationAngle
        {
            get { return base.BackColorGradientRotationAngle; }
            set { base.BackColorGradientRotationAngle = value; }
        }

        /// <summary>
        /// Gets or sets the first background color. Readjusts also <see cref="BackColor2"/> if it
        /// has the same value currently. Overridden to disable designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }

        /// <summary>
        /// Gets or sets the second color to draw the background gradient. Overridden to disable
        /// designer support.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor2
        {
            get { return base.BackColor2; }
            set { base.BackColor2 = value; }
        }

        /// <summary>
        /// Gets or sets the rotation angle of the text in degrees.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float TextRotationAngle
        {
            get { return base.TextRotationAngle; }
            set { base.TextRotationAngle = value; }
        }

        /// <summary>
        /// Gets or sets the width of the halo of the text. 0 or smaller if now halo should be shown.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float TextHaloWidth
        {
            get { return base.TextHaloWidth; }
            set { base.TextHaloWidth = value; }
        }

        /// <summary>
        /// Gets or sets the width of the color of the halo of the text.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color TextHaloColor
        {
            get { return base.TextHaloColor; }
            set { base.TextHaloColor = value; }
        }

        /// <summary>
        /// Gets or sets the zoom factor with which the text should be drawn.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override float TextZoom
        {
            get { return base.TextZoom; }
            set { base.TextZoom = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the main image shadow.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Point ShadowOffset
        {
            get { return base.ShadowOffset; }
            set { base.ShadowOffset = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the main image.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Point ImageOffset
        {
            get { return base.ImageOffset; }
            set { base.ImageOffset = value; }
        }

        /// <summary>
        /// Gets or sets the offset of the text.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Point TextOffset
        {
            get { return base.TextOffset; }
            set { base.TextOffset = value; }
        }

        /// <summary>
        /// Gets or sets whether a shadow of the main image should be drawn and how the offset is calculated.
        /// </summary>
        [DefaultValue(DEFAULT_SHADOW_MODE)]
        public override ShadowMode ShadowMode
        {
            get { return base.ShadowMode; }
            set { base.ShadowMode = value; }
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="Control.MouseEnter"/> event and starts animation to <see cref="EndState"/>.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (base.Enabled && base.ParentForm.ContainsFocus)
                AnimateToEnd();
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseLeave"/> event and starts animation to <see cref="StartState"/>.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (base.Enabled)
                AnimateToStart();
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseDown"/> event and sets the state to <see cref="PushedState"/>.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Push();
        }

        /// <summary>
        /// Raises the <see cref="Control.MouseUp"/> event and sets the state to <see cref="PushedState"/>.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Release();
        }

        /// <summary>
        /// Raises the <see cref="Control.EnabledChanged"/> event and starts animation to <see cref="StartState"/>.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            AnimateToStart();
        }

        #endregion
    }
}