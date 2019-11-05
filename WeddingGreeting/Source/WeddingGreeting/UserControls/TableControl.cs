using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ComboBox;
using System.Drawing.Design;
using System.Collections.ObjectModel;

namespace WeddingGreeting.UserControls
{
    public partial class TableControl : UserControl
    {
        private string tableName;
        public string TableName
        {
            get => tableName;
            set
            {
                if (tableName != value)
                {
                    tableName = value;
                    this.Invalidate();
                }
            }
        }
        private string tableNo;
        public string TableNo
        {
            get => tableNo;
            set
            {
                if (tableNo != value)
                {
                    tableNo = value;
                    this.Invalidate();
                }
            }
        }


        private Color tableColor = Color.HotPink;
        public Color TableColor
        {
            get => tableColor;
            set
            {
                if (tableColor != value)
                {
                    tableColor = value;
                    this.Invalidate();
                }
            }
        }

        private Color tableNameColor = Color.Red;
        public Color TableNameColor
        {
            get => tableNameColor;
            set
            {
                if (tableNameColor != value)
                {
                    tableNameColor = value;
                    this.Invalidate();
                }
            }
        }

        private Color guestNameColor = Color.Black;
        public Color GuestNameColor
        {
            get => guestNameColor;
            set
            {
                if (guestNameColor != value)
                {
                    guestNameColor = value;
                    this.Invalidate();
                }
            }
        }




        private ObservableCollection<String> nameList = new ObservableCollection<String>();
        [Browsable(true)]
        [Description("名单")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<String> NameList
        {
            get => nameList;
            set
            {
                if (nameList != value)
                {
                    nameList = value;
                    this.Invalidate();
                }
            }
        }



        public TableControl()
        {
            InitializeComponent();
            //设置Style支持透明背景色
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
            DoubleBuffered = false;//双缓存处理
            nameList.CollectionChanged += NameList_CollectionChanged;
        }

        private void NameList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.Invalidate();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = 0x20;
                return cp;
            }
        }


        private void TableControl_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var center = new Point(Width / 2, Height / 2);
            int r = Width > Height ? Height / 2 : Width / 2;

            #region 画圆
            var ellipseRect = new Rectangle(center.X - r + 10, center.Y - r + 10, 2 * (r - 10), 2 * (r - 10));

            var tableBrush = new SolidBrush(tableColor);//填充的颜色
            g.FillEllipse(tableBrush, ellipseRect);
            tableBrush.Dispose();
            Pen pen = new Pen(Color.Pink, 5);//画笔颜色                                
            g.DrawEllipse(pen, ellipseRect);
            pen.Dispose();
            #endregion

            #region 画桌名与桌号
            StringFormat format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,  // 垂直居中
                Alignment = StringAlignment.Center      // 水平居中
            };
            Font myFont = new Font("Arial", 10, FontStyle.Bold);
            var tableNameBrush = new SolidBrush(tableNameColor);//填充的颜色
            g.DrawString(TableName + "\n" + TableNo, myFont, tableNameBrush, new Rectangle(0, 0, Width, Height), format);
            myFont.Dispose();
            tableBrush.Dispose();

            #endregion

            #region Draw names

            g.ResetTransform();
            g.TranslateTransform(center.X, center.Y);
            var guestNameBrush = new SolidBrush(guestNameColor);
            if (nameList != null && nameList.Count > 0)
            {
                var angle = 360 / nameList.Count;
                for (var i = 0; i < nameList.Count; i++)
                {
                    using (var p = new System.Drawing.Drawing2D.GraphicsPath())
                    {
                        var point = new Point(r - 60, 0);
                        p.AddString(nameList[i], new FontFamily("Arial"), (int)System.Drawing.FontStyle.Underline, 10, point, new StringFormat());

                        if (i * angle > 90 && i * angle < 270)
                        {
                            var offsetX = (float)r+30;
                            p.Transform(new System.Drawing.Drawing2D.Matrix(-1, 0, 0, -1, offsetX, 0));
                        }

                        g.FillPath(guestNameBrush, p);
                        g.RotateTransform(angle);

                    }
                }
            }
            guestNameBrush.Dispose();
            g.ResetTransform();

            #endregion


        }
    }
}
