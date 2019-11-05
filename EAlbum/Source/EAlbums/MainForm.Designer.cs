using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using EgoDevil.Utilities.UI.EPanels;

namespace EAlbums
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ePanelTask = new EgoDevil.Utilities.UI.EPanels.EPanel();
            this.taskStrip = new EAlbums.TaskStrip();
            this.albumImageList = new EAlbums.AlbumImageList();
            this.albumView = new EAlbums.ImageViewer();
            this.ePanelTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // ePanelTask
            // 
            this.ePanelTask.AssociatedSplitter = null;
            this.ePanelTask.BackColor = System.Drawing.Color.Transparent;
            this.ePanelTask.CaptionFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ePanelTask.CaptionHeight = 18;
            this.ePanelTask.Controls.Add(this.taskStrip);
            this.ePanelTask.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(157)))));
            this.ePanelTask.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.ePanelTask.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.ePanelTask.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(235)))), ((int)(((byte)(216)))));
            this.ePanelTask.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.ePanelTask.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(238)))), ((int)(((byte)(226)))));
            this.ePanelTask.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(162)))), ((int)(((byte)(221)))));
            this.ePanelTask.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(143)))), ((int)(((byte)(162)))), ((int)(((byte)(221)))));
            this.ePanelTask.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.ePanelTask.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.ePanelTask.CustomColors.ContentGradientBegin = System.Drawing.SystemColors.ButtonFace;
            this.ePanelTask.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(234)))), ((int)(((byte)(215)))));
            this.ePanelTask.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.ePanelTask.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ePanelTask.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ePanelTask.Image = null;
            this.ePanelTask.Location = new System.Drawing.Point(0, 626);
            this.ePanelTask.MinimumSize = new System.Drawing.Size(18, 18);
            this.ePanelTask.Name = "ePanelTask";
            this.ePanelTask.PanelStyle = EgoDevil.Utilities.UI.EPanels.PanelStyle.Office2007;
            this.ePanelTask.ShowExpandIcon = true;
            this.ePanelTask.ShowTransparentBackground = false;
            this.ePanelTask.ShowXPanderPanelProfessionalStyle = true;
            this.ePanelTask.Size = new System.Drawing.Size(904, 85);
            this.ePanelTask.TabIndex = 0;
            this.ePanelTask.Text = "ePanelView";
            this.ePanelTask.ToolTipTextCloseIcon = null;
            this.ePanelTask.ToolTipTextExpandIconPanelCollapsed = null;
            this.ePanelTask.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // taskStrip
            // 
            this.taskStrip.AssociatedAlbumImageList = this.albumImageList;
            this.taskStrip.AssociatedAlbumView = this.albumView;
            this.taskStrip.Location = new System.Drawing.Point(12, 22);
            this.taskStrip.Name = "taskStrip";
            this.taskStrip.Size = new System.Drawing.Size(457, 59);
            this.taskStrip.TabIndex = 0;
            // 
            // albumImageList
            // 
            this.albumImageList.AutoScroll = true;
            this.albumImageList.Dock = System.Windows.Forms.DockStyle.Right;
            this.albumImageList.Location = new System.Drawing.Point(683, 0);
            this.albumImageList.Name = "albumImageList";
            this.albumImageList.Size = new System.Drawing.Size(221, 626);
            this.albumImageList.TabIndex = 2;
            this.albumImageList.Visible = false;
            this.albumImageList.ImagesLoaded += new EAlbums.AlbumImageList.ImagesLoadedEventHandler(this.AlbumImageListImagesLoaded);
            // 
            // albumView
            // 
            this.albumView.AllowDrop = true;
            this.albumView.Alpha = 0F;
            this.albumView.AutoScroll = true;
            this.albumView.AutoSize = true;
            this.albumView.BackColor = System.Drawing.Color.Black;
            this.albumView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.albumView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.albumView.ImagePreviewMode = EAlbums.PreviewMode.None;
            this.albumView.Location = new System.Drawing.Point(0, 0);
            this.albumView.Name = "albumView";
            this.albumView.Pattern = EAlbums.ViewPatterns.Ready;
            this.albumView.Size = new System.Drawing.Size(904, 626);
            this.albumView.TabIndex = 1;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 711);
            this.Controls.Add(this.albumImageList);
            this.Controls.Add(this.albumView);
            this.Controls.Add(this.ePanelTask);
            this.Name = "MainForm";
            this.Text = "E Albums";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.ePanelTask.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private EPanel ePanelTask;
        private ImageViewer albumView;
        private TaskStrip taskStrip;
        private AlbumImageList albumImageList;
  
    
     
       
    }
}

