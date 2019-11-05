using System.ComponentModel;
using System.Windows.Forms;

using EgoDevil.Utilities.UI.ActivityBar;

namespace EgoDevil.Utilities.BkWorker
{
    partial class BackgroundForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.activityBar1 = new EgoDevil.Utilities.UI.ActivityBar.ActivityBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Orange;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(82, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Waitting...";
            // 
            // activityBar1
            // 
            this.activityBar1.BackColor = System.Drawing.Color.Transparent;
            this.activityBar1.BarColor = System.Drawing.Color.Orange;
            this.activityBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.activityBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityBar1.HighlightColor = System.Drawing.Color.White;
            this.activityBar1.Location = new System.Drawing.Point(0, 0);
            this.activityBar1.Name = "activityBar1";
            this.activityBar1.Size = new System.Drawing.Size(285, 47);
            this.activityBar1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.activityBar1);
            this.panel1.Location = new System.Drawing.Point(46, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 47);
            this.panel1.TabIndex = 2;
            // 
            // BackgroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(377, 101);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BackgroundForm";
            this.Opacity = 0.5;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LoadingForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label label1;
        private ActivityBar activityBar1;
        private Panel panel1;
    }
}