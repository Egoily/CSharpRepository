using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.ActivityBar
{
    /// <summary>
    /// An XP style Activity Bar.
    /// </summary>
    public partial class ActivityBar : UserControl
    {
        private ColorBlend colorBlend;
        private Timer timer;
        private int tickCount;

        private Color _barColor = Color.Orange;

        /// <summary>
        /// Determines the main color for the activity bar
        /// </summary>
        [Description("Determines the main color for the activity bar."),
        Category("Appearance")]
        public Color BarColor
        {
            get { return _barColor; }
            set
            {
                _barColor = value;
                ColorPropertyChanged();
            }
        }

        private Color _borderColor = SystemColors.ActiveBorder;

        /// <summary>
        /// Determines the color for the activity bar border
        /// </summary>
        [Description("Determines the color for the activity bar border."),
        Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                ColorPropertyChanged();
            }
        }

        private Color _highlightColor = Color.White;

        /// <summary>
        /// Determines the highlight color for the activity bar
        /// </summary>
        [Description("Determines the highlight color for the activity bar."),
        Category("Appearance")]
        public Color HighlightColor
        {
            get { return _highlightColor; }
            set
            {
                _highlightColor = value;
                ColorPropertyChanged();
            }
        }

        [Description(" The status of the activity bar."),
        Category("Appearance"),
        DefaultValue(true)]
        public bool Status
        {
            get { return timer.Enabled; }
            set
            {
                timer.Enabled = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ActivityBar()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // set double buffer styles
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);

            this.BackColor = Color.Transparent;
            UpdateStyles();

            BuildColorBlend();

            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
            this.EnabledChanged += new EventHandler(ActivityBar_EnabledChanged);
            Status = true;
        }

        private void BuildColorBlend()
        {
            colorBlend = new ColorBlend(7);
            colorBlend.Colors = new Color[] {
										 _barColor,
										 _barColor,
										 _barColor,
										 _highlightColor,
										 _highlightColor,
										 _barColor,
										 _barColor};

            colorBlend.Positions = new float[] {
										   0.0f,
										   0.01f,
										   0.2f,
										   0.4f,
										   0.6f,
										   0.8f,
										   1.0f};
        }

        private void ColorPropertyChanged()
        {
            BuildColorBlend();
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            LinearGradientBrush lBrush = new LinearGradientBrush(this.ClientRectangle, Color.White, Color.Orange,
                LinearGradientMode.Horizontal);
            lBrush.InterpolationColors = colorBlend;
            g.FillRectangle(lBrush, 1, 1, this.Width - 2, this.Height - 2);
            Brush b = new SolidBrush(_borderColor);
            Pen p = new Pen(b, 1);
            g.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
            p.Dispose();
            b.Dispose();
            lBrush.Dispose();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Make sure the background isn't erased, to reduce flicker

            //base.OnPaintBackground (pevent);
        }

        /// <summary>
        /// Performs the color rotation in the activity bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            // Each color segment gets "scrolled" 20 positions to the right then the colors get
            // moved to the right in the colorblend array and the deltas reset
            if (++tickCount >= 20)
            {
                tickCount = 0;
                Color last = colorBlend.Colors[6];
                for (int i = 6; i > 0; i--)
                {
                    colorBlend.Colors[i] = colorBlend.Colors[i - 1];
                }
                colorBlend.Colors[0] = last;
            }

            // get color advance delta
            float f = tickCount / 100.0f;

            // advance colors
            colorBlend.Positions[1] = 0.01f + f;
            colorBlend.Positions[2] = 0.2f + f;
            colorBlend.Positions[3] = 0.4f + f;
            colorBlend.Positions[4] = 0.6f + f;
            colorBlend.Positions[5] = 0.8f + f;
            //redraw controls
            this.Refresh();
        }

        private void ActivityBar_EnabledChanged(object sender, EventArgs e)
        {
            timer.Enabled = this.Enabled;
        }
    }
}