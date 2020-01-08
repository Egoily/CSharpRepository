namespace AutoClicker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.GbClickMode = new System.Windows.Forms.GroupBox();
            this.RbFollowCursor = new System.Windows.Forms.RadioButton();
            this.RbFixedCursor = new System.Windows.Forms.RadioButton();
            this.SstStatusStrip = new System.Windows.Forms.StatusStrip();
            this.TsslCursorPosLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslCursorPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.BtnStart = new System.Windows.Forms.Button();
            this.BtnStop = new System.Windows.Forms.Button();
            this.TsslStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblTimeInterval = new System.Windows.Forms.Label();
            this.NudTimeInterval = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.GbClickMode.SuspendLayout();
            this.SstStatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudTimeInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // GbClickMode
            // 
            this.GbClickMode.Controls.Add(this.NudTimeInterval);
            this.GbClickMode.Controls.Add(this.LblTimeInterval);
            this.GbClickMode.Controls.Add(this.RbFixedCursor);
            this.GbClickMode.Controls.Add(this.RbFollowCursor);
            this.GbClickMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.GbClickMode.Location = new System.Drawing.Point(0, 0);
            this.GbClickMode.Name = "GbClickMode";
            this.GbClickMode.Size = new System.Drawing.Size(270, 61);
            this.GbClickMode.TabIndex = 0;
            this.GbClickMode.TabStop = false;
            // 
            // RbFollowCursor
            // 
            this.RbFollowCursor.AutoSize = true;
            this.RbFollowCursor.Checked = true;
            this.RbFollowCursor.Location = new System.Drawing.Point(25, 12);
            this.RbFollowCursor.Name = "RbFollowCursor";
            this.RbFollowCursor.Size = new System.Drawing.Size(71, 16);
            this.RbFollowCursor.TabIndex = 0;
            this.RbFollowCursor.Text = "光标跟随";
            this.RbFollowCursor.UseVisualStyleBackColor = true;
            // 
            // RbFixedCursor
            // 
            this.RbFixedCursor.AutoSize = true;
            this.RbFixedCursor.Location = new System.Drawing.Point(112, 12);
            this.RbFixedCursor.Name = "RbFixedCursor";
            this.RbFixedCursor.Size = new System.Drawing.Size(119, 16);
            this.RbFixedCursor.TabIndex = 0;
            this.RbFixedCursor.Text = "固定光标(F8设定)";
            this.RbFixedCursor.UseVisualStyleBackColor = true;
            // 
            // SstStatusStrip
            // 
            this.SstStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsslStatusLabel,
            this.TsslStatus,
            this.TsslCursorPosLabel,
            this.TsslCursorPos});
            this.SstStatusStrip.Location = new System.Drawing.Point(0, 102);
            this.SstStatusStrip.Name = "SstStatusStrip";
            this.SstStatusStrip.Size = new System.Drawing.Size(270, 22);
            this.SstStatusStrip.TabIndex = 1;
            // 
            // TsslCursorPosLabel
            // 
            this.TsslCursorPosLabel.Name = "TsslCursorPosLabel";
            this.TsslCursorPosLabel.Size = new System.Drawing.Size(59, 17);
            this.TsslCursorPosLabel.Text = "光标位置:";
            // 
            // TsslCursorPos
            // 
            this.TsslCursorPos.Name = "TsslCursorPos";
            this.TsslCursorPos.Size = new System.Drawing.Size(25, 17);
            this.TsslCursorPos.Text = "0,0";
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(44, 67);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(75, 23);
            this.BtnStart.TabIndex = 2;
            this.BtnStart.Text = "启动(F5)";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // BtnStop
            // 
            this.BtnStop.Enabled = false;
            this.BtnStop.Location = new System.Drawing.Point(125, 67);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(75, 23);
            this.BtnStop.TabIndex = 2;
            this.BtnStop.Text = "停止(F9)";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // TsslStatusLabel
            // 
            this.TsslStatusLabel.Name = "TsslStatusLabel";
            this.TsslStatusLabel.Size = new System.Drawing.Size(35, 17);
            this.TsslStatusLabel.Text = "状态:";
            // 
            // TsslStatus
            // 
            this.TsslStatus.Name = "TsslStatus";
            this.TsslStatus.Size = new System.Drawing.Size(32, 17);
            this.TsslStatus.Text = "停止";
            // 
            // LblTimeInterval
            // 
            this.LblTimeInterval.AutoSize = true;
            this.LblTimeInterval.Location = new System.Drawing.Point(23, 39);
            this.LblTimeInterval.Name = "LblTimeInterval";
            this.LblTimeInterval.Size = new System.Drawing.Size(83, 12);
            this.LblTimeInterval.TabIndex = 3;
            this.LblTimeInterval.Text = "时间间隔(ms):";
            // 
            // NudTimeInterval
            // 
            this.NudTimeInterval.Location = new System.Drawing.Point(112, 37);
            this.NudTimeInterval.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.NudTimeInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudTimeInterval.Name = "NudTimeInterval";
            this.NudTimeInterval.Size = new System.Drawing.Size(88, 21);
            this.NudTimeInterval.TabIndex = 4;
            this.NudTimeInterval.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "正在后台运行";
            this.notifyIcon.BalloonTipTitle = "5000";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "鼠标自动点击器";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 124);
            this.Controls.Add(this.GbClickMode);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnStart);
            this.Controls.Add(this.SstStatusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "自动点击器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.GbClickMode.ResumeLayout(false);
            this.GbClickMode.PerformLayout();
            this.SstStatusStrip.ResumeLayout(false);
            this.SstStatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NudTimeInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox GbClickMode;
        private System.Windows.Forms.RadioButton RbFixedCursor;
        private System.Windows.Forms.RadioButton RbFollowCursor;
        private System.Windows.Forms.StatusStrip SstStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel TsslStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel TsslStatus;
        private System.Windows.Forms.ToolStripStatusLabel TsslCursorPosLabel;
        private System.Windows.Forms.ToolStripStatusLabel TsslCursorPos;
        private System.Windows.Forms.Button BtnStart;
        private System.Windows.Forms.Button BtnStop;
        private System.Windows.Forms.Label LblTimeInterval;
        private System.Windows.Forms.NumericUpDown NudTimeInterval;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

