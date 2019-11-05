using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.EForm
{
    public partial class EForm : Form
    {
        #region (* PInvoke *)

        private const int NCLBUTTONDOWN = 0x00A1;
        private const int WM_SYSCOMMAND = 0x112;
        private const int HTCAPTION = 2;
        private const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        #endregion

        #region (* Private members *)

        private Rectangle m_rcTitleBar = new Rectangle();
        private Image m_bmpMenuIcon;
        private Image m_bmpBackImage;
        private ContentAlignment m_ImageAlign = ContentAlignment.TopLeft;

        private Rectangle m_rcETitleBarIconHolder = new Rectangle();
        private Rectangle m_rcSizeGrip = new Rectangle();
        private Rectangle m_rcXMenuIcon = new Rectangle();
        private Rectangle m_rcBox = new Rectangle();

        private Color m_clrBackground = Color.FromArgb(191, 219, 255);

        private Rectangle m_rcRestoreBounds = new Rectangle();
        private Rectangle m_rcIconHolder = new Rectangle();

        private int m_lTitleBarHeight = 35;

        private Rectangle m_rcTitleBarIcon = new Rectangle();
        private Rectangle m_rcXStatusBar = new Rectangle();

        private bool m_bMouseDown = false;
        private bool m_bMaximized = false;

        private ETitleBarIconHolder m_etbHolder = new ETitleBarIconHolder();
        private ETitleBar m_eTitleBar = new ETitleBar();
        private EStatusBar m_esbStatusBar = new EStatusBar();
        private E3DBorderPrimitive m_e3dx = new E3DBorderPrimitive();

        private GraphicsPath m_TitleBarButtonsBox = new GraphicsPath();

        private List<Color> m_MenuIconMix = new List<Color>();
        private Color m_clrMenuIconBorderInner = Color.FromArgb(246, 244, 252);
        private Color m_clrMenuIconBorderOuter = Color.FromArgb(154, 174, 213);

        #endregion

        #region EFormHolderButtonClickArgs

        public class EFormHolderButtonClickArgs : EventArgs
        {
            /// <summary>
            /// Button index.
            /// </summary>
            private int m_lIndex;

            public int ButtonIndex
            {
                get
                {
                    return m_lIndex;
                }
            }

            public EFormHolderButtonClickArgs(int lIndex)
                : base()
            {
                m_lIndex = lIndex;
            }
        }

        #endregion

        #region Events / Delegates

        public delegate void EFormHolderButtonClickHandler(EFormHolderButtonClickArgs e);

        public event EFormHolderButtonClickHandler EFormHolderButtonClick;

        #endregion

        #region Properties

        public ETitleBarIconHolder IconHolder
        {
            get
            {
                return this.m_etbHolder;
            }
        }

        public ETitleBar TitleBar
        {
            get
            {
                return this.m_eTitleBar;
            }
        }

        public EStatusBar StatusBar
        {
            get
            {
                return this.m_esbStatusBar;
            }
        }

        public E3DBorderPrimitive Border
        {
            get
            {
                return this.m_e3dx;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                m_eTitleBar.TitleBarCaption = value;
                this.Refresh();
            }
        }

        public new bool MaximizeBox
        {
            get
            {
                return base.MaximizeBox;
            }
            set
            {
                base.MaximizeBox = value;
                m_eTitleBar.MaximizeBox = value;
                this.Refresh();
            }
        }

        public new bool MinimizeBox
        {
            get
            {
                return base.MinimizeBox;
            }
            set
            {
                base.MinimizeBox = value;
                m_eTitleBar.MinimizeBox = value;
                this.Refresh();
            }
        }

        public EFormCtrlButtons.EButtonStyle ButtonStyle
        {
            get
            {
                return m_eTitleBar.ButtonStyle;
            }
            set
            {
                m_eTitleBar.ButtonStyle = value;
                this.Refresh();
            }
        }

        [DefaultValue(null)]
        public override Color BackColor
        {
            get
            {
                return this.m_clrBackground;
            }
            set
            {
                if (value != null)
                {
                    this.m_clrBackground = value;
                }
                else
                {
                    this.m_clrBackground = Color.FromArgb(191, 219, 255);
                }

                this.Refresh();
            }
        }

        [DefaultValue(null)]
        public Color MenuIconInnerBorder
        {
            get
            {
                return this.m_clrMenuIconBorderInner;
            }
            set
            {
                if (value != null)
                {
                    this.m_clrMenuIconBorderInner = value;
                }
                else
                {
                    this.m_clrMenuIconBorderInner = Color.FromArgb(246, 244, 252);
                }
                this.Refresh();
            }
        }

        [DefaultValue(null)]
        public Color MenuIconOuterBorder
        {
            get
            {
                return this.m_clrMenuIconBorderOuter;
            }
            set
            {
                if (value != null)
                {
                    this.m_clrMenuIconBorderOuter = value;
                }
                else
                {
                    this.m_clrMenuIconBorderOuter = Color.FromArgb(154, 174, 213);
                }
                this.Refresh();
            }
        }

        [DefaultValue(null)]
        public Image MenuIcon
        {
            get
            {
                return this.m_bmpMenuIcon;
            }
            set
            {
                if (value != null)
                {
                    this.m_bmpMenuIcon = value.GetThumbnailImage(28, 28, null, IntPtr.Zero);
                }
                else
                {
                    this.m_bmpMenuIcon = value;
                }
                this.Refresh();
            }
        }

        [Browsable(false)]
        public new Icon Icon { get; set; }

        [Browsable(false)]
        public new bool ShowIcon { get; set; }

        [DefaultValue(null)]
        public override Image BackgroundImage
        {
            get
            {
                return this.m_bmpBackImage;
            }
            set
            {
                this.m_bmpBackImage = value;
                this.Refresh();
            }
        }

        public List<Color> MenuIconMix
        {
            get
            {
                return this.m_MenuIconMix;
            }
            set
            {
                this.m_MenuIconMix = value;

                this.Refresh();
            }
        }

        #endregion

        public EForm()
        {
            InitializeComponent();

            // set control styles:
            this.SetStyle(
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.DoubleBuffer |
                 ControlStyles.UserPaint |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor,
                 true
            );
            this.FormBorderStyle = FormBorderStyle.None;
            this.MinimumSize = new Size(200, 100);
            this.Padding = new Padding(6, 36, 6, 30);

            // initialize titlebar buttons:
            m_eTitleBar.CtrlButtons.Add(new EFormCtrlButtons(EFormCtrlButtons.ECtrlType.Close));
            m_eTitleBar.CtrlButtons.Add(new EFormCtrlButtons(EFormCtrlButtons.ECtrlType.Maximize, Color.FromArgb(3, 63, 126), Color.FromArgb(119, 217, 246)));
            m_eTitleBar.CtrlButtons.Add(new EFormCtrlButtons(EFormCtrlButtons.ECtrlType.Minimize, Color.FromArgb(124, 13, 2), Color.FromArgb(251, 164, 48)));

            // initialize mix:
            m_MenuIconMix.Add(Color.FromArgb(227, 235, 247));
            m_MenuIconMix.Add(Color.FromArgb(221, 234, 251));
            m_MenuIconMix.Add(Color.FromArgb(205, 224, 248));
            m_MenuIconMix.Add(Color.FromArgb(217, 232, 250));
            m_MenuIconMix.Add(Color.FromArgb(223, 236, 252));

            m_etbHolder = new ETitleBarIconHolder(this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            bool bShouldReset = false;
            if (m_e3dx.BorderType != E3DBorderPrimitive.EBorderType.Rectangular)
            {
                e.Graphics.Clip = new Region(m_e3dx.FindX3DBorderPrimitive(this.ClientRectangle));
                bShouldReset = true;
                this.BackColor = Color.Fuchsia;
                this.TransparencyKey = Color.Fuchsia;
            }
            using (SolidBrush sb = new SolidBrush(m_clrBackground))
            {
                e.Graphics.FillRectangle(sb, this.ClientRectangle);
                if (bShouldReset)
                    e.Graphics.ResetClip();
            }

            if (m_bmpBackImage != null)
            {
                e.Graphics.DrawImage(m_bmpBackImage, this.ClientRectangle);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_bMouseDown = true;

            #region Titlebar buttons

            if (m_eTitleBar.ShouldDrawButtonBox)
            {
                foreach (EFormCtrlButtons ebtn in m_eTitleBar.CtrlButtons)
                {
                    if (m_TitleBarButtonsBox.IsVisible(e.Location))
                    {
                        if (PointInRect(e.Location, new Rectangle(ebtn.ButtonLeft, ebtn.ButtonTop, ebtn.ButtonWidth, ebtn.ButtonHeight)))
                        {
                            if (ebtn.ButtonType == EFormCtrlButtons.ECtrlType.Minimize && MinimizeBox)
                                this.WindowState = FormWindowState.Minimized;
                            else if (ebtn.ButtonType == EFormCtrlButtons.ECtrlType.Maximize && MaximizeBox)
                            {
                                if (m_bMaximized)
                                {
                                    m_bMaximized = false;
                                    this.Size = new Size(m_rcRestoreBounds.Width, m_rcRestoreBounds.Height);
                                    this.Location = new Point(m_rcRestoreBounds.Left, m_rcRestoreBounds.Top);
                                }
                                else
                                {
                                    m_rcRestoreBounds = new Rectangle(this.Location, this.Size);
                                    Rectangle wa = Screen.GetWorkingArea(this);
                                    this.Size = new Size(wa.Width, wa.Height);
                                    this.Location = new Point(wa.Left, wa.Top);
                                    m_bMaximized = true;
                                }
                            }
                            else if (ebtn.ButtonType == EFormCtrlButtons.ECtrlType.Close)
                            {
                                this.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (EFormCtrlButtons ebtn in m_eTitleBar.CtrlButtons)
                {
                    if (PointInRect(e.Location, new Rectangle(ebtn.ButtonLeft, ebtn.ButtonTop, ebtn.ButtonWidth, ebtn.ButtonHeight)))
                    {
                        if (ebtn.ButtonType == EFormCtrlButtons.ECtrlType.Minimize && MinimizeBox)
                            this.WindowState = FormWindowState.Minimized;
                        else if (ebtn.ButtonType == EFormCtrlButtons.ECtrlType.Maximize && MaximizeBox)
                        {
                            if (m_bMaximized)
                            {
                                m_bMaximized = false;
                                this.Size = new Size(m_rcRestoreBounds.Width, m_rcRestoreBounds.Height);
                                this.Location = new Point(m_rcRestoreBounds.Left, m_rcRestoreBounds.Top);
                            }
                            else
                            {
                                m_rcRestoreBounds = new Rectangle(this.Location, this.Size);
                                Rectangle wa = Screen.GetWorkingArea(this);
                                this.Size = new Size(wa.Width, wa.Height);
                                this.Location = new Point(wa.Left, wa.Top);
                                m_bMaximized = true;
                            }
                        }
                        else if (ebtn.ButtonType == EFormCtrlButtons.ECtrlType.Close)
                        {
                            this.Close();
                        }
                    }
                }
            }

            #endregion

            #region Titlebar icon holder

            // mouse over TitleBarIconHolder :
            if (PointInRect(e.Location, m_rcETitleBarIconHolder))
            {
                if (e.Button == MouseButtons.Left)
                {
                    // find hovering button:
                    int lIdx = m_etbHolder.HitTestHolderButton(e.X, e.Y, m_rcETitleBarIconHolder);

                    for (int i = 0; i < m_etbHolder.HolderButtons.Count; i++)
                    {
                        if (i == lIdx)
                        {
                            EFormHolderButtonClickArgs EHolderButton = new EFormHolderButtonClickArgs(i);
                            if (EFormHolderButtonClick != null)
                                EFormHolderButtonClick(EHolderButton);
                        }
                    }
                }
            }

            #endregion
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                m_bMouseDown = false;
        }

        protected override void OnResizeBegin(EventArgs e)
        {
            this.Invalidate();
            base.OnResizeBegin(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            #region TitleBarIconHolder

            // mouse over TitleBarIconHolder ?:
            if (PointInRect(e.Location, m_rcETitleBarIconHolder))
            {
                // find hovering button:
                int lIdx = m_etbHolder.HitTestHolderButton(e.X, e.Y, m_rcETitleBarIconHolder);

                for (int i = 0; i < m_etbHolder.HolderButtons.Count; i++)
                {
                    if (i == lIdx)
                    {
                        if (!m_etbHolder.HolderButtons[i].Hot)
                        {
                            m_etbHolder.HolderButtons[i].Hot = true;
                            Invalidate(m_rcETitleBarIconHolder);
                        }
                    }
                    else
                    {
                        if (m_etbHolder.HolderButtons[i].Hot)
                        {
                            m_etbHolder.HolderButtons[i].Hot = false;
                            Invalidate(m_rcETitleBarIconHolder);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < m_etbHolder.HolderButtons.Count; i++)
                {
                    if (m_etbHolder.HolderButtons[i].Hot)
                    {
                        m_etbHolder.HolderButtons[i].Hot = false;
                        Invalidate(m_rcETitleBarIconHolder);
                    }
                }
            }

            #endregion

            #region TitleBar buttons

            HitTestTitleBarButtons(e.Location);

            #endregion

            #region Form moving

            HitTestMoveTitleBar(e);

            #endregion

            #region Sizing

            ResizeWindow(e);

            #endregion

            base.OnMouseMove(e);
        }

        private void ResizeWindow(MouseEventArgs e)
        {
            bool bResizing = true;

            if (PointInRect(e.Location, m_rcSizeGrip))
            {
                Cursor = Cursors.SizeNWSE;
                if (m_bMouseDown && bResizing)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (this.Width < this.MinimumSize.Width)
                            bResizing = false;
                        if (this.Height < this.MinimumSize.Height)
                            bResizing = false;

                        ReleaseCapture();
                        SendMessage(this.Handle, WM_SYSCOMMAND, (IntPtr)(SC_SIZE + 8), IntPtr.Zero);
                    }
                }
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void HitTestMoveTitleBar(MouseEventArgs e)
        {
            if (m_bMouseDown)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (PointInRect(e.Location, m_rcTitleBar))
                    {
                        ReleaseCapture();
                        SendMessage(this.Handle, NCLBUTTONDOWN, (IntPtr)HTCAPTION, IntPtr.Zero);
                    }
                }
            }
        }

        private void HitTestTitleBarButtons(Point pos)
        {
            bool bChanged = false;

            if (m_eTitleBar.ShouldDrawButtonBox)
            {
                if (m_TitleBarButtonsBox.IsVisible(pos))
                {
                    foreach (EFormCtrlButtons ebtn in m_eTitleBar.CtrlButtons)
                    {
                        if (PointInRect(pos, new Rectangle(ebtn.ButtonLeft, ebtn.ButtonTop, ebtn.ButtonWidth, ebtn.ButtonHeight)))
                        {
                            if (!ebtn.Hovering)
                            {
                                ebtn.Hovering = true;
                                bChanged = true;
                            }
                        }
                        else
                        {
                            if (ebtn.Hovering)
                            {
                                ebtn.Hovering = false;
                                bChanged = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (EFormCtrlButtons xbtn in m_eTitleBar.CtrlButtons)
                    {
                        if (xbtn.Hovering)
                        {
                            xbtn.Hovering = false;
                            bChanged = true;
                        }
                    }
                }
            }
            else
            {
                foreach (EFormCtrlButtons ebtn in m_eTitleBar.CtrlButtons)
                {
                    if (PointInRect(pos, new Rectangle(ebtn.ButtonLeft, ebtn.ButtonTop, ebtn.ButtonWidth, ebtn.ButtonHeight)))
                    {
                        if (!ebtn.Hovering)
                        {
                            ebtn.Hovering = true;
                            Invalidate(new Rectangle(ebtn.ButtonLeft, ebtn.ButtonTop, ebtn.ButtonWidth, ebtn.ButtonHeight));
                        }
                    }
                    else
                    {
                        if (ebtn.Hovering)
                        {
                            ebtn.Hovering = false;
                            Invalidate(new Rectangle(ebtn.ButtonLeft, ebtn.ButtonTop, ebtn.ButtonWidth, ebtn.ButtonHeight));
                        }
                    }
                }
            }
            if (bChanged)
            {
                Invalidate(m_rcBox);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            m_rcTitleBarIcon = new Rectangle(7, 5, 40, 40);
            Rectangle rcBorder = new Rectangle(0, 0, this.ClientRectangle.Width - 1, this.ClientRectangle.Height - 1);

            DrawStatusBar(e.Graphics);

            // render border:
            m_e3dx.Render(rcBorder, e.Graphics);

            DrawSysIcon(e.Graphics);
            DrawButtonsBox(e.Graphics);
            DrawTitleBar(e.Graphics);

            // build titlebar buttons box:
            m_TitleBarButtonsBox = m_eTitleBar.BuildCtrlButtonsBox(m_rcBox);

            // render holder buttons:
            m_etbHolder.RenderHolderButtons(m_rcIconHolder.X, m_rcIconHolder.Y, e.Graphics);
        }

        private void DrawButtonsBox(Graphics g)
        {
            int lBoxTop = 0;
            int lBtnWidth = 0;
            int lBtnHeight = 0;
            int lBorder = 6;
            int lBoxWidth = 0;
            int lBoxHeight = 0;
            int x = this.ClientRectangle.Right - lBorder - 24;
            int y = 4;
            if (m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Angular || m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Rectangular)
                lBoxTop += 4;

            foreach (EFormCtrlButtons btn in m_eTitleBar.CtrlButtons)
            {
                lBtnWidth = btn.ButtonWidth;
                lBtnHeight = btn.ButtonHeight;
                break;
            }
            //if (m_eTitleBar.ShouldDrawButtonBox)
            //    lBoxWidth = lBtnWidth * 3 - 1;
            //else
            //    lBoxWidth = 60;
            lBoxWidth = lBtnWidth * 3 - 1;
            lBoxHeight = lBtnHeight;

            m_rcBox = new Rectangle(ClientRectangle.Right - lBorder - lBoxWidth, lBoxTop, lBoxWidth, lBoxHeight);

            m_eTitleBar.RenderCtrlButtonsBox(m_rcBox, g, x, y);
        }

        private void DrawSysIcon(Graphics g)
        {
            int lLeft = 6; int lTop = 3;
            int lWidth = 47;
            int lHeight = m_lTitleBarHeight - 6;
            m_rcXMenuIcon = new Rectangle(lLeft, lTop, lWidth, lHeight);
            RenderSysMenuIcon(m_rcXMenuIcon, g);
        }

        private void DrawStatusBar(Graphics g)
        {
            int lBorderExcess = 7;
            if (m_e3dx.BorderStyle == E3DBorderPrimitive.EBorderStyle.Flat)
                lBorderExcess = 2;

            m_rcXStatusBar = new Rectangle(1, ClientRectangle.Bottom - lBorderExcess - m_esbStatusBar.BarHeight, ClientRectangle.Right - ClientRectangle.Left, m_esbStatusBar.BarHeight);
            m_esbStatusBar.RenderStatusBar(g, m_rcXStatusBar.Left, m_rcXStatusBar.Top, m_rcXStatusBar.Width, m_rcXStatusBar.Height);
            m_rcSizeGrip = m_esbStatusBar.EGripRect;
        }

        private void DrawTitleBar(Graphics g)
        {
            int lTitleBarWidth = m_rcBox.Left - m_rcXMenuIcon.Width - 12;
            int lTopOffset = 5;
            int lRectOffset = m_rcXMenuIcon.Right - 2;

            if (m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Angular)
            {
                lTitleBarWidth += 25;
            }
            if (m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Rounded)
            {
                lTitleBarWidth -= 10;
            }
            if (m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Rectangular)
            {
                lRectOffset += 5;
            }

            m_rcTitleBar = new Rectangle(lRectOffset, lTopOffset, lTitleBarWidth, 25);
            int lIconHolderOffset = m_rcTitleBar.Left + 4;
            if (m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Angular)
            {
                lIconHolderOffset += 15;
            }
            if (m_eTitleBar.TitleBarType == ETitleBar.ETitleBarType.Rounded)
            {
                lIconHolderOffset += 4;
            }

            m_rcETitleBarIconHolder = new Rectangle(55, m_rcTitleBar.Top + 3, 255, m_lTitleBarHeight + 60);
            m_rcIconHolder = new Rectangle(lIconHolderOffset, 7, 200, 400);

            m_eTitleBar.RenderTitleBar(g, m_rcTitleBar);
        }

        private void RenderSysMenuIcon(Rectangle rcMenuIcon, Graphics g)
        {
            using (GraphicsPath EMenuIconPath = BuildMenuIconShape(ref rcMenuIcon))
            {
                if (m_MenuIconMix == null || m_MenuIconMix.Count != 5)
                {
                    m_MenuIconMix = new List<Color>();
                    m_MenuIconMix.Add(Color.FromArgb(227, 235, 247));
                    m_MenuIconMix.Add(Color.FromArgb(221, 234, 251));
                    m_MenuIconMix.Add(Color.FromArgb(205, 224, 248));
                    m_MenuIconMix.Add(Color.FromArgb(217, 232, 250));
                    m_MenuIconMix.Add(Color.FromArgb(223, 236, 252));
                }
                FillMenuIconGradient(EMenuIconPath, g, m_MenuIconMix);

                using (EAntiAlias xaa = new EAntiAlias(g))
                {
                    DrawInnerMenuIconBorder(rcMenuIcon, g, m_clrMenuIconBorderInner);
                    g.DrawPath(new Pen(m_clrMenuIconBorderOuter), EMenuIconPath);
                }
            }

            #region Draw icon

            if (m_bmpMenuIcon != null)
            {
                int lH = m_bmpMenuIcon.Height;
                int lW = m_bmpMenuIcon.Width;

                Rectangle rcImage = new Rectangle((rcMenuIcon.Right - rcMenuIcon.Width / 2) - lW / 2 - 2, (rcMenuIcon.Bottom - rcMenuIcon.Height / 2) - lH / 2, lW, lH);
                g.DrawImage(m_bmpMenuIcon, rcImage);
            }

            #endregion
        }

        private void FillMenuIconGradient(GraphicsPath XFillPath, Graphics g, List<Color> mix)
        {
            using (EAntiAlias xaa = new EAntiAlias(g))
            {
                using (LinearGradientBrush lgb = new LinearGradientBrush(XFillPath.GetBounds(), mix[0], mix[4], LinearGradientMode.Vertical))
                {
                    lgb.InterpolationColors = EFormHelper.ColorMix(mix, false);

                    g.FillPath(lgb, XFillPath);
                }
            }
        }

        private GraphicsPath BuildMenuIconShape(ref Rectangle rcMenuIcon)
        {
            GraphicsPath EMenuIconPath = new GraphicsPath();
            switch (m_eTitleBar.TitleBarType)
            {
                case ETitleBar.ETitleBarType.Rounded:
                    EMenuIconPath.AddArc(rcMenuIcon.Left, rcMenuIcon.Top, rcMenuIcon.Height, rcMenuIcon.Height, 90, 180);
                    EMenuIconPath.AddLine(rcMenuIcon.Left + rcMenuIcon.Height / 2, rcMenuIcon.Top, rcMenuIcon.Right, rcMenuIcon.Top);
                    EMenuIconPath.AddBezier(
                    new Point(rcMenuIcon.Right, rcMenuIcon.Top),
                    new Point(rcMenuIcon.Right - 10, rcMenuIcon.Bottom / 2 - 5),
                    new Point(rcMenuIcon.Right - 12, rcMenuIcon.Bottom / 2 + 5),
                    new Point(rcMenuIcon.Right, rcMenuIcon.Bottom)
                    );
                    EMenuIconPath.AddLine(rcMenuIcon.Right, rcMenuIcon.Bottom, rcMenuIcon.Left + rcMenuIcon.Height / 2, rcMenuIcon.Bottom);
                    break;

                case ETitleBar.ETitleBarType.Angular:
                    EMenuIconPath.AddArc(rcMenuIcon.Left, rcMenuIcon.Top, rcMenuIcon.Height, rcMenuIcon.Height, 90, 180);
                    EMenuIconPath.AddLine(rcMenuIcon.Left + rcMenuIcon.Height / 2, rcMenuIcon.Top, rcMenuIcon.Right + 18, rcMenuIcon.Top);
                    EMenuIconPath.AddLine(rcMenuIcon.Right + 18, rcMenuIcon.Top, rcMenuIcon.Right - 5, rcMenuIcon.Bottom);
                    EMenuIconPath.AddLine(rcMenuIcon.Right - 5, rcMenuIcon.Bottom, rcMenuIcon.Left + rcMenuIcon.Height / 2, rcMenuIcon.Bottom);
                    break;

                case ETitleBar.ETitleBarType.Rectangular:
                    EMenuIconPath.AddArc(rcMenuIcon.Left, rcMenuIcon.Top, rcMenuIcon.Height, rcMenuIcon.Height, 90, 180);
                    EMenuIconPath.AddLine(rcMenuIcon.Left + rcMenuIcon.Height / 2, rcMenuIcon.Top, rcMenuIcon.Right, rcMenuIcon.Top);
                    EMenuIconPath.AddLine(rcMenuIcon.Right, rcMenuIcon.Top, rcMenuIcon.Right, rcMenuIcon.Bottom);
                    EMenuIconPath.AddLine(rcMenuIcon.Right, rcMenuIcon.Bottom, rcMenuIcon.Left + rcMenuIcon.Height / 2, rcMenuIcon.Bottom);
                    break;
            }
            return EMenuIconPath;
        }

        private void DrawInnerMenuIconBorder(Rectangle rcMenuIcon, Graphics g, Color clr)
        {
            rcMenuIcon.Inflate(-1, -1);
            using (GraphicsPath XMenuIconPath = BuildMenuIconShape(ref rcMenuIcon))
            {
                using (Pen pInner = new Pen(clr))
                {
                    g.DrawPath(
                      pInner,
                      XMenuIconPath
                    );
                }
            }
        }

        /// <summary>
        /// Checks if point is inside specific rectangle.
        /// </summary>
        /// <param name="p">Point to check.</param>
        /// <param name="rc">Rectangle area.</param>
        /// <returns></returns>
        private bool PointInRect(Point p, Rectangle rc)
        {
            if ((p.X > rc.Left && p.X < rc.Right && p.Y > rc.Top && p.Y < rc.Bottom))
                return true;
            else
                return false;
        }

        private void DrawBackImage(Graphics gfx, Rectangle rc)
        {
            if (m_bmpBackImage != null)
            {
                int lW = m_bmpBackImage.Width;
                int lH = m_bmpBackImage.Height;
                Rectangle rcImage = new Rectangle(0, 0, lW, lH);

                switch (m_ImageAlign)
                {
                    case ContentAlignment.BottomCenter:
                        rcImage.X = rc.Width / 2 - lW / 2;
                        rcImage.Y = rc.Height - lH - 2;
                        break;

                    case ContentAlignment.BottomLeft:
                        rcImage.X = rc.Left + 2;
                        rcImage.Y = rc.Height - lH - 2;
                        break;

                    case ContentAlignment.BottomRight:
                        rcImage.X = rc.Right - lW - 2;
                        rcImage.Y = rc.Height - lH - 2;
                        break;

                    case ContentAlignment.MiddleCenter:
                        rcImage.X = rc.Width / 2 - lW / 2;
                        rcImage.Y = rc.Height / 2 - lH / 2;
                        break;

                    case ContentAlignment.MiddleLeft:
                        rcImage.X = rc.Left + 2;
                        rcImage.Y = rc.Height / 2 - lH / 2;
                        break;

                    case ContentAlignment.MiddleRight:
                        rcImage.X = rc.Right - lW - 2;
                        rcImage.Y = rc.Height / 2 - lH / 2;
                        break;

                    case ContentAlignment.TopCenter:
                        rcImage.X = rc.Width / 2 - lW / 2;
                        rcImage.Y = rc.Top + 2;
                        break;

                    case ContentAlignment.TopLeft:
                        rcImage.X = rc.Left + 2;
                        rcImage.Y = rc.Top + 2;
                        break;

                    case ContentAlignment.TopRight:
                        rcImage.X = rc.Right - lW - 2;
                        rcImage.Y = rc.Top + 2;
                        break;
                }

                gfx.DrawImage(m_bmpBackImage, rcImage);
            }
        }
    }
}