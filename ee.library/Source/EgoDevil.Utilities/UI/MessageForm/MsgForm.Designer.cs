using System.ComponentModel;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.MessageForm
{
    partial class MsgForm
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelMsg = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.labelTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelMsg
            // 
            this.labelMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMsg.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelMsg.Location = new System.Drawing.Point(63, 39);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(169, 66);
            this.labelMsg.TabIndex = 0;
            this.labelMsg.Text = "Show message here.";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(9, 39);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(48, 48);
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            this.pictureBox.Visible = false;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Location = new System.Drawing.Point(9, 118);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(29, 12);
            this.labelTime.TabIndex = 2;
            this.labelTime.Text = "time";
            // 
            // MsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ButtonStyle = EgoDevil.Utilities.UI.EForm.EFormCtrlButtons.EButtonStyle.MacStyle;
            this.ClientSize = new System.Drawing.Size(238, 142);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelMsg);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MsgForm";
            this.ShowInTaskbar = false;
            this.Text = "MsgForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Timer timer1;
        private Label labelMsg;
        private PictureBox pictureBox;
        private Label labelTime;
    }
}