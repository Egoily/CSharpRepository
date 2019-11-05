namespace WeddingGreeting.Forms
{
    partial class FrmTableLayout
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
            this.eplStage = new EgoDevil.Utilities.UI.EPanels.EPanel();
            this.SuspendLayout();
            // 
            // eplStage
            // 
            this.eplStage.AssociatedSplitter = null;
            this.eplStage.BackColor = System.Drawing.Color.Transparent;
            this.eplStage.CaptionFont = new System.Drawing.Font("Microsoft YaHei", 11.75F, System.Drawing.FontStyle.Bold);
            this.eplStage.CaptionHeight = 27;
            this.eplStage.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.eplStage.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.eplStage.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.eplStage.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.eplStage.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.eplStage.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.eplStage.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.eplStage.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.eplStage.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.eplStage.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.eplStage.CustomColors.ContentGradientBegin = System.Drawing.SystemColors.ButtonFace;
            this.eplStage.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.eplStage.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.eplStage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.eplStage.Image = global::WeddingGreeting.Properties.Resources.user_edit;
            this.eplStage.Location = new System.Drawing.Point(312, 12);
            this.eplStage.MinimumSize = new System.Drawing.Size(27, 27);
            this.eplStage.Name = "eplStage";
            this.eplStage.PanelStyle = EgoDevil.Utilities.UI.EPanels.PanelStyle.Office2007;
            this.eplStage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.eplStage.Size = new System.Drawing.Size(200, 100);
            this.eplStage.TabIndex = 1;
            this.eplStage.Text = "舞台";
            this.eplStage.ToolTipTextCloseIcon = null;
            this.eplStage.ToolTipTextExpandIconPanelCollapsed = null;
            this.eplStage.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // FrmTableLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(892, 598);
            this.Controls.Add(this.eplStage);
            this.MinimizeBox = false;
            this.Name = "FrmTableLayout";
            this.ShowIcon = false;
            this.Text = "FrmTableLayout";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTableLayout_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private EgoDevil.Utilities.UI.EPanels.EPanel eplStage;
    }
}