using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EAlbums
{
    partial class ImageViewer
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
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 30;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundWorker_RunWorkerCompleted);
            // 
            // ImageViewer
            // 
            this.AllowDrop = true;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.DoubleBuffered = true;
            this.Name = "ImageViewer";
            this.Size = new System.Drawing.Size(467, 381);
            this.Load += new System.EventHandler(this.ImageViewer_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ImageViewer_Paint);
            this.DoubleClick += new System.EventHandler(this.ImageViewer_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageViewer_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageViewer_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageViewer_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Timer timer;
        private BackgroundWorker backgroundWorker;
    }
}
