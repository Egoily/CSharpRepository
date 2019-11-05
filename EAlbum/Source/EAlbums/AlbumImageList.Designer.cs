using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using EgoDevil.Utilities.UI.EPanels;

namespace EAlbums
{
    partial class AlbumImageList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlbumImageList));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ImageThumb = new System.Windows.Forms.DataGridViewImageColumn();
            this.ImageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbInsertImage = new System.Windows.Forms.ToolStripButton();
            this.ePanelAlbumImageList = new EgoDevil.Utilities.UI.EPanels.EPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.ePanelAlbumImageList.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ImageThumb,
            this.ImageName,
            this.Target});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.Location = new System.Drawing.Point(1, 19);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.RowHeadersWidth = 20;
            this.dataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(215, 286);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DataGridViewRowPostPaint);
            // 
            // ImageThumb
            // 
            this.ImageThumb.HeaderText = "缩略图";
            this.ImageThumb.Name = "ImageThumb";
            this.ImageThumb.Width = 47;
            // 
            // ImageName
            // 
            this.ImageName.HeaderText = "名称";
            this.ImageName.Name = "ImageName";
            this.ImageName.Width = 54;
            // 
            // Target
            // 
            this.Target.HeaderText = "目标";
            this.Target.Name = "Target";
            this.Target.Width = 54;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OpenFileDialogFileOk);
            // 
            // toolStrip
            // 
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbInsertImage});
            this.toolStrip.Location = new System.Drawing.Point(1, 280);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(215, 25);
            this.toolStrip.TabIndex = 4;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ToolStripItemClicked);
            // 
            // tsbInsertImage
            // 
            this.tsbInsertImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbInsertImage.Image = ((System.Drawing.Image)(resources.GetObject("tsbInsertImage.Image")));
            this.tsbInsertImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbInsertImage.Name = "tsbInsertImage";
            this.tsbInsertImage.Size = new System.Drawing.Size(23, 22);
            this.tsbInsertImage.Text = "toolStripButton1";
            // 
            // ePanelAlbumImageList
            // 
            this.ePanelAlbumImageList.AssociatedSplitter = null;
            this.ePanelAlbumImageList.AutoScroll = true;
            this.ePanelAlbumImageList.BackColor = System.Drawing.Color.Transparent;
            this.ePanelAlbumImageList.CaptionFont = new System.Drawing.Font("Segoe UI", 11.75F, System.Drawing.FontStyle.Bold);
            this.ePanelAlbumImageList.CaptionHeight = 18;
            this.ePanelAlbumImageList.Controls.Add(this.toolStrip);
            this.ePanelAlbumImageList.Controls.Add(this.dataGridView);
            this.ePanelAlbumImageList.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(157)))));
            this.ePanelAlbumImageList.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.ePanelAlbumImageList.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.ePanelAlbumImageList.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(235)))), ((int)(((byte)(216)))));
            this.ePanelAlbumImageList.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.ePanelAlbumImageList.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(226)))));
            this.ePanelAlbumImageList.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(162)))), ((int)(((byte)(221)))));
            this.ePanelAlbumImageList.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(162)))), ((int)(((byte)(221)))));
            this.ePanelAlbumImageList.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.ePanelAlbumImageList.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.ePanelAlbumImageList.CustomColors.ContentGradientBegin = System.Drawing.SystemColors.ButtonFace;
            this.ePanelAlbumImageList.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(234)))), ((int)(((byte)(215)))));
            this.ePanelAlbumImageList.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.ePanelAlbumImageList.Dock = System.Windows.Forms.DockStyle.Left;
            this.ePanelAlbumImageList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ePanelAlbumImageList.Image = null;
            this.ePanelAlbumImageList.Location = new System.Drawing.Point(0, 0);
            this.ePanelAlbumImageList.MinimumSize = new System.Drawing.Size(18, 18);
            this.ePanelAlbumImageList.Name = "ePanelAlbumImageList";
            this.ePanelAlbumImageList.PanelStyle = EgoDevil.Utilities.UI.EPanels.PanelStyle.Office2007;
            this.ePanelAlbumImageList.ShowCloseIcon = true;
            this.ePanelAlbumImageList.Size = new System.Drawing.Size(217, 306);
            this.ePanelAlbumImageList.TabIndex = 5;
            this.ePanelAlbumImageList.Text = "列表";
            this.ePanelAlbumImageList.ToolTipTextCloseIcon = null;
            this.ePanelAlbumImageList.ToolTipTextExpandIconPanelCollapsed = null;
            this.ePanelAlbumImageList.ToolTipTextExpandIconPanelExpanded = null;
            this.ePanelAlbumImageList.CloseClick += new System.EventHandler<System.EventArgs>(this.EPanelAlbumImageListCloseClick);
            // 
            // AlbumImageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ePanelAlbumImageList);
            this.DoubleBuffered = true;
            this.Name = "AlbumImageList";
            this.Size = new System.Drawing.Size(221, 306);
            this.Load += new System.EventHandler(this.AlbumImageListLoad);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ePanelAlbumImageList.ResumeLayout(false);
            this.ePanelAlbumImageList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView;
        private DataGridViewImageColumn ImageThumb;
        private DataGridViewTextBoxColumn ImageName;
        private DataGridViewTextBoxColumn Target;
        private OpenFileDialog openFileDialog;
        private ToolStrip toolStrip;
        private ToolStripButton tsbInsertImage;
        private EPanel ePanelAlbumImageList;
    }
}
