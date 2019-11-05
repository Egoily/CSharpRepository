/*---------------------------------------------------------------------------
 * File: LBLed.cs
 * Utente: lucabonotto
 * Date: 05/04/2009
 *-------------------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using EgoDevil.Utilities.UI.IndustrialCtrls.Base;

namespace EgoDevil.Utilities.UI.IndustrialCtrls.Leds
{
    /// <summary>
    /// Class for the Led control.
    /// </summary>
    [DefaultEvent("Click")]
    public partial class LBLed : LBIndustrialCtrlBase
    {
        #region (* Enumeratives *)

        public enum LedState
        {
            Off = 0,
            On,
            Blink,
        }

        public enum LedLabelPosition
        {
            Left = 0,
            Top,
            Right,
            Bottom,
        }

        public enum LedStyle
        {
            Circular = 0,
            Rectangular,
        }

        #endregion

        #region (* Properties variables *)

        private Color ledColor;
        private LedState state;
        private LedStyle style;
        private LedLabelPosition labelPosition;
        private String label = "Led";
        private SizeF ledSize;
        private int blinkInterval = 500;

        #endregion

        #region (* Class variables *)

        private Timer tmrBlink;
        private bool blinkIsOn = false;

        #endregion

        #region (* Constructor *)

        public LBLed()
        {
            InitializeComponent();

            this.Size = new Size(20, 20);
            this.ledColor = Color.Red;
            this.state = LedState.Off;
            this.style = LedStyle.Circular;
            this.blinkIsOn = false;
            this.ledSize = new SizeF(10F, 10F);
            this.labelPosition = LedLabelPosition.Top;
        }

        #endregion

        #region (* Properties *)

        [
            Category("Led"),
            Description("Style of the led")
        ]
        public LedStyle Style
        {
            get { return style; }
            set
            {
                style = value;
                this.CalculateDimensions();
            }
        }

        [
            Category("Led"),
            Description("Color of the led")
        ]
        public Color LedColor
        {
            get { return ledColor; }
            set
            {
                ledColor = value;
                Invalidate();
            }
        }

        [
            Category("Led"),
            Description("State of the led")
        ]
        public LedState State
        {
            get { return state; }
            set
            {
                LedState tmp = state;
                state = value;
                if (state == LedState.Blink)
                {
                    this.blinkIsOn = true;
                    this.tmrBlink.Interval = this.BlinkInterval;
                    this.tmrBlink.Start();
                }
                else
                {
                    this.blinkIsOn = true;
                    this.tmrBlink.Stop();
                }

                Invalidate();

                OnStateChanged(new StateEventArgs(tmp, state));
            }
        }

        [
            Category("Led"),
            Description("Size of the led")
        ]
        public SizeF LedSize
        {
            get { return this.ledSize; }
            set
            {
                this.ledSize = value;
                this.CalculateDimensions();
                Invalidate();
            }
        }

        [
            Category("Led"),
            Description("Label of the led")
        ]
        public String Label
        {
            get { return this.label; }
            set
            {
                this.label = value;
                Invalidate();
            }
        }

        [
            Category("Led"),
            Description("Position of the label of the led")
        ]
        public LedLabelPosition LabelPosition
        {
            get { return this.labelPosition; }
            set
            {
                this.labelPosition = value;
                this.CalculateDimensions();
                Invalidate();
            }
        }

        [
            Category("Led"),
            Description("Interval for the blink state of the led")
        ]
        public int BlinkInterval
        {
            get { return this.blinkInterval; }
            set { this.blinkInterval = value; }
        }

        [Browsable(false)]
        public bool BlinkIsOn
        {
            get { return this.blinkIsOn; }
        }

        #endregion

        #region (* Events delegates *)

        private void OnBlink(object sender, EventArgs e)
        {
            if (this.State == LedState.Blink)
            {
                if (this.blinkIsOn == false)
                    this.blinkIsOn = true;
                else
                    this.blinkIsOn = false;

                this.Invalidate();
            }
        }

        public delegate void StateChanged(object sender, StateEventArgs e);

        public class StateEventArgs : EventArgs
        {
            public LedState BeforeChanged;
            public LedState AfterChanged;

            public StateEventArgs(LedState state1, LedState state2)
            {
                BeforeChanged = state1;
                AfterChanged = state2;
            }
        }

        public event StateChanged LedStateChanged;

        private void OnStateChanged(StateEventArgs e)
        {
            if (this.LedStateChanged != null)
                this.LedStateChanged(this, e);
        }

        #endregion

        #region (* Overrided methods *)

        protected override ILBRenderer CreateDefaultRenderer()
        {
            return new LBLedRenderer();
        }

        #endregion
    }
}