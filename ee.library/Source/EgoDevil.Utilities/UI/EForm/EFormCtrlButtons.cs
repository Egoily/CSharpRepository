using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EgoDevil.Utilities.UI.EForm
{
    public class EFormCtrlButtons
    {
        public enum ECtrlType
        {
            /// <summary>
            /// Minimize button.
            /// </summary>
            Minimize,

            /// <summary>
            /// Maximize button.
            /// </summary>
            Maximize,

            /// <summary>
            /// Close button.
            /// </summary>
            Close
        }

        public enum EButtonStyle
        {
            /// <summary>
            /// Button symbol is drawn using graphics primitives.
            /// </summary>
            Pixeled,

            /// <summary>
            /// Custom button symbol using bitmap. Will be implemented in next release.
            /// </summary>
            Bitmap,

            /// <summary>
            /// Rounded mac style buttons.
            /// </summary>
            MacStyle
        }

        public enum EButtonFillMode
        {
            /// <summary>
            /// Only button symbol is highlighted.
            /// </summary>
            None,

            /// <summary>
            /// Renders upper glow.
            /// </summary>
            UpperGlow,

            /// <summary>
            /// Full button rendering.
            /// </summary>
            FullFill
        }

        /// <summary>
        /// Button's top offset.
        /// </summary>
        private int m_lTop = 0;

        /// <summary>
        /// Button's left offset.
        /// </summary>
        private int m_lLeft = 0;

        /// <summary>
        /// Button's width.
        /// </summary>
        private int m_lButtonWidth = 26;

        /// <summary>
        /// Button's height.
        /// </summary>
        private int m_lButtonHeight = 26;

        /// <summary>
        /// Button type that will be rendered.
        /// </summary>
        private ECtrlType m_eButtonType = ECtrlType.Minimize;

        /// <summary>
        /// Indicates if mouse is over a button.
        /// </summary>
        private bool m_bHovering = false;

        private bool m_bVisible = true;

        /// <summary>
        /// Symbol of a button.
        /// </summary>
        private EButtonStyle m_eButtonStyle = EButtonStyle.Pixeled;

        /// <summary>
        /// Button fill mode.
        /// </summary>
        private EButtonFillMode m_eFillMode = EButtonFillMode.None;

        /// <summary>
        /// Color of a button symbol when button is rendered in pixeled mode.
        /// </summary>
        private Color m_clrButtonType = Color.FromArgb(152, 155, 162);

        /// <summary>
        /// Color of a button symbol when button is being hovered.
        /// </summary>
        private Color m_clrButtonTypeHover = Color.White;

        /// <summary>
        /// Color of a button used in full mode filling
        /// </summary>
        private Color m_clrRectStart = Color.FromArgb(69, 121, 0);

        /// <summary>
        /// Color of a button used in full mode filling.
        /// </summary>
        private Color m_clrRectEnd = Color.FromArgb(214, 250, 23);

        /// <summary>
        /// Titlebar containing the buttons.
        /// </summary>
        private ETitleBar m_xTbar = new ETitleBar();

        public ECtrlType ButtonType
        {
            get
            {
                return this.m_eButtonType;
            }
            set
            {
                this.m_eButtonType = value;
            }
        }

        [DefaultValue(true)]
        public bool Visible
        {
            get
            {
                return m_bVisible;
            }
            set
            {
                m_bVisible = value;
            }
        }

        public int ButtonTop
        {
            get
            {
                return this.m_lTop;
            }
            set
            {
                this.m_lTop = value;
            }
        }

        public int ButtonLeft
        {
            get
            {
                return this.m_lLeft;
            }
            set
            {
                this.m_lLeft = value;
            }
        }

        public int ButtonWidth
        {
            get
            {
                return this.m_lButtonWidth;
            }
            set
            {
                this.m_lButtonWidth = value;
            }
        }

        public int ButtonHeight
        {
            get
            {
                return this.m_lButtonHeight;
            }
            set
            {
                this.m_lButtonHeight = value;
            }
        }

        public bool Hovering
        {
            get
            {
                return this.m_bHovering;
            }
            set
            {
                this.m_bHovering = value;
            }
        }

        public EButtonFillMode ButtonFillMode
        {
            get
            {
                return this.m_eFillMode;
            }
            set
            {
                this.m_eFillMode = value;
            }
        }

        public EButtonStyle ButtonStyle
        {
            get
            {
                return this.m_eButtonStyle;
            }
            set
            {
                this.m_eButtonStyle = value;
            }
        }

        public Color ButtonSymbolColor
        {
            get
            {
                return this.m_clrButtonType;
            }
            set
            {
                this.m_clrButtonType = value;
            }
        }

        public Color ButtonSymbolHoverColor
        {
            get
            {
                return this.m_clrButtonTypeHover;
            }
            set
            {
                this.m_clrButtonTypeHover = value;
            }
        }

        public Color FillColorOne
        {
            get
            {
                return this.m_clrRectStart;
            }
            set
            {
                this.m_clrRectStart = value;
            }
        }

        public Color FillColorTwo
        {
            get
            {
                return this.m_clrRectEnd;
            }
            set
            {
                this.m_clrRectEnd = value;
            }
        }

        public EFormCtrlButtons()
        {
        }

        public EFormCtrlButtons(ECtrlType type)
        {
            m_eButtonType = type;
        }

        public EFormCtrlButtons(ECtrlType type, Color clrStart, Color clrEnd)
        {
            m_eButtonType = type;
            m_clrRectStart = clrStart;
            m_clrRectEnd = clrEnd;
        }

        /// <summary>
        /// Main button rendering method. -
        /// </summary>
        /// <param name="x">X coordinate of a button</param>
        /// <param name="y">Y coordinate of a button</param>
        /// <param name="g">Graphics object.</param>
        /// <param name="XBoxClip">Clipping path.</param>
        public void RenderCtrlButtons(int x, int y, Graphics g, GraphicsPath XBoxClip)
        {
            if (!Visible)
            {
                return;
            }
            Pen pen = new Pen(m_clrButtonType);
            Rectangle rcBtn = new Rectangle(x, y, m_lButtonWidth, m_lButtonHeight);
            if (m_bHovering)
            {
                if (m_eButtonStyle == EButtonStyle.Pixeled)
                    FillButton(rcBtn, g, m_clrRectStart, m_clrRectEnd, XBoxClip);
            }

            if (m_bHovering)
                pen = new Pen(m_clrButtonTypeHover);
            else
                pen = new Pen(m_clrButtonType);

            switch (m_eButtonStyle)
            {
                case EButtonStyle.Pixeled:
                    {
                        #region Pixeled button

                        g.SmoothingMode = SmoothingMode.None;
                        switch (m_eButtonType)
                        {
                            case ECtrlType.Close:
                                Rectangle rcClose = new Rectangle((rcBtn.Right - rcBtn.Width / 2) - 6, (rcBtn.Bottom - rcBtn.Height / 2) - 3, 8, 6);
                                if (m_xTbar.TitleBarType == ETitleBar.ETitleBarType.Angular)
                                {
                                    rcClose.X -= 1;
                                    rcClose.Y += 1;
                                }
                                g.DrawLine(pen, (rcClose.Right - rcClose.Width / 2), (rcClose.Bottom - rcClose.Height / 2) - 2, (rcClose.Right - rcClose.Width / 2) + 1, (rcClose.Bottom - rcClose.Height / 2) - 2);
                                g.DrawLine(pen, (rcClose.Right - rcClose.Width / 2) + 1, (rcClose.Bottom - rcClose.Height / 2) - 1, (rcClose.Right - rcClose.Width / 2) + 2, (rcClose.Bottom - rcClose.Height / 2) - 1);
                                g.FillRectangle(pen.Brush, new Rectangle((rcClose.Right - rcClose.Width / 2) - 2, (rcClose.Bottom - rcClose.Height / 2) - 4, 2, 2));
                                g.FillRectangle(pen.Brush, new Rectangle((rcClose.Right - rcClose.Width / 2) + 3, (rcClose.Bottom - rcClose.Height / 2), 2, 2));
                                g.FillRectangle(pen.Brush, new Rectangle((rcClose.Right - rcClose.Width / 2) + 3, (rcClose.Bottom - rcClose.Height / 2) - 4, 2, 2));
                                g.FillRectangle(pen.Brush, new Rectangle((rcClose.Right - rcClose.Width / 2) - 2, (rcClose.Bottom - rcClose.Height / 2), 2, 2));
                                break;

                            case ECtrlType.Maximize:
                                Rectangle rcMax = new Rectangle((rcBtn.Right - rcBtn.Width / 2) - 4, (rcBtn.Bottom - rcBtn.Height / 2) - 4, 8, 6);
                                if (m_xTbar.TitleBarType == ETitleBar.ETitleBarType.Angular)
                                    rcMax.Y += 1;

                                g.DrawLine(pen, rcMax.Right, rcMax.Top, rcMax.Right - 3, rcMax.Top);
                                pen.Width = 2;
                                g.DrawLine(pen, rcMax.Right, rcMax.Top + 1, rcMax.Right, rcMax.Top + 2);
                                g.DrawLine(pen, rcMax.Right, rcMax.Top + 3, rcMax.Right, rcMax.Bottom);
                                pen.Width = 1;
                                g.DrawLine(pen, rcMax.Right - 2, rcMax.Bottom - 1, rcMax.Right - 3, rcMax.Bottom - 1);
                                g.DrawLine(pen, rcMax.Right - 5, rcMax.Bottom - 1, rcMax.Left, rcMax.Bottom - 1);
                                pen.Width = 2;
                                g.DrawLine(pen, rcMax.Left, rcMax.Bottom, rcMax.Left, rcMax.Bottom - 3);
                                g.DrawLine(pen, rcMax.Left, rcMax.Bottom - 4, rcMax.Left, rcMax.Top);
                                g.DrawLine(pen, rcMax.Left, rcMax.Top + 1, rcMax.Left, rcMax.Top + 2);
                                pen.Width = 1;
                                g.DrawLine(pen, rcMax.Left + 1, rcMax.Top, rcMax.Left + 3, rcMax.Top);
                                break;

                            case ECtrlType.Minimize:
                                Rectangle rcMin = new Rectangle((rcBtn.Right - rcBtn.Width / 2) - 5, (rcBtn.Bottom - rcBtn.Height / 2) - 4, 8, 6);
                                if (m_xTbar.TitleBarType == ETitleBar.ETitleBarType.Angular)
                                {
                                    rcMin.X += 1;
                                    rcMin.Y += 2;
                                }
                                else if (m_xTbar.TitleBarType == ETitleBar.ETitleBarType.Rectangular)
                                {
                                    rcMin.X -= 3;
                                    rcMin.Y += 1;
                                }
                                pen.Width = 2;
                                g.DrawLine(pen, rcMin.Right, rcMin.Top, rcMin.Right, rcMin.Top + 4);
                                g.DrawLine(pen, rcMin.Right - 1, rcMin.Top + 4, rcMin.Right + 4, rcMin.Top + 4);
                                g.FillRectangle(pen.Brush, rcMin.Right + 2, rcMin.Top, 2, 2);
                                g.FillRectangle(pen.Brush, rcMin.Right + 3, rcMin.Top - 1, 2, 2);
                                break;
                        }
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        pen.Dispose();

                        #endregion
                    }
                    break;

                case EButtonStyle.MacStyle:
                    {
                        #region Macstyle

                        switch (m_eButtonType)
                        {
                            case ECtrlType.Close:
                                createMacStyleButton(Color.FromArgb(172, 61, 49), Color.FromArgb(255, 160, 110), Color.FromArgb(153, 93, 94), g);
                                break;

                            case ECtrlType.Maximize:
                                createMacStyleButton(Color.FromArgb(223, 129, 64), Color.FromArgb(255, 231, 117), Color.FromArgb(158, 109, 60), g);
                                break;

                            case ECtrlType.Minimize:
                                createMacStyleButton(Color.FromArgb(158, 214, 85), Color.FromArgb(200, 255, 130), Color.FromArgb(108, 140, 60), g);
                                break;
                        }

                        #endregion
                    }
                    break;
            }
        }

        /// <summary>
        /// Fills titlebar button.
        /// </summary>
        /// <param name="rcBtn">Button bounding rectangle.</param>
        /// <param name="g">Graphics object.</param>
        /// <param name="clrStart">Color used to fill the button.</param>
        /// <param name="clrEnd">Color used to fill the outer glow.</param>
        /// <param name="XBoxClip">Path to perform clipping tasks.</param>
        private void FillButton(Rectangle rcBtn, Graphics g, Color clrStart, Color clrEnd, GraphicsPath XBoxClip)
        {
            switch (m_eFillMode)
            {
                case EButtonFillMode.UpperGlow:
                    rcBtn.Height = 3;
                    using (LinearGradientBrush lgb = new LinearGradientBrush(rcBtn, clrStart, clrEnd, LinearGradientMode.Vertical))
                    {
                        g.FillRectangle(lgb, rcBtn);
                    }
                    break;

                case EButtonFillMode.FullFill:
                    // restrict drawing inside button box / rectangle:
                    g.SetClip(XBoxClip);
                    g.SetClip(rcBtn, CombineMode.Intersect);

                    #region Fill button

                    using (SolidBrush sb = new SolidBrush(clrStart))
                    {
                        g.FillRectangle(sb, rcBtn);
                    }

                    #endregion

                    using (EAntiAlias xaa = new EAntiAlias(g))
                    {
                        #region Fill shine

                        using (GraphicsPath XBtnGlow = new GraphicsPath())
                        {
                            XBtnGlow.AddEllipse(rcBtn.Left - 5, rcBtn.Bottom - rcBtn.Height / 2 + 3, rcBtn.Width + 11, rcBtn.Height + 11);
                            using (PathGradientBrush pgb = new PathGradientBrush(XBtnGlow))
                            {
                                pgb.CenterColor = Color.FromArgb(235, Color.White);
                                pgb.SurroundColors = new Color[] { Color.FromArgb(0, clrEnd) };
                                pgb.SetSigmaBellShape(0.8f);

                                g.FillPath(pgb, XBtnGlow);
                            }
                        }

                        #endregion

                        #region Fill upper glow

                        rcBtn.Height = rcBtn.Height / 2 - 2;
                        using (LinearGradientBrush lgb = new LinearGradientBrush(rcBtn, Color.FromArgb(80, Color.White), Color.FromArgb(140, Color.White), LinearGradientMode.Vertical))
                        {
                            using (GraphicsPath XGlowPath = EFormHelper.RoundRect((RectangleF)rcBtn, 0, 0, 4, 4))
                            {
                                lgb.WrapMode = WrapMode.TileFlipXY;
                                g.FillPath(lgb, XGlowPath);
                            }
                        }

                        #endregion
                    }
                    // reset clipping back:
                    g.ResetClip();
                    break;
            }
        }

        private void createMacStyleButton(Color clrStartUp, Color clrEndUp, Color clrBorder, Graphics g)
        {
            GraphicsPath btn = new GraphicsPath();

            //Rectangle rcBtn = new Rectangle(m_lLeft, m_lTop, m_lButtonWidth, m_lButtonHeight);

            int l = m_lLeft + m_lButtonWidth / 4;
            int t = m_lTop + m_lButtonHeight / 4;
            int w = m_lButtonWidth / 2;
            int h = m_lButtonHeight / 2;
            Rectangle rcBtn = new Rectangle(l, t, w, h);
            btn.AddEllipse(rcBtn);

            // fill button:
            using (LinearGradientBrush aquaUp = new LinearGradientBrush(rcBtn, clrStartUp, clrEndUp, LinearGradientMode.Vertical))
            {
                aquaUp.SetSigmaBellShape(1.0f);
                g.FillPath(aquaUp, btn);
                using (Pen border = new Pen(clrBorder))
                {
                    g.DrawPath(border, btn);
                }

                #region Outer border

                // This will give very cool shadow drop effect to the button:
                Rectangle rcOuterBorder = rcBtn;
                rcOuterBorder.Inflate(1, 1);
                using (GraphicsPath ob = new GraphicsPath())
                {
                    ob.AddEllipse(rcOuterBorder);
                    using (Pen po = new Pen(Color.FromArgb(205, 205, 205)))
                    {
                        g.DrawPath(po, ob);
                    }
                }

                #endregion
            }
            // draw upper glow:
            Rectangle rcGlow = new Rectangle(rcBtn.Left + rcBtn.Width / 2 - 1, rcBtn.Top + 1, 3, 2);
            using (SolidBrush sb = new SolidBrush(Color.FromArgb(200, Color.White)))
            {
                g.FillRectangle(sb, rcGlow);
            }
            // draw button symbol:
            if (m_bHovering)
            {
                switch (m_eButtonType)
                {
                    case ECtrlType.Close:
                        using (Pen pClose = new Pen(Color.FromArgb(146, 57, 46)))
                        {
                            pClose.Width = 1.55f;
                            g.DrawLine(pClose, rcBtn.Left + 4, rcBtn.Top + 4, rcBtn.Right - 4, rcBtn.Bottom - 4);
                            g.DrawLine(pClose, rcBtn.Right - 4, rcBtn.Top + 4, rcBtn.Left + 4, rcBtn.Bottom - 4);
                        }
                        break;

                    case ECtrlType.Maximize:
                        using (Pen pMaximize = new Pen(Color.FromArgb(162, 95, 59)))
                        {
                            pMaximize.Width = 1.55f;
                            Rectangle rcMax = rcBtn;
                            rcMax.Inflate(-4, -4);
                            g.DrawRectangle(pMaximize, rcMax);
                        }
                        break;

                    case ECtrlType.Minimize:
                        using (Pen pMinimize = new Pen(Color.FromArgb(81, 120, 47)))
                        {
                            pMinimize.Width = 1.55f;
                            g.DrawLine(pMinimize, rcBtn.Left + 4, rcBtn.Bottom - 4, rcBtn.Right - 4, rcBtn.Bottom - 4);
                        }
                        break;
                }
                // fill lower glow:
                using (GraphicsPath shn = new GraphicsPath())
                {
                    shn.AddEllipse(rcBtn.Left, rcBtn.Top + rcBtn.Height / 2, rcBtn.Width, rcBtn.Height);
                    using (PathGradientBrush pgb = new PathGradientBrush(shn))
                    {
                        g.SetClip(btn);
                        pgb.CenterColor = Color.FromArgb(210, Color.White);
                        pgb.SurroundColors = new Color[] { Color.FromArgb(0, clrEndUp) };
                        if (m_bHovering)
                        {
                            g.FillPath(pgb, shn);
                        }
                        g.ResetClip();
                    }
                }
            }
            btn.Dispose();
        }
    }
}