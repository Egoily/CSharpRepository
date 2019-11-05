namespace WeddingGreeting.Forms
{
    partial class FrmVideoSnap
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
            this.picbVideoContainer = new System.Windows.Forms.PictureBox();
            this.btnCancel = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            this.btnOk = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            this.btnSnap = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            ((System.ComponentModel.ISupportInitialize)(this.picbVideoContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // picbVideoContainer
            // 
            this.picbVideoContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.picbVideoContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picbVideoContainer.Location = new System.Drawing.Point(12, 12);
            this.picbVideoContainer.Name = "picbVideoContainer";
            this.picbVideoContainer.Size = new System.Drawing.Size(415, 305);
            this.picbVideoContainer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbVideoContainer.TabIndex = 1;
            this.picbVideoContainer.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnCancel.Location = new System.Drawing.Point(433, 265);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.AutoSize = true;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnOk.Location = new System.Drawing.Point(433, 113);
            this.btnOk.Name = "btnOk";
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnSnap
            // 
            this.btnSnap.AutoSize = true;
            this.btnSnap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSnap.Font = new System.Drawing.Font("SimSun", 14F);
            this.btnSnap.Location = new System.Drawing.Point(433, 33);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.TabIndex = 2;
            this.btnSnap.Text = "拍照";
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // FrmVideoSnap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 325);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnSnap);
            this.Controls.Add(this.picbVideoContainer);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVideoSnap";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "拍照取图";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVideoSnap_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.picbVideoContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picbVideoContainer;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnSnap;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnOk;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnCancel;
    }
}