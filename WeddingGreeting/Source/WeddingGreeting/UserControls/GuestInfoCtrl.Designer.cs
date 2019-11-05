namespace WeddingGreeting.UserControls
{
    partial class GuestInfoCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbbGuestType = new System.Windows.Forms.ComboBox();
            this.cbbGender = new System.Windows.Forms.ComboBox();
            this.txtEntourage = new System.Windows.Forms.TextBox();
            this.lbEntourage = new System.Windows.Forms.Label();
            this.txtSeatNo = new System.Windows.Forms.TextBox();
            this.lbSeatNo = new System.Windows.Forms.Label();
            this.lbTableNo = new System.Windows.Forms.Label();
            this.txtLabels = new System.Windows.Forms.TextBox();
            this.lbLabels = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lbEntourageDesc = new System.Windows.Forms.Label();
            this.lbIdDesc = new System.Windows.Forms.Label();
            this.lbGuestType = new System.Windows.Forms.Label();
            this.lbUserId = new System.Windows.Forms.Label();
            this.lbGender = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lbName = new System.Windows.Forms.Label();
            this.picbFacePicture = new System.Windows.Forms.PictureBox();
            this.lbCashGift = new System.Windows.Forms.Label();
            this.txtCashGift = new System.Windows.Forms.TextBox();
            this.lbTableName = new System.Windows.Forms.Label();
            this.btnAttendAction = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            this.cbbTables = new System.Windows.Forms.ComboBox();
            this.btnSnap = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            ((System.ComponentModel.ISupportInitialize)(this.picbFacePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // cbbGuestType
            // 
            this.cbbGuestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGuestType.FormattingEnabled = true;
            this.cbbGuestType.Items.AddRange(new object[] {
            "其他方",
            "新郎方",
            "新娘方"});
            this.cbbGuestType.Location = new System.Drawing.Point(275, 49);
            this.cbbGuestType.Name = "cbbGuestType";
            this.cbbGuestType.Size = new System.Drawing.Size(63, 20);
            this.cbbGuestType.TabIndex = 40;
            // 
            // cbbGender
            // 
            this.cbbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbGender.FormattingEnabled = true;
            this.cbbGender.Items.AddRange(new object[] {
            "未定义",
            "男",
            "女"});
            this.cbbGender.Location = new System.Drawing.Point(152, 48);
            this.cbbGender.Name = "cbbGender";
            this.cbbGender.Size = new System.Drawing.Size(67, 20);
            this.cbbGender.TabIndex = 39;
            // 
            // txtEntourage
            // 
            this.txtEntourage.Location = new System.Drawing.Point(152, 146);
            this.txtEntourage.Name = "txtEntourage";
            this.txtEntourage.Size = new System.Drawing.Size(186, 21);
            this.txtEntourage.TabIndex = 37;
            // 
            // lbEntourage
            // 
            this.lbEntourage.AutoSize = true;
            this.lbEntourage.Location = new System.Drawing.Point(87, 155);
            this.lbEntourage.Name = "lbEntourage";
            this.lbEntourage.Size = new System.Drawing.Size(59, 12);
            this.lbEntourage.TabIndex = 29;
            this.lbEntourage.Text = "随行人员:";
            this.lbEntourage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtSeatNo
            // 
            this.txtSeatNo.Location = new System.Drawing.Point(269, 96);
            this.txtSeatNo.Name = "txtSeatNo";
            this.txtSeatNo.Size = new System.Drawing.Size(67, 21);
            this.txtSeatNo.TabIndex = 36;
            // 
            // lbSeatNo
            // 
            this.lbSeatNo.AutoSize = true;
            this.lbSeatNo.Location = new System.Drawing.Point(226, 100);
            this.lbSeatNo.Name = "lbSeatNo";
            this.lbSeatNo.Size = new System.Drawing.Size(47, 12);
            this.lbSeatNo.TabIndex = 30;
            this.lbSeatNo.Text = "座位号:";
            // 
            // lbTableNo
            // 
            this.lbTableNo.AutoSize = true;
            this.lbTableNo.Location = new System.Drawing.Point(111, 101);
            this.lbTableNo.Name = "lbTableNo";
            this.lbTableNo.Size = new System.Drawing.Size(35, 12);
            this.lbTableNo.TabIndex = 31;
            this.lbTableNo.Text = "桌号:";
            // 
            // txtLabels
            // 
            this.txtLabels.Location = new System.Drawing.Point(152, 72);
            this.txtLabels.Name = "txtLabels";
            this.txtLabels.Size = new System.Drawing.Size(186, 21);
            this.txtLabels.TabIndex = 32;
            // 
            // lbLabels
            // 
            this.lbLabels.AutoSize = true;
            this.lbLabels.Location = new System.Drawing.Point(111, 76);
            this.lbLabels.Name = "lbLabels";
            this.lbLabels.Size = new System.Drawing.Size(35, 12);
            this.lbLabels.TabIndex = 22;
            this.lbLabels.Text = "标签:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(152, 1);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(108, 21);
            this.txtID.TabIndex = 34;
            // 
            // lbEntourageDesc
            // 
            this.lbEntourageDesc.AutoSize = true;
            this.lbEntourageDesc.Location = new System.Drawing.Point(101, 170);
            this.lbEntourageDesc.Name = "lbEntourageDesc";
            this.lbEntourageDesc.Size = new System.Drawing.Size(227, 12);
            this.lbEntourageDesc.TabIndex = 27;
            this.lbEntourageDesc.Text = "(以逗号分隔,若不知道名字则用数字代替)";
            // 
            // lbIdDesc
            // 
            this.lbIdDesc.AutoSize = true;
            this.lbIdDesc.Location = new System.Drawing.Point(256, 6);
            this.lbIdDesc.Name = "lbIdDesc";
            this.lbIdDesc.Size = new System.Drawing.Size(83, 12);
            this.lbIdDesc.TabIndex = 26;
            this.lbIdDesc.Text = "(英文名/拼音)";
            // 
            // lbGuestType
            // 
            this.lbGuestType.AutoSize = true;
            this.lbGuestType.Location = new System.Drawing.Point(220, 51);
            this.lbGuestType.Name = "lbGuestType";
            this.lbGuestType.Size = new System.Drawing.Size(59, 12);
            this.lbGuestType.TabIndex = 25;
            this.lbGuestType.Text = "贵宾类型:";
            // 
            // lbUserId
            // 
            this.lbUserId.AutoSize = true;
            this.lbUserId.Location = new System.Drawing.Point(123, 6);
            this.lbUserId.Name = "lbUserId";
            this.lbUserId.Size = new System.Drawing.Size(23, 12);
            this.lbUserId.TabIndex = 24;
            this.lbUserId.Text = "ID:";
            // 
            // lbGender
            // 
            this.lbGender.AutoSize = true;
            this.lbGender.Location = new System.Drawing.Point(111, 52);
            this.lbGender.Name = "lbGender";
            this.lbGender.Size = new System.Drawing.Size(35, 12);
            this.lbGender.TabIndex = 23;
            this.lbGender.Text = "性别:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(152, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(186, 21);
            this.txtName.TabIndex = 33;
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(111, 30);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 12);
            this.lbName.TabIndex = 28;
            this.lbName.Text = "姓名:";
            // 
            // picbFacePicture
            // 
            this.picbFacePicture.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picbFacePicture.Location = new System.Drawing.Point(1, 1);
            this.picbFacePicture.Name = "picbFacePicture";
            this.picbFacePicture.Size = new System.Drawing.Size(103, 132);
            this.picbFacePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbFacePicture.TabIndex = 38;
            this.picbFacePicture.TabStop = false;
            this.picbFacePicture.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.picbFacePicture_LoadCompleted);
            this.picbFacePicture.DoubleClick += new System.EventHandler(this.picbFacePicture_DoubleClick);
            this.picbFacePicture.MouseEnter += new System.EventHandler(this.picbFacePicture_MouseEnter);
            this.picbFacePicture.MouseLeave += new System.EventHandler(this.picbFacePicture_MouseLeave);
            // 
            // lbCashGift
            // 
            this.lbCashGift.AutoSize = true;
            this.lbCashGift.Location = new System.Drawing.Point(111, 197);
            this.lbCashGift.Name = "lbCashGift";
            this.lbCashGift.Size = new System.Drawing.Size(35, 12);
            this.lbCashGift.TabIndex = 30;
            this.lbCashGift.Text = "礼金:";
            // 
            // txtCashGift
            // 
            this.txtCashGift.Location = new System.Drawing.Point(152, 194);
            this.txtCashGift.Name = "txtCashGift";
            this.txtCashGift.Size = new System.Drawing.Size(186, 21);
            this.txtCashGift.TabIndex = 36;
            // 
            // lbTableName
            // 
            this.lbTableName.AutoSize = true;
            this.lbTableName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbTableName.Location = new System.Drawing.Point(150, 121);
            this.lbTableName.Name = "lbTableName";
            this.lbTableName.Size = new System.Drawing.Size(11, 12);
            this.lbTableName.TabIndex = 31;
            this.lbTableName.Text = ":";
            // 
            // btnAttendAction
            // 
            this.btnAttendAction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAttendAction.Location = new System.Drawing.Point(0, 185);
            this.btnAttendAction.Name = "btnAttendAction";
            this.btnAttendAction.TabIndex = 41;
            this.btnAttendAction.Text = "签到";
            this.btnAttendAction.Click += new System.EventHandler(this.btnAttendAction_Click);
            // 
            // cbbTables
            // 
            this.cbbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbTables.FormattingEnabled = true;
            this.cbbTables.Items.AddRange(new object[] {
            "未定义",
            "男",
            "女"});
            this.cbbTables.Location = new System.Drawing.Point(152, 98);
            this.cbbTables.Name = "cbbTables";
            this.cbbTables.Size = new System.Drawing.Size(67, 20);
            this.cbbTables.TabIndex = 39;
            this.cbbTables.SelectedIndexChanged += new System.EventHandler(this.cbbTables_SelectedIndexChanged);
            // 
            // btnSnap
            // 
            this.btnSnap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSnap.Location = new System.Drawing.Point(0, 137);
            this.btnSnap.Name = "btnSnap";
            this.btnSnap.TabIndex = 41;
            this.btnSnap.Text = "拍照";
            this.btnSnap.Click += new System.EventHandler(this.btnSnap_Click);
            // 
            // GuestInfoCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSnap);
            this.Controls.Add(this.btnAttendAction);
            this.Controls.Add(this.cbbGuestType);
            this.Controls.Add(this.cbbTables);
            this.Controls.Add(this.cbbGender);
            this.Controls.Add(this.picbFacePicture);
            this.Controls.Add(this.txtEntourage);
            this.Controls.Add(this.lbEntourage);
            this.Controls.Add(this.txtCashGift);
            this.Controls.Add(this.lbCashGift);
            this.Controls.Add(this.txtSeatNo);
            this.Controls.Add(this.lbSeatNo);
            this.Controls.Add(this.lbTableName);
            this.Controls.Add(this.lbTableNo);
            this.Controls.Add(this.txtLabels);
            this.Controls.Add(this.lbLabels);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.lbEntourageDesc);
            this.Controls.Add(this.lbIdDesc);
            this.Controls.Add(this.lbGuestType);
            this.Controls.Add(this.lbUserId);
            this.Controls.Add(this.lbGender);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lbName);
            this.Name = "GuestInfoCtrl";
            this.Size = new System.Drawing.Size(350, 229);
            this.Load += new System.EventHandler(this.GuestInfoCtrl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbFacePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbbGuestType;
        private System.Windows.Forms.ComboBox cbbGender;
        private System.Windows.Forms.PictureBox picbFacePicture;
        private System.Windows.Forms.TextBox txtEntourage;
        private System.Windows.Forms.Label lbEntourage;
        private System.Windows.Forms.TextBox txtSeatNo;
        private System.Windows.Forms.Label lbSeatNo;
        private System.Windows.Forms.Label lbTableNo;
        private System.Windows.Forms.TextBox txtLabels;
        private System.Windows.Forms.Label lbLabels;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lbEntourageDesc;
        private System.Windows.Forms.Label lbIdDesc;
        private System.Windows.Forms.Label lbGuestType;
        private System.Windows.Forms.Label lbUserId;
        private System.Windows.Forms.Label lbGender;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lbName;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnAttendAction;
        private System.Windows.Forms.Label lbCashGift;
        private System.Windows.Forms.TextBox txtCashGift;
        private System.Windows.Forms.Label lbTableName;
        private System.Windows.Forms.ComboBox cbbTables;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnSnap;
    }
}
