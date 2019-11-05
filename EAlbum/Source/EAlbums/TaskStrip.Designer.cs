using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using EgoDevil.Utilities.UI.EPanels;
using EgoDevil.Utilities.UI.IndustrialCtrls.Buttons;

using ExtendedPictureBoxLib;

namespace EAlbums
{
    partial class TaskStrip
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(TaskStrip));
            this.listViewAlbums = new ListView();
            this.imageList = new ImageList(this.components);
            this.ePanelOption = new EPanel();
            this.btnLookup = new Button();
            this.btnDeleteAlbum = new Button();
            this.btnInsertAlbum = new Button();
            this.apbtnDefaultAlbum = new AnimatedPictureButton();
            this.lbButtonStart = new LBButton();
            this.ePanelOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewAlbums
            // 
            this.listViewAlbums.Alignment = ListViewAlignment.SnapToGrid;
            this.listViewAlbums.BorderStyle = BorderStyle.None;
            this.listViewAlbums.Cursor = Cursors.Hand;
            this.listViewAlbums.HeaderStyle = ColumnHeaderStyle.None;
            this.listViewAlbums.LabelEdit = true;
            this.listViewAlbums.LabelWrap = false;
            this.listViewAlbums.LargeImageList = this.imageList;
            this.listViewAlbums.Location = new Point(106, 5);
            this.listViewAlbums.MultiSelect = false;
            this.listViewAlbums.Name = "listViewAlbums";
            this.listViewAlbums.ShowItemToolTips = true;
            this.listViewAlbums.Size = new Size(334, 52);
            this.listViewAlbums.StateImageList = this.imageList;
            this.listViewAlbums.TabIndex = 2;
            this.listViewAlbums.UseCompatibleStateImageBehavior = false;
            this.listViewAlbums.SelectedIndexChanged += new EventHandler(this.ListViewAlbumsSelectedIndexChanged);
            this.listViewAlbums.MouseDown += new MouseEventHandler(this.ListViewAlbumsMouseDown);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = Color.Transparent;
            this.imageList.Images.SetKeyName(0, "Dance.gif");
            this.imageList.Images.SetKeyName(1, "AlbumBK.jpg");
            // 
            // ePanelOption
            // 
            this.ePanelOption.AssociatedSplitter = null;
            this.ePanelOption.BackColor = Color.Transparent;
            this.ePanelOption.CaptionFont = new Font("Segoe UI", 11.75F, FontStyle.Bold);
            this.ePanelOption.CaptionHeight = 27;
            this.ePanelOption.Controls.Add(this.btnLookup);
            this.ePanelOption.Controls.Add(this.btnDeleteAlbum);
            this.ePanelOption.Controls.Add(this.btnInsertAlbum);
            this.ePanelOption.CustomColors.BorderColor = Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(157)))));
            this.ePanelOption.CustomColors.CaptionCloseIcon = SystemColors.ControlText;
            this.ePanelOption.CustomColors.CaptionExpandIcon = SystemColors.ControlText;
            this.ePanelOption.CustomColors.CaptionGradientBegin = Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(235)))), ((int)(((byte)(216)))));
            this.ePanelOption.CustomColors.CaptionGradientEnd = SystemColors.ButtonFace;
            this.ePanelOption.CustomColors.CaptionGradientMiddle = Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(226)))));
            this.ePanelOption.CustomColors.CaptionSelectedGradientBegin = Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(162)))), ((int)(((byte)(221)))));
            this.ePanelOption.CustomColors.CaptionSelectedGradientEnd = Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(162)))), ((int)(((byte)(221)))));
            this.ePanelOption.CustomColors.CaptionText = SystemColors.ControlText;
            this.ePanelOption.CustomColors.CollapsedCaptionText = SystemColors.ControlText;
            this.ePanelOption.CustomColors.ContentGradientBegin = SystemColors.ButtonFace;
            this.ePanelOption.CustomColors.ContentGradientEnd = Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(234)))), ((int)(((byte)(215)))));
            this.ePanelOption.CustomColors.InnerBorderColor = SystemColors.Window;
            this.ePanelOption.ForeColor = SystemColors.ControlText;
            this.ePanelOption.Image = null;
            this.ePanelOption.LinearGradientMode = LinearGradientMode.Vertical;
            this.ePanelOption.Location = new Point(106, 63);
            this.ePanelOption.MinimumSize = new Size(27, 27);
            this.ePanelOption.Name = "ePanelOption";
            this.ePanelOption.PanelStyle = PanelStyle.Office2007;
            this.ePanelOption.ShowCaptionbar = false;
            this.ePanelOption.ShowTransparentBackground = false;
            this.ePanelOption.Size = new Size(250, 27);
            this.ePanelOption.TabIndex = 4;
            this.ePanelOption.Text = "Option";
            this.ePanelOption.ToolTipTextCloseIcon = null;
            this.ePanelOption.ToolTipTextExpandIconPanelCollapsed = null;
            this.ePanelOption.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // btnLookup
            // 
            this.btnLookup.Location = new Point(165, 1);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new Size(75, 23);
            this.btnLookup.TabIndex = 3;
            this.btnLookup.Text = "查看";
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new EventHandler(this.BtnLookupClick);
            // 
            // btnDeleteAlbum
            // 
            this.btnDeleteAlbum.Location = new Point(84, 0);
            this.btnDeleteAlbum.Name = "btnDeleteAlbum";
            this.btnDeleteAlbum.Size = new Size(75, 23);
            this.btnDeleteAlbum.TabIndex = 3;
            this.btnDeleteAlbum.Text = "删除相册";
            this.btnDeleteAlbum.UseVisualStyleBackColor = true;
            this.btnDeleteAlbum.Click += new EventHandler(this.BtnDeleteAlbumClick);
            // 
            // btnInsertAlbum
            // 
            this.btnInsertAlbum.Location = new Point(3, 0);
            this.btnInsertAlbum.Name = "btnInsertAlbum";
            this.btnInsertAlbum.Size = new Size(75, 23);
            this.btnInsertAlbum.TabIndex = 3;
            this.btnInsertAlbum.Text = "添加相册";
            this.btnInsertAlbum.UseVisualStyleBackColor = true;
            this.btnInsertAlbum.Click += new EventHandler(this.BtnInsertAlbumClick);
            // 
            // apbtnDefaultAlbum
            // 
            this.apbtnDefaultAlbum.ForeColor = Color.Black;
            this.apbtnDefaultAlbum.Location = new Point(54, 5);
            this.apbtnDefaultAlbum.Name = "apbtnDefaultAlbum";
            this.apbtnDefaultAlbum.PushedProperties = ((PictureBoxStateProperties)((((((PictureBoxStateProperties.Alpha | PictureBoxStateProperties.RotationAngle)
                        | PictureBoxStateProperties.Zoom)
                        | PictureBoxStateProperties.ExtraImageRotationAngle)
                        | PictureBoxStateProperties.ShadowOffset)
                        | PictureBoxStateProperties.ImageOffset)));
            this.apbtnDefaultAlbum.Size = new Size(46, 31);
            this.apbtnDefaultAlbum.TabIndex = 1;
            this.apbtnDefaultAlbum.Text = "默认相册";
            this.apbtnDefaultAlbum.Click += new EventHandler(this.ApbtnDefaultAlbumClick);
            // 
            // lbButtonStart
            // 
            this.lbButtonStart.AutoRebound = true;
            this.lbButtonStart.BackColor = Color.Transparent;
            this.lbButtonStart.ButtonColor = Color.Red;
            this.lbButtonStart.Label = "";
            this.lbButtonStart.Location = new Point(3, 0);
            this.lbButtonStart.Name = "lbButtonStart";
            this.lbButtonStart.Renderer = null;
            this.lbButtonStart.RepeatInterval = 100;
            this.lbButtonStart.RepeatState = false;
            this.lbButtonStart.Size = new Size(45, 43);
            this.lbButtonStart.StartRepeatInterval = 500;
            this.lbButtonStart.State = LBButton.ButtonState.Normal;
            this.lbButtonStart.Style = LBButton.ButtonStyle.Circular;
            this.lbButtonStart.TabIndex = 0;
            // 
            // TaskStrip
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.ePanelOption);
            this.Controls.Add(this.listViewAlbums);
            this.Controls.Add(this.apbtnDefaultAlbum);
            this.Controls.Add(this.lbButtonStart);
            this.Name = "TaskStrip";
            this.Size = new Size(582, 200);
            this.Load += new EventHandler(this.TaskStripLoad);
            this.ePanelOption.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LBButton lbButtonStart;
        private AnimatedPictureButton apbtnDefaultAlbum;
        private ListView listViewAlbums;
        private ImageList imageList;
        private Button btnInsertAlbum;
        private Button btnDeleteAlbum;
        private EPanel ePanelOption;
        private Button btnLookup;


    }
}
