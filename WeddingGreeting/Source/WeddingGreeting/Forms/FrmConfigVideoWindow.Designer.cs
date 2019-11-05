namespace WeddingGreeting.Forms
{
    partial class FrmConfigVideoWindow
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
            this.lbWinWidth = new System.Windows.Forms.Label();
            this.nudWinWidth = new System.Windows.Forms.NumericUpDown();
            this.lbHeight = new System.Windows.Forms.Label();
            this.nudWinHeight = new System.Windows.Forms.NumericUpDown();
            this.lbLocation = new System.Windows.Forms.Label();
            this.nudLocationX = new System.Windows.Forms.NumericUpDown();
            this.nudLocationY = new System.Windows.Forms.NumericUpDown();
            this.btnSetVisiable = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudWinWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWinHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocationX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocationY)).BeginInit();
            this.SuspendLayout();
            // 
            // lbWinWidth
            // 
            this.lbWinWidth.AutoSize = true;
            this.lbWinWidth.Location = new System.Drawing.Point(12, 27);
            this.lbWinWidth.Name = "lbWinWidth";
            this.lbWinWidth.Size = new System.Drawing.Size(23, 12);
            this.lbWinWidth.TabIndex = 0;
            this.lbWinWidth.Text = "宽:";
            // 
            // nudWinWidth
            // 
            this.nudWinWidth.Location = new System.Drawing.Point(41, 25);
            this.nudWinWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWinWidth.Name = "nudWinWidth";
            this.nudWinWidth.Size = new System.Drawing.Size(73, 21);
            this.nudWinWidth.TabIndex = 1;
            this.nudWinWidth.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.nudWinWidth.ValueChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // lbHeight
            // 
            this.lbHeight.AutoSize = true;
            this.lbHeight.Location = new System.Drawing.Point(130, 29);
            this.lbHeight.Name = "lbHeight";
            this.lbHeight.Size = new System.Drawing.Size(23, 12);
            this.lbHeight.TabIndex = 0;
            this.lbHeight.Text = "高:";
            // 
            // nudWinHeight
            // 
            this.nudWinHeight.Location = new System.Drawing.Point(159, 25);
            this.nudWinHeight.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudWinHeight.Name = "nudWinHeight";
            this.nudWinHeight.Size = new System.Drawing.Size(73, 21);
            this.nudWinHeight.TabIndex = 1;
            this.nudWinHeight.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            this.nudWinHeight.ValueChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // lbLocation
            // 
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(12, 70);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(35, 12);
            this.lbLocation.TabIndex = 0;
            this.lbLocation.Text = "位置:";
            // 
            // nudLocationX
            // 
            this.nudLocationX.Location = new System.Drawing.Point(50, 68);
            this.nudLocationX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLocationX.Name = "nudLocationX";
            this.nudLocationX.Size = new System.Drawing.Size(73, 21);
            this.nudLocationX.TabIndex = 1;
            this.nudLocationX.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLocationX.ValueChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // nudLocationY
            // 
            this.nudLocationY.Location = new System.Drawing.Point(129, 68);
            this.nudLocationY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLocationY.Name = "nudLocationY";
            this.nudLocationY.Size = new System.Drawing.Size(73, 21);
            this.nudLocationY.TabIndex = 1;
            this.nudLocationY.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLocationY.ValueChanged += new System.EventHandler(this.ControlValueChanged);
            // 
            // btnSetVisiable
            // 
            this.btnSetVisiable.Location = new System.Drawing.Point(12, 217);
            this.btnSetVisiable.Name = "btnSetVisiable";
            this.btnSetVisiable.Size = new System.Drawing.Size(75, 23);
            this.btnSetVisiable.TabIndex = 2;
            this.btnSetVisiable.Text = "显示";
            this.btnSetVisiable.UseVisualStyleBackColor = true;
            this.btnSetVisiable.Click += new System.EventHandler(this.btnSetVisiable_Click);
            // 
            // FrmConfigVideoWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnSetVisiable);
            this.Controls.Add(this.nudLocationY);
            this.Controls.Add(this.nudWinHeight);
            this.Controls.Add(this.nudLocationX);
            this.Controls.Add(this.lbHeight);
            this.Controls.Add(this.nudWinWidth);
            this.Controls.Add(this.lbLocation);
            this.Controls.Add(this.lbWinWidth);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConfigVideoWindow";
            this.ShowIcon = false;
            this.Text = "配置视频窗口";
            ((System.ComponentModel.ISupportInitialize)(this.nudWinWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWinHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocationX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLocationY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbWinWidth;
        private System.Windows.Forms.NumericUpDown nudWinWidth;
        private System.Windows.Forms.Label lbHeight;
        private System.Windows.Forms.NumericUpDown nudWinHeight;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.NumericUpDown nudLocationX;
        private System.Windows.Forms.NumericUpDown nudLocationY;
        private System.Windows.Forms.Button btnSetVisiable;
    }
}