using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace EgoDevil.Utilities.UI.EForm
{
    public class ETitleBarIconHolder
    {
        private EForm m_owner;
        private List<EHolderButton> m_ehButtons = new List<EHolderButton>();

        public List<EHolderButton> HolderButtons
        {
            get
            {
                return this.m_ehButtons;
            }
        }

        public ETitleBarIconHolder(EForm eform)
        {
            m_owner = eform;
        }

        public ETitleBarIconHolder()
        {
        }

        public void RenderHolderButtons(int x, int y, Graphics g)
        {
            int lX = x;
            Rectangle rcIcon = new Rectangle();
            RectangleF rcImage = new RectangleF();
            RectangleF rcFrame = new RectangleF();

            foreach (EHolderButton xbtn in m_ehButtons)
            {
                if (xbtn.ButtonImage != null)
                {
                    xbtn.Left = lX;
                    xbtn.Top = y + 1;

                    rcIcon = new Rectangle(lX, y + 1, xbtn.ButtonImage.Size.Width, xbtn.ButtonImage.Size.Height);

                    if (xbtn.Hot)
                    {
                        using (EAntiAlias xaa = new EAntiAlias(g))
                        {
                            using (GraphicsPath XHolderBtnPath = BuildHolderButtonFrame(rcIcon, 100, 40))
                            {
                                using (LinearGradientBrush lgb = new LinearGradientBrush(XHolderBtnPath.GetBounds(),
                                                                                         Color.FromArgb(xbtn.FrameAlpha, xbtn.FrameStartColor),
                                                                                         Color.FromArgb(xbtn.FrameAlpha, xbtn.FrameEndColor),
                                                                                         LinearGradientMode.Vertical
                                                                                        )
                                      )
                                {
                                    g.FillPath(lgb, XHolderBtnPath);
                                }

                                rcFrame = XHolderBtnPath.GetBounds();
                            }
                            int lFrameImageWidth = 0;
                            if (xbtn.FrameBackImage != null)
                            {
                                // draw frame image:
                                rcImage = new RectangleF(rcFrame.Right - xbtn.FrameBackImage.Width,
                                                          rcFrame.Bottom - xbtn.FrameBackImage.Height,
                                                          xbtn.FrameBackImage.Width,
                                                          xbtn.FrameBackImage.Height
                                                        );
                                g.DrawImage(xbtn.FrameBackImage, rcImage);
                                lFrameImageWidth = xbtn.FrameBackImage.Height;
                            }
                            // draw caption / description:
                            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                            g.DrawString(xbtn.HolderButtonCaption,
                                         xbtn.HolderButtonCaptionFont,
                                         new SolidBrush(xbtn.HolderButtonCaptionColor),
                                         rcFrame.Left + 2,
                                         rcIcon.Bottom + 4
                                        );

                            StringFormat sf = new StringFormat();
                            sf.Alignment = StringAlignment.Near;
                            sf.LineAlignment = StringAlignment.Near;
                            sf.Trimming = StringTrimming.EllipsisCharacter;
                            sf.FormatFlags = StringFormatFlags.LineLimit;

                            float fCaptionWidth = g.MeasureString(xbtn.HolderButtonCaption, xbtn.HolderButtonCaptionFont).Height;

                            RectangleF rcDescription = new RectangleF(rcFrame.Left + 2,
                                                                      rcIcon.Bottom + fCaptionWidth,
                                                                      rcFrame.Width,
                                                                      rcFrame.Height - lFrameImageWidth
                                                                     );
                            g.DrawString(xbtn.HolderButtonDescription,
                                         xbtn.HolderButtonDescriptionFont,
                                         new SolidBrush(xbtn.HolderButtonDescriptionColor),
                                         rcDescription,
                                         sf
                                        );
                        }
                    }

                    // draw button:
                    g.DrawImage(xbtn.ButtonImage, rcIcon);

                    xbtn.ButtonRectangle = rcIcon;

                    // update position:
                    lX += rcIcon.Width + 2;
                }
            }
        }

        private GraphicsPath BuildHolderButtonFrame(Rectangle rcBtn, int lFrameWidth, int lFrameHeight)
        {
            GraphicsPath XHolderBtnPath = new GraphicsPath();

            XHolderBtnPath.AddArc(
            rcBtn.Left - 2,
            rcBtn.Top,
            rcBtn.Height / 2,
            rcBtn.Height / 2,
            180,
            90);
            XHolderBtnPath.AddLine(
            rcBtn.Left + rcBtn.Height / 2,
            rcBtn.Top,
            rcBtn.Right - 2,
            rcBtn.Top);
            XHolderBtnPath.AddArc(
            rcBtn.Right - rcBtn.Height / 2 + 2,
            rcBtn.Top,
            rcBtn.Height / 2,
            rcBtn.Height / 2,
            -90,
            90);
            XHolderBtnPath.AddLine(
            rcBtn.Right + 2,
            rcBtn.Top + rcBtn.Height / 2,
            rcBtn.Right + 2,
            rcBtn.Top + rcBtn.Height + 2 + rcBtn.Height / 2 - 8);

            XHolderBtnPath.AddLine(
            rcBtn.Right + 2,
            rcBtn.Top + rcBtn.Height + 2 + rcBtn.Height / 2 - 8,
            rcBtn.Right + lFrameWidth,
            rcBtn.Top + rcBtn.Height + 2 + rcBtn.Height / 2 - 8);

            XHolderBtnPath.AddArc(
            rcBtn.Right + lFrameWidth,
            rcBtn.Top + rcBtn.Height + 2 + rcBtn.Height / 2 - 8,
            rcBtn.Height / 2,
            rcBtn.Height / 2,
            -90,
            90);

            XHolderBtnPath.AddLine(
            rcBtn.Right + lFrameWidth + rcBtn.Height / 2,
            rcBtn.Top + rcBtn.Height + 2 - 8 + rcBtn.Height / 2,
            rcBtn.Right + lFrameWidth + rcBtn.Height / 2,
            rcBtn.Top + rcBtn.Height + 2 - 8 + rcBtn.Height / 2 + lFrameHeight);

            XHolderBtnPath.AddLine(
            rcBtn.Right + lFrameWidth + rcBtn.Height / 2,
            rcBtn.Top + rcBtn.Height + 2 - 8 + rcBtn.Height / 2 + lFrameHeight,
            rcBtn.Left,
            rcBtn.Top + rcBtn.Height + 2 - 8 + rcBtn.Height / 2 + lFrameHeight
            );

            XHolderBtnPath.AddLine(
            rcBtn.Left - 2,
            rcBtn.Top + rcBtn.Height + 2 - 8 + rcBtn.Height / 2 + lFrameHeight,
            rcBtn.Left - 2,
            rcBtn.Top + rcBtn.Height / 2 - 4
            );

            return XHolderBtnPath;
        }

        public int HitTestHolderButton(int x, int y, Rectangle rcHolder)
        {
            int lBtnIndex = -1;
            if (x >= rcHolder.Left && x <= rcHolder.Right)
            {
                EHolderButton btn = null;
                for (int i = 0; i < m_ehButtons.Count; i++)
                {
                    btn = m_ehButtons[i];
                    if (y >= 4 && y <= btn.ButtonRectangle.Bottom)
                    {
                        if (x >= btn.Left)
                        {
                            if (x < btn.Left + btn.ButtonRectangle.Width)
                            {
                                lBtnIndex = i;
                                break;
                            }
                        }
                    }
                }
            }
            return lBtnIndex;
        }

        public class EHolderButton
        {
            private Image m_btnIcon;
            private Rectangle m_rcBtn = new Rectangle();
            private bool m_bHot = false;
            private int m_lTop = 0;
            private int m_lLeft = 0;
            private string m_sCaption = "EHolderButton";
            private Font m_fntBtnFnt = new Font("Visitor TT2 BRK", 14);
            private Color m_clrCaptionColor = Color.YellowGreen;
            private string m_sDescription = "Description";
            private Font m_fntDescription = new Font("Visitor TT2 BRK", 9);
            private Color m_clrDescriptionColor = Color.White;
            private Image m_FrameBackImage;

            private Color m_clrFrameStartColor = Color.FromArgb(118, 151, 202);
            private Color m_clrFrameEndColor = Color.FromArgb(169, 203, 248);
            private int m_lFrameAlpha = 255;

            #region (* Properties *)

            public Rectangle ButtonRectangle
            {
                get
                {
                    return this.m_rcBtn;
                }
                set
                {
                    this.m_rcBtn = value;
                }
            }

            public Image ButtonImage
            {
                get
                {
                    return this.m_btnIcon;
                }
                set
                {
                    this.m_btnIcon = value;
                }
            }

            public bool Hot
            {
                get
                {
                    return this.m_bHot;
                }
                set
                {
                    this.m_bHot = value;
                }
            }

            public int Top
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

            public int Left
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

            public string HolderButtonCaption
            {
                get
                {
                    return this.m_sCaption;
                }
                set
                {
                    this.m_sCaption = value;
                }
            }

            public Color HolderButtonCaptionColor
            {
                get
                {
                    return this.m_clrCaptionColor;
                }
                set
                {
                    this.m_clrCaptionColor = value;
                }
            }

            public Font HolderButtonCaptionFont
            {
                get
                {
                    return this.m_fntBtnFnt;
                }
                set
                {
                    this.m_fntBtnFnt = value;
                }
            }

            public string HolderButtonDescription
            {
                get
                {
                    return this.m_sDescription;
                }
                set
                {
                    this.m_sDescription = value;
                }
            }

            public Color HolderButtonDescriptionColor
            {
                get
                {
                    return this.m_clrDescriptionColor;
                }
                set
                {
                    this.m_clrDescriptionColor = value;
                }
            }

            public Font HolderButtonDescriptionFont
            {
                get
                {
                    return this.m_fntDescription;
                }
                set
                {
                    this.m_fntDescription = value;
                }
            }

            public Image FrameBackImage
            {
                get
                {
                    return m_FrameBackImage;
                }
                set
                {
                    m_FrameBackImage = value;
                }
            }

            public Color FrameStartColor
            {
                get
                {
                    return this.m_clrFrameStartColor;
                }
                set
                {
                    this.m_clrFrameStartColor = value;
                }
            }

            public Color FrameEndColor
            {
                get
                {
                    return this.m_clrFrameEndColor;
                }
                set
                {
                    this.m_clrFrameEndColor = value;
                }
            }

            public int FrameAlpha
            {
                get
                {
                    return this.m_lFrameAlpha;
                }
                set
                {
                    if (m_lFrameAlpha < 0) m_lFrameAlpha = 0;
                    if (m_lFrameAlpha > 255) m_lFrameAlpha = 255;
                    this.m_lFrameAlpha = value;
                }
            }

            #endregion

            public EHolderButton(Image btn)
            {
                this.m_btnIcon = btn;
            }

            public EHolderButton(Image btn, string sCaption)
            {
                this.m_btnIcon = btn;
                this.m_sCaption = sCaption;
            }
        }
    }
}