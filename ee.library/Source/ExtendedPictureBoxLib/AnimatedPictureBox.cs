using System;
using System.ComponentModel;

using ExtendedPictureBoxLib.Animators;

namespace ExtendedPictureBoxLib
{
    /// <summary>
    /// Control further extending the <see cref="ExtendedPictureBox"/> by adding animation capabilities.
    /// </summary>
    public partial class AnimatedPictureBox : ExtendedPictureBox
    {
        #region Events

        /// <summary>
        /// Event which gets fired when <see cref="AnimationIntervall"/> has changed.
        /// </summary>
        public event EventHandler AnimationIntervallChanged;

        /// <summary>
        /// Event which gets fired when <see cref="AnimationStepSize"/> has changed.
        /// </summary>
        public event EventHandler AnimationStepSizeChanged;

        /// <summary>
        /// Event which gets fired when animation has been started with <see cref="Animate"/>.
        /// </summary>
        public event EventHandler AnimationStarted;

        /// <summary>
        /// Event which gets fired when animation has finished.
        /// </summary>
        public event EventHandler AnimationFinished;

        /// <summary>
        /// Event which gets fired when animation has been stopped with <see cref="StopAnimation()"/>.
        /// </summary>
        public event EventHandler AnimationStopped;

        #endregion

        #region (* Fields *)

        private const int DEFAULT_ANIMATION_INTERVALL = 20;
        private const double DEFAULT_ANIMATION_STEP_SIZE = 10;
        private const PictureBoxStateProperties DEFAULT_ANIMATED_PROPERTIES = PictureBoxStateProperties.All;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public AnimatedPictureBox()
        {
            InitializeComponent();

            _stateAnimator.AnimatedProperties = DEFAULT_ANIMATED_PROPERTIES;

            _stateAnimator.Intervall = DEFAULT_ANIMATION_INTERVALL;
            _stateAnimator.StepSize = DEFAULT_ANIMATION_STEP_SIZE;
        }

        #endregion

        #region Internal interface

        /// <summary>
        /// Gets the internally used <see cref="ExtendedPictureBoxStateAnimator"/>.
        /// </summary>
        internal ExtendedPictureBoxStateAnimator StateAnimator
        {
            get { return _stateAnimator; }
        }

        #endregion

        #region Public interface

        /// <summary>
        /// Gets or sets the intervall between updates of the animation (in milliseconds).
        /// </summary>
        [Browsable(true), Category("Behavior"), DefaultValue(DEFAULT_ANIMATION_INTERVALL)]
        [Description("Gets or sets the intervall between updates of the animation (in milliseconds).")]
        public int AnimationIntervall
        {
            get { return _stateAnimator.Intervall; }
            set { _stateAnimator.Intervall = value; }
        }

        /// <summary>
        /// Gets or sets the step size between updates of the animation (in % - 100 will result in
        /// one step -&gt; no actual animation).
        /// </summary>
        [Browsable(true), Category("Behavior"), DefaultValue(DEFAULT_ANIMATION_STEP_SIZE)]
        [Description("Gets or sets the step size between updates of the animation.")]
        public double AnimationStepSize
        {
            get { return _stateAnimator.StepSize; }
            set { _stateAnimator.StepSize = value; }
        }

        /// <summary>
        /// Gets whether an animation is currently running.
        /// </summary>
        [Browsable(false)]
        public bool IsAnimationRunning
        {
            get { return _stateAnimator.IsRunning; }
        }

        /// <summary>
        /// Animates from the last end state to the given new state.
        /// </summary>
        /// <param name="state">Destination state of the animation.</param>
        public void Animate(PictureBoxState state)
        {
            UpdateEndValues(state);
            _stateAnimator.Start(true);
        }

        /// <summary>
        /// Stops the animation immediately.
        /// </summary>
        public void StopAnimation()
        {
            _stateAnimator.Stop();
        }

        /// <summary>
        /// Sets or gets which properties of given <see cref="PictureBoxState"/> s should be animated.
        /// </summary>
        [Browsable(true), Category("Behavior"), DefaultValue(DEFAULT_ANIMATED_PROPERTIES)]
        [Description("Sets or gets which properties of given PictureBoxStates should be animated.")]
        public PictureBoxStateProperties AnimatedStateProperties
        {
            get { return _stateAnimator.AnimatedProperties; }
            set { _stateAnimator.AnimatedProperties = value; }
        }

        #endregion

        #region Privates

        private void OnAnimationStarted(object sender, EventArgs e)
        {
            OnAnimationStarted(e);
        }

        private void OnAnimationFinished(object sender, EventArgs e)
        {
            OnAnimationFinished(e);
        }

        private void OnAnimationStopped(object sender, EventArgs e)
        {
            OnAnimationStopped(e);
        }

        private void OnAnimationIntervallChanged(object sender, EventArgs e)
        {
            OnAnimationIntervallChanged(e);
        }

        private void OnAnimationStepSizeChanged(object sender, EventArgs e)
        {
            OnAnimationStepSizeChanged(e);
        }

        #endregion

        #region Protected interface

        /// <summary>
        /// Updates the starting state of the contained animator.
        /// </summary>
        /// <param name="state">State to set.</param>
        protected void UpdateStartValues(PictureBoxState state)
        {
            _stateAnimator.StartState = state;
        }

        /// <summary>
        /// Updates the ending state of the contained animator.
        /// </summary>
        /// <param name="state">State to set.</param>
        protected void UpdateEndValues(PictureBoxState state)
        {
            _stateAnimator.EndState = state;
        }

        #region Eventraiser

        /// <summary>
        /// Raises the <see cref="AnimationIntervallChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnAnimationIntervallChanged(EventArgs eventArgs)
        {
            if (AnimationIntervallChanged != null)
                AnimationIntervallChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationStepSizeChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnAnimationStepSizeChanged(EventArgs eventArgs)
        {
            if (AnimationStepSizeChanged != null)
                AnimationStepSizeChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationStarted"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnAnimationStarted(EventArgs eventArgs)
        {
            if (AnimationStarted != null)
                AnimationStarted(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationStopped"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnAnimationStopped(EventArgs eventArgs)
        {
            if (AnimationStopped != null)
                AnimationStopped(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationFinished"/> event.
        /// </summary>
        /// <param name="eventArgs">Event arguments.</param>
        protected virtual void OnAnimationFinished(EventArgs eventArgs)
        {
            if (AnimationFinished != null)
                AnimationFinished(this, eventArgs);
        }

        #endregion

        #endregion
    }
}