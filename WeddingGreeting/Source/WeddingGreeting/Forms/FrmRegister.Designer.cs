namespace WeddingGreeting.Forms
{
    partial class FrmRegister
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
            this.lbName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbLabels = new System.Windows.Forms.Label();
            this.txtLabels = new System.Windows.Forms.TextBox();
            this.lbTableNo = new System.Windows.Forms.Label();
            this.txtTableNo = new System.Windows.Forms.TextBox();
            this.picbFacePicture = new System.Windows.Forms.PictureBox();
            this.lbUserId = new System.Windows.Forms.Label();
            this.txtNamePinyin = new System.Windows.Forms.TextBox();
            this.lbIdDesc = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.cbbGender = new System.Windows.Forms.ComboBox();
            this.lbGuestType = new System.Windows.Forms.Label();
            this.cbbGuestType = new System.Windows.Forms.ComboBox();
            this.lbEntourage = new System.Windows.Forms.Label();
            this.txtEntourage = new System.Windows.Forms.TextBox();
            this.lbEntourageDesc = new System.Windows.Forms.Label();
            this.lbOperationResult = new System.Windows.Forms.Label();
            this.btnRegister = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            this.btnLoadFromFile = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            ((System.ComponentModel.ISupportInitialize)(this.picbFacePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(204, 57);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 12);
            this.lbName.TabIndex = 0;
            this.lbName.Text = "姓名:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(245, 54);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(174, 21);
            this.txtName.TabIndex = 1;
            // 
            // lbLabels
            // 
            this.lbLabels.AutoSize = true;
            this.lbLabels.Location = new System.Drawing.Point(204, 136);
            this.lbLabels.Name = "lbLabels";
            this.lbLabels.Size = new System.Drawing.Size(35, 12);
            this.lbLabels.TabIndex = 0;
            this.lbLabels.Text = "标签:";
            // 
            // txtLabels
            // 
            this.txtLabels.Location = new System.Drawing.Point(245, 133);
            this.txtLabels.Name = "txtLabels";
            this.txtLabels.Size = new System.Drawing.Size(174, 21);
            this.txtLabels.TabIndex = 1;
            // 
            // lbTableNo
            // 
            this.lbTableNo.AutoSize = true;
            this.lbTableNo.Location = new System.Drawing.Point(204, 163);
            this.lbTableNo.Name = "lbTableNo";
            this.lbTableNo.Size = new System.Drawing.Size(35, 12);
            this.lbTableNo.TabIndex = 0;
            this.lbTableNo.Text = "桌号:";
            // 
            // txtTableNo
            // 
            this.txtTableNo.Location = new System.Drawing.Point(245, 160);
            this.txtTableNo.Name = "txtTableNo";
            this.txtTableNo.Size = new System.Drawing.Size(174, 21);
            this.txtTableNo.TabIndex = 1;
            // 
            // picbFacePicture
            // 
            this.picbFacePicture.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picbFacePicture.Location = new System.Drawing.Point(12, 22);
            this.picbFacePicture.Name = "picbFacePicture";
            this.picbFacePicture.Size = new System.Drawing.Size(163, 186);
            this.picbFacePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbFacePicture.TabIndex = 2;
            this.picbFacePicture.TabStop = false;
            this.picbFacePicture.DoubleClick += new System.EventHandler(this.picbFacePicture_DoubleClick);
            this.picbFacePicture.MouseEnter += new System.EventHandler(this.picbFacePicture_MouseEnter);
            this.picbFacePicture.MouseLeave += new System.EventHandler(this.picbFacePicture_MouseLeave);
            // 
            // lbUserId
            // 
            this.lbUserId.AutoSize = true;
            this.lbUserId.Location = new System.Drawing.Point(216, 33);
            this.lbUserId.Name = "lbUserId";
            this.lbUserId.Size = new System.Drawing.Size(23, 12);
            this.lbUserId.TabIndex = 0;
            this.lbUserId.Text = "ID:";
            // 
            // txtNamePinyin
            // 
            this.txtNamePinyin.Location = new System.Drawing.Point(245, 30);
            this.txtNamePinyin.Name = "txtNamePinyin";
            this.txtNamePinyin.Size = new System.Drawing.Size(174, 21);
            this.txtNamePinyin.TabIndex = 1;
            // 
            // lbIdDesc
            // 
            this.lbIdDesc.AutoSize = true;
            this.lbIdDesc.Location = new System.Drawing.Point(425, 33);
            this.lbIdDesc.Name = "lbIdDesc";
            this.lbIdDesc.Size = new System.Drawing.Size(113, 12);
            this.lbIdDesc.TabIndex = 0;
            this.lbIdDesc.Text = "(英文名或中文拼音)";
            // 
            // lbGender
            // 
            this.lbGender.AutoSize = true;
            this.lbGender.Location = new System.Drawing.Point(204, 84);
            this.lbGender.Name = "lbGender";
            this.lbGender.Size = new System.Drawing.Size(35, 12);
            this.lbGender.TabIndex = 0;
            this.lbGender.Text = "性别:";
            // 
            // cbbGender
            // 
            this.cbbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGender.FormattingEnabled = true;
            this.cbbGender.Items.AddRange(new object[] {
            "未定义",
            "男",
            "女"});
            this.cbbGender.Location = new System.Drawing.Point(245, 81);
            this.cbbGender.Name = "cbbGender";
            this.cbbGender.Size = new System.Drawing.Size(174, 20);
            this.cbbGender.TabIndex = 4;
            // 
            // lbGuestType
            // 
            this.lbGuestType.AutoSize = true;
            this.lbGuestType.Location = new System.Drawing.Point(180, 110);
            this.lbGuestType.Name = "lbGuestType";
            this.lbGuestType.Size = new System.Drawing.Size(59, 12);
            this.lbGuestType.TabIndex = 0;
            this.lbGuestType.Text = "贵宾类型:";
            // 
            // cbbGuestType
            // 
            this.cbbGuestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGuestType.FormattingEnabled = true;
            this.cbbGuestType.Items.AddRange(new object[] {
            "其他方",
            "新郎方",
            "新娘方"});
            this.cbbGuestType.Location = new System.Drawing.Point(245, 107);
            this.cbbGuestType.Name = "cbbGuestType";
            this.cbbGuestType.Size = new System.Drawing.Size(174, 20);
            this.cbbGuestType.TabIndex = 4;
            // 
            // lbEntourage
            // 
            this.lbEntourage.AutoSize = true;
            this.lbEntourage.Location = new System.Drawing.Point(180, 190);
            this.lbEntourage.Name = "lbEntourage";
            this.lbEntourage.Size = new System.Drawing.Size(59, 12);
            this.lbEntourage.TabIndex = 0;
            this.lbEntourage.Text = "随行人员:";
            // 
            // txtEntourage
            // 
            this.txtEntourage.Location = new System.Drawing.Point(245, 187);
            this.txtEntourage.Name = "txtEntourage";
            this.txtEntourage.Size = new System.Drawing.Size(174, 21);
            this.txtEntourage.TabIndex = 1;
            // 
            // lbEntourageDesc
            // 
            this.lbEntourageDesc.AutoSize = true;
            this.lbEntourageDesc.Location = new System.Drawing.Point(425, 190);
            this.lbEntourageDesc.Name = "lbEntourageDesc";
            this.lbEntourageDesc.Size = new System.Drawing.Size(227, 12);
            this.lbEntourageDesc.TabIndex = 0;
            this.lbEntourageDesc.Text = "(以逗号分隔,若不知道名字则用数字代替)";
            // 
            // lbOperationResult
            // 
            this.lbOperationResult.AutoSize = true;
            this.lbOperationResult.BackColor = System.Drawing.Color.Blue;
            this.lbOperationResult.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOperationResult.ForeColor = System.Drawing.Color.GreenYellow;
            this.lbOperationResult.Location = new System.Drawing.Point(12, 211);
            this.lbOperationResult.Name = "lbOperationResult";
            this.lbOperationResult.Size = new System.Drawing.Size(82, 24);
            this.lbOperationResult.TabIndex = 0;
            this.lbOperationResult.Text = "......";
            // 
            // btnRegister
            // 
            this.btnRegister.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegister.Location = new System.Drawing.Point(302, 272);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.TabIndex = 5;
            this.btnRegister.Text = "录入";
            this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadFromFile.Location = new System.Drawing.Point(27, 272);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.TabIndex = 5;
            this.btnLoadFromFile.Text = "从文件加载";
            // 
            // FrmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 323);
            this.Controls.Add(this.btnRegister);
            this.Controls.Add(this.btnLoadFromFile);
            this.Controls.Add(this.cbbGuestType);
            this.Controls.Add(this.cbbGender);
            this.Controls.Add(this.picbFacePicture);
            this.Controls.Add(this.txtEntourage);
            this.Controls.Add(this.lbEntourage);
            this.Controls.Add(this.txtTableNo);
            this.Controls.Add(this.lbTableNo);
            this.Controls.Add(this.txtLabels);
            this.Controls.Add(this.lbLabels);
            this.Controls.Add(this.txtNamePinyin);
            this.Controls.Add(this.lbOperationResult);
            this.Controls.Add(this.lbEntourageDesc);
            this.Controls.Add(this.lbIdDesc);
            this.Controls.Add(this.lbGuestType);
            this.Controls.Add(this.lbUserId);
            this.Controls.Add(this.lbGender);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "人员录入";
            this.Load += new System.EventHandler(this.FrmRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbFacePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbLabels;
        private System.Windows.Forms.TextBox txtLabels;
        private System.Windows.Forms.Label lbTableNo;
        private System.Windows.Forms.TextBox txtTableNo;
        private System.Windows.Forms.PictureBox picbFacePicture;
        private System.Windows.Forms.Label lbUserId;
        private System.Windows.Forms.TextBox txtNamePinyin;
        private System.Windows.Forms.Label lbIdDesc;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.ComboBox cbbGender;
        private System.Windows.Forms.Label lbGuestType;
        private System.Windows.Forms.ComboBox cbbGuestType;
        private System.Windows.Forms.Label lbEntourage;
        private System.Windows.Forms.TextBox txtEntourage;
        private System.Windows.Forms.Label lbEntourageDesc;
        private System.Windows.Forms.Label lbOperationResult;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnLoadFromFile;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnRegister;
    }
}