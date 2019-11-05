using System.Drawing;
using System.Drawing.Drawing2D;

namespace EgoDevil.Utilities.UI.EForm
{
    public class E3DBorderPrimitive
    {
        public enum EBorderStyle
        {
            /// <summary>
            /// Simple flat style border.
            /// </summary>
            Flat,

            /// <summary>
            /// Advanced 3D border rendering.
            /// </summary>
            E3D,
        }

        public enum EBorderType
        {
            /// <summary>
            /// Border is rendered as a rectangular area.
            /// </summary>
            Rectangular,

            /// <summary>
            /// Upper corners are rounded. Added: 04/03/09
            /// </summary>
            Rounded,

            /// <summary>
            /// Upper corners are inclinated. Added: 06/03/09
            /// </summary>
            Inclinated
        }

        private EBorderType m_eBorderType = EBorderType.Rectangular;
        private EBorderStyle m_eBorderStyle = EBorderStyle.E3D;

        private Color m_clrFlatBorder = Color.FromArgb(52, 52, 52);
        private int m_lTitleBarHeight = 30;
        private int m_lRadius = 6;
        private int m_lInclination = 6;
        private GraphicsPath m_BorderShape = new GraphicsPath();

        private Color[] m_clrOuterBorder = new Color[]
        {
            Color.FromArgb(59,90 ,130 ),
            Color.FromArgb(177,198,225)
        };

        private Color[] m_clrInnerBorder = new Color[]
        {
            Color.FromArgb(194,217,247),
            Color.FromArgb(181,207,241),
            Color.FromArgb(138,166,205),
            Color.FromArgb(253,246,253)
        };

        #region (* Properties *)

        public EBorderType BorderType
        {
            get
            {
                return this.m_eBorderType;
            }
            set
            {
                this.m_eBorderType = value;
            }
        }

        public EBorderStyle BorderStyle
        {
            get
            {
                return this.m_eBorderStyle;
            }
            set
            {
                this.m_eBorderStyle = value;
            }
        }

        public int Inclination
        {
            get
            {
                return m_lInclination;
            }
            set
            {
                if (m_lInclination > 6)
                    m_lInclination = 6;

                this.m_lInclination = value;
            }
        }

        public int Radius
        {
            get
            {
                return this.m_lRadius;
            }
            set
            {
                if (value > 9)
                    this.m_lRadius = 9;

                this.m_lRadius = value;
            }
        }

        public Color FlatBorder
        {
            get
            {
                return this.m_clrFlatBorder;
            }
            set
            {
                this.m_clrFlatBorder = value;
            }
        }

        public Color[] OuterBorderColors
        {
            get
            {
                return this.m_clrOuterBorder;
            }
            set
            {
                this.m_clrOuterBorder = value;
            }
        }

        public Color[] InnerBorderColors
        {
            get
            {
                return this.m_clrInnerBorder;
            }
            set
            {
                this.m_clrInnerBorder = value;
            }
        }

        public int TitleBarHeight
        {
            get
            {
                return this.m_lTitleBarHeight;
            }
            set
            {
                this.m_lTitleBarHeight = value;
            }
        }

        #endregion

        public GraphicsPath FindX3DBorderPrimitive(Rectangle rcBorder)
        {
            switch (m_eBorderType)
            {
                case EBorderType.Rounded:
                    m_BorderShape = EFormHelper.RoundRect((RectangleF)rcBorder, m_lRadius, m_lRadius, 0, 0);
                    break;

                case EBorderType.Inclinated:
                    m_BorderShape = CreateInclinatedBorderPath(rcBorder);
                    break;
            }
            return m_BorderShape;
        }

        /// <summary>
        /// Main rendering method.
        /// </summary>
        /// <param name="rcBorder">Border bounds</param>
        /// <param name="g">Graphics object</param>
        public void Render(Rectangle rcBorder, Graphics g)
        {
            GraphicsPath XBorderPath = new GraphicsPath();
            switch (m_eBorderStyle)
            {
                case EBorderStyle.E3D:
                    switch (m_eBorderType)
                    {
                        case EBorderType.Rectangular:
                            using (EAntiAlias xaa = new EAntiAlias(g))
                            {
                                DrawBorderLine(g, XBorderPath, rcBorder, 0, false);
                            }
                            break;

                        case EBorderType.Rounded:
                            DrawBorderLine(g, XBorderPath, rcBorder, m_lRadius, false);
                            break;

                        case EBorderType.Inclinated:
                            DrawBorderLine(g, XBorderPath, rcBorder, 0, false);
                            break;
                    }
                    break;

                case EBorderStyle.Flat:
                    switch (m_eBorderType)
                    {
                        case EBorderType.Rectangular:
                            using (EAntiAlias xaa = new EAntiAlias(g))
                            {
                                DrawBorderLine(g, XBorderPath, rcBorder, 0, true);
                            }
                            break;

                        case EBorderType.Rounded:
                            DrawBorderLine(g, XBorderPath, rcBorder, m_lRadius, true);
                            break;

                        case EBorderType.Inclinated:
                            DrawBorderLine(g, XBorderPath, rcBorder, 0, false);
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// Helper method for rectangle deflating.
        /// </summary>
        /// <param name="rcBorder">Rectangle to deflate</param>
        private void DeflateRect(ref Rectangle rcBorder)
        {
            rcBorder.X += 1; rcBorder.Y += 1;
            rcBorder.Width -= 2; rcBorder.Height -= 2;
        }

        /// <summary> Draws inner & outer 3D borders. </summary> <param name="g"> Graphics
        /// object</param> <param name="XBorderPath"> Border path</param> <param name="rcBorder">
        /// Border bounds</param> <param name="lCorner"> Radius of a rounded rectangle</param>
        /// <param name="bFlat"> Controls border type mode</param>
        private void DrawBorderLine(Graphics g, GraphicsPath XBorderPath, Rectangle rcBorder, int lCorner, bool bFlat)
        {
            int lC = lCorner;

            #region Draw outer border

            if (bFlat)
            {
                switch (m_eBorderType)
                {
                    case EBorderType.Rectangular:
                        XBorderPath = EFormHelper.RoundRect((RectangleF)rcBorder, lC, lC, lC, lC);
                        break;

                    case EBorderType.Rounded:
                        XBorderPath = EFormHelper.RoundRect((RectangleF)rcBorder, lC, lC, 0, 0);
                        break;

                    case EBorderType.Inclinated:
                        XBorderPath = CreateInclinatedBorderPath(rcBorder);
                        break;
                }
                using (Pen pFlat = new Pen(m_clrFlatBorder))
                {
                    g.DrawPath(pFlat, XBorderPath);
                }
            }
            else
            {
                for (int o = 0; o < m_clrOuterBorder.Length; o++)
                {
                    switch (m_eBorderType)
                    {
                        case EBorderType.Rectangular:
                            XBorderPath = EFormHelper.RoundRect((RectangleF)rcBorder, lC, lC, lC, lC);
                            break;

                        case EBorderType.Rounded:
                            XBorderPath = EFormHelper.RoundRect((RectangleF)rcBorder, lC, lC, 0, 0);
                            break;

                        case EBorderType.Inclinated:
                            XBorderPath = CreateInclinatedBorderPath(rcBorder);
                            break;
                    }
                    Pen pen = new Pen(m_clrOuterBorder[o]);
                    g.DrawPath(pen, XBorderPath);
                    DeflateRect(ref rcBorder);
                    if (m_eBorderType != EBorderType.Rectangular)
                        lC--;
                }

            #endregion

                #region Draw inner border

                rcBorder.Y += m_lTitleBarHeight;
                rcBorder.Height -= m_lTitleBarHeight;

                for (int i = 0; i < m_clrInnerBorder.Length; i++)
                {
                    Pen penInner = new Pen(m_clrInnerBorder[i]);
                    g.DrawRectangle(penInner, rcBorder);
                    DeflateRect(ref rcBorder);
                }
            }

                #endregion
        }

        private GraphicsPath CreateInclinatedBorderPath(Rectangle rcBorder)
        {
            GraphicsPath i = new GraphicsPath();
            i.AddLine(rcBorder.X, rcBorder.Y + m_lInclination, rcBorder.X, rcBorder.Bottom);
            i.AddLine(rcBorder.X, rcBorder.Bottom, rcBorder.Right, rcBorder.Bottom);
            i.AddLine(rcBorder.Right, rcBorder.Bottom, rcBorder.Right, rcBorder.Top + m_lInclination);
            i.AddLine(rcBorder.Right, rcBorder.Top + m_lInclination, rcBorder.Right - m_lInclination, rcBorder.Top);
            i.AddLine(rcBorder.Right - m_lInclination, rcBorder.Top, rcBorder.Left + m_lInclination, rcBorder.Top);
            i.AddLine(rcBorder.Left + m_lInclination, rcBorder.Top, rcBorder.Left, rcBorder.Top + m_lInclination);

            return i;
        }
    }
}