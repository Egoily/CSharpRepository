namespace WeddingGreeting.Forms
{
    partial class FrmGuestManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            ee.Models.GuestInfo guestInfo1 = new ee.Models.GuestInfo();
            this.dgvGuests = new System.Windows.Forms.DataGridView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tscbbFilter = new System.Windows.Forms.ToolStripComboBox();
            this.tslbName = new System.Windows.Forms.ToolStripLabel();
            this.tstxtName = new System.Windows.Forms.ToolStripTextBox();
            this.tsbQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tspbPercentage = new EgoDevil.Utilities.UI.EPanels.EToolStripProgressBar();
            this.tslbStatisticInfo = new System.Windows.Forms.ToolStripLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.btnCancelRegisterOrUpdate = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            this.btnRegisterOrUpdate = new EgoDevil.Utilities.UI.AquaButtons.AquaButton();
            this.guestInfoCtrl = new WeddingGreeting.UserControls.GuestInfoCtrl();
            this.colId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTableNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGenderStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGuestTypeStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLabels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntourage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEntourageNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSeatNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsAttendStr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttendTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCachGift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGuestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImagePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsAttend = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuests)).BeginInit();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGuests
            // 
            this.dgvGuests.AllowUserToAddRows = false;
            this.dgvGuests.AllowUserToDeleteRows = false;
            this.dgvGuests.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dgvGuests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGuests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colId,
            this.colTableNo,
            this.colTableName,
            this.colName,
            this.colGenderStr,
            this.colGuestTypeStr,
            this.colLabels,
            this.colEntourage,
            this.colEntourageNum,
            this.colSeatNo,
            this.colIsAttendStr,
            this.colAttendTime,
            this.colCachGift,
            this.colGender,
            this.colGuestType,
            this.colImagePath,
            this.colIsAttend,
            this.colCreateTime});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGuests.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGuests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGuests.Location = new System.Drawing.Point(0, 0);
            this.dgvGuests.MultiSelect = false;
            this.dgvGuests.Name = "dgvGuests";
            this.dgvGuests.ReadOnly = true;
            this.dgvGuests.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dgvGuests.RowTemplate.Height = 23;
            this.dgvGuests.RowTemplate.ReadOnly = true;
            this.dgvGuests.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGuests.Size = new System.Drawing.Size(795, 705);
            this.dgvGuests.TabIndex = 0;
            this.dgvGuests.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGuests_CellDoubleClick);
            this.dgvGuests.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvGuests_CellPainting);
            this.dgvGuests.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGuests_RowEnter);
            this.dgvGuests.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvGuests_RowStateChanged);
            this.dgvGuests.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvGuests_Scroll);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbEdit,
            this.tsbRemove,
            this.toolStripSeparator1,
            this.tscbbFilter,
            this.tslbName,
            this.tstxtName,
            this.tsbQuery,
            this.toolStripSeparator2,
            this.tspbPercentage,
            this.tslbStatisticInfo});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip.Size = new System.Drawing.Size(1151, 25);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip";
            // 
            // tsbNew
            // 
            this.tsbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNew.Image = global::WeddingGreeting.Properties.Resources.user_add;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(23, 22);
            this.tsbNew.Text = "新增";
            this.tsbNew.Click += new System.EventHandler(this.tsbNew_Click);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = global::WeddingGreeting.Properties.Resources.user_edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "编辑";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // tsbRemove
            // 
            this.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemove.Image = global::WeddingGreeting.Properties.Resources.user_del;
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(23, 22);
            this.tsbRemove.Text = "删除";
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tscbbFilter
            // 
            this.tscbbFilter.AutoCompleteCustomSource.AddRange(new string[] {
            "全部",
            "已签到",
            "未签到"});
            this.tscbbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbbFilter.Items.AddRange(new object[] {
            "全部",
            "已签到",
            "未签到"});
            this.tscbbFilter.Name = "tscbbFilter";
            this.tscbbFilter.Size = new System.Drawing.Size(121, 25);
            this.tscbbFilter.SelectedIndexChanged += new System.EventHandler(this.cbbFilter_SelectedIndexChanged);
            // 
            // tslbName
            // 
            this.tslbName.Name = "tslbName";
            this.tslbName.Size = new System.Drawing.Size(35, 22);
            this.tslbName.Text = "姓名:";
            // 
            // tstxtName
            // 
            this.tstxtName.Name = "tstxtName";
            this.tstxtName.Size = new System.Drawing.Size(100, 25);
            // 
            // tsbQuery
            // 
            this.tsbQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbQuery.Image = global::WeddingGreeting.Properties.Resources.user_query;
            this.tsbQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuery.Name = "tsbQuery";
            this.tsbQuery.Size = new System.Drawing.Size(23, 22);
            this.tsbQuery.Text = "搜索";
            this.tsbQuery.Click += new System.EventHandler(this.tsbQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tspbPercentage
            // 
            this.tspbPercentage.BackColor = System.Drawing.Color.Transparent;
            this.tspbPercentage.BackgroundColor = System.Drawing.Color.Silver;
            this.tspbPercentage.Name = "tspbPercentage";
            this.tspbPercentage.Size = new System.Drawing.Size(100, 22);
            this.tspbPercentage.Text = "1/100";
            this.tspbPercentage.ToolTipText = "已到人数/总人数";
            this.tspbPercentage.ValueColor = System.Drawing.Color.Lime;
            // 
            // tslbStatisticInfo
            // 
            this.tslbStatisticInfo.Name = "tslbStatisticInfo";
            this.tslbStatisticInfo.Size = new System.Drawing.Size(142, 22);
            this.tslbStatisticInfo.Text = "总数:{0} 已到:{1} 未到:{2}";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 25);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvGuests);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.btnCancelRegisterOrUpdate);
            this.splitContainer.Panel2.Controls.Add(this.btnRegisterOrUpdate);
            this.splitContainer.Panel2.Controls.Add(this.guestInfoCtrl);
            this.splitContainer.Size = new System.Drawing.Size(1151, 705);
            this.splitContainer.SplitterDistance = 795;
            this.splitContainer.TabIndex = 3;
            // 
            // btnCancelRegisterOrUpdate
            // 
            this.btnCancelRegisterOrUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelRegisterOrUpdate.Location = new System.Drawing.Point(221, 276);
            this.btnCancelRegisterOrUpdate.Name = "btnCancelRegisterOrUpdate";
            this.btnCancelRegisterOrUpdate.TabIndex = 1;
            this.btnCancelRegisterOrUpdate.Text = "取消";
            this.btnCancelRegisterOrUpdate.Click += new System.EventHandler(this.btnCancelRegisterOrUpdate_Click);
            // 
            // btnRegisterOrUpdate
            // 
            this.btnRegisterOrUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegisterOrUpdate.Location = new System.Drawing.Point(126, 276);
            this.btnRegisterOrUpdate.Name = "btnRegisterOrUpdate";
            this.btnRegisterOrUpdate.TabIndex = 1;
            this.btnRegisterOrUpdate.Text = "确定";
            this.btnRegisterOrUpdate.Click += new System.EventHandler(this.btnRegisterOrUpdate_Click);
            // 
            // guestInfoCtrl
            // 
            guestInfo1.AttendTime = null;
            guestInfo1.CashGift = "";
            guestInfo1.CreateTime = new System.DateTime(2018, 8, 31, 18, 0, 58, 782);
            guestInfo1.Entourage = "";
            guestInfo1.EntourageNum = 0;
            guestInfo1.Gender = 0;
            guestInfo1.GuestType = 0;
            guestInfo1.Id = "";
            guestInfo1.ImagePath = null;
            guestInfo1.IsAttend = false;
            guestInfo1.Labels = "";
            guestInfo1.Name = "";
            guestInfo1.ParentId = null;
            guestInfo1.SeatNo = "";
            guestInfo1.TableNo = "";
            this.guestInfoCtrl.Information = guestInfo1;
            this.guestInfoCtrl.Location = new System.Drawing.Point(3, 3);
            this.guestInfoCtrl.Name = "guestInfoCtrl";
            this.guestInfoCtrl.Size = new System.Drawing.Size(359, 239);
            this.guestInfoCtrl.TabIndex = 0;
            this.guestInfoCtrl.Tables = null;
            // 
            // colId
            // 
            this.colId.DataPropertyName = "Id";
            this.colId.HeaderText = "Id";
            this.colId.Name = "colId";
            this.colId.ReadOnly = true;
            this.colId.Visible = false;
            // 
            // colTableNo
            // 
            this.colTableNo.DataPropertyName = "TableNo";
            this.colTableNo.HeaderText = "桌号";
            this.colTableNo.Name = "colTableNo";
            this.colTableNo.ReadOnly = true;
            this.colTableNo.Width = 40;
            // 
            // colTableName
            // 
            this.colTableName.DataPropertyName = "TableName";
            this.colTableName.HeaderText = "桌名";
            this.colTableName.Name = "colTableName";
            this.colTableName.ReadOnly = true;
            this.colTableName.Width = 80;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "FullName";
            this.colName.HeaderText = "姓名";
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colGenderStr
            // 
            this.colGenderStr.DataPropertyName = "GenderStr";
            this.colGenderStr.HeaderText = "性别";
            this.colGenderStr.Name = "colGenderStr";
            this.colGenderStr.ReadOnly = true;
            this.colGenderStr.Width = 60;
            // 
            // colGuestTypeStr
            // 
            this.colGuestTypeStr.DataPropertyName = "GuestTypeStr";
            this.colGuestTypeStr.HeaderText = "贵宾类型";
            this.colGuestTypeStr.Name = "colGuestTypeStr";
            this.colGuestTypeStr.ReadOnly = true;
            this.colGuestTypeStr.Width = 80;
            // 
            // colLabels
            // 
            this.colLabels.DataPropertyName = "Labels";
            this.colLabels.HeaderText = "身份";
            this.colLabels.Name = "colLabels";
            this.colLabels.ReadOnly = true;
            // 
            // colEntourage
            // 
            this.colEntourage.DataPropertyName = "Entourage";
            this.colEntourage.HeaderText = "随行人员";
            this.colEntourage.Name = "colEntourage";
            this.colEntourage.ReadOnly = true;
            // 
            // colEntourageNum
            // 
            this.colEntourageNum.DataPropertyName = "EntourageNum";
            this.colEntourageNum.HeaderText = "随行人数";
            this.colEntourageNum.Name = "colEntourageNum";
            this.colEntourageNum.ReadOnly = true;
            this.colEntourageNum.Width = 80;
            // 
            // colSeatNo
            // 
            this.colSeatNo.DataPropertyName = "SeatNo";
            this.colSeatNo.HeaderText = "座位号";
            this.colSeatNo.Name = "colSeatNo";
            this.colSeatNo.ReadOnly = true;
            this.colSeatNo.Visible = false;
            // 
            // colIsAttendStr
            // 
            this.colIsAttendStr.DataPropertyName = "IsAttendStr";
            this.colIsAttendStr.HeaderText = "签到";
            this.colIsAttendStr.Name = "colIsAttendStr";
            this.colIsAttendStr.ReadOnly = true;
            this.colIsAttendStr.Width = 40;
            // 
            // colAttendTime
            // 
            this.colAttendTime.DataPropertyName = "AttendTime";
            this.colAttendTime.HeaderText = "签到时间";
            this.colAttendTime.Name = "colAttendTime";
            this.colAttendTime.ReadOnly = true;
            this.colAttendTime.Width = 120;
            // 
            // colCachGift
            // 
            this.colCachGift.DataPropertyName = "CachGift";
            this.colCachGift.HeaderText = "礼金";
            this.colCachGift.Name = "colCachGift";
            this.colCachGift.ReadOnly = true;
            // 
            // colGender
            // 
            this.colGender.HeaderText = "Gender";
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            this.colGender.Visible = false;
            // 
            // colGuestType
            // 
            this.colGuestType.HeaderText = "GuestType";
            this.colGuestType.Name = "colGuestType";
            this.colGuestType.ReadOnly = true;
            this.colGuestType.Visible = false;
            // 
            // colImagePath
            // 
            this.colImagePath.DataPropertyName = "ImagePath";
            this.colImagePath.HeaderText = "ImagePath";
            this.colImagePath.Name = "colImagePath";
            this.colImagePath.ReadOnly = true;
            this.colImagePath.Visible = false;
            // 
            // colIsAttend
            // 
            this.colIsAttend.DataPropertyName = "IsAttend";
            this.colIsAttend.HeaderText = "IsAttend";
            this.colIsAttend.Name = "colIsAttend";
            this.colIsAttend.ReadOnly = true;
            this.colIsAttend.Visible = false;
            // 
            // colCreateTime
            // 
            this.colCreateTime.DataPropertyName = "CreateTime";
            this.colCreateTime.HeaderText = "CreateTime";
            this.colCreateTime.Name = "colCreateTime";
            this.colCreateTime.ReadOnly = true;
            this.colCreateTime.Visible = false;
            // 
            // FrmGuestManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1151, 730);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip);
            this.MinimizeBox = false;
            this.Name = "FrmGuestManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "宾客管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGuestManagement_FormClosing);
            this.Load += new System.EventHandler(this.FrmGuestManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGuests)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGuests;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox tscbbFilter;
        private System.Windows.Forms.ToolStripLabel tslbName;
        private System.Windows.Forms.ToolStripTextBox tstxtName;
        private System.Windows.Forms.ToolStripButton tsbQuery;
        private System.Windows.Forms.ToolStripLabel tslbStatisticInfo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private EgoDevil.Utilities.UI.EPanels.EToolStripProgressBar tspbPercentage;
        private System.Windows.Forms.SplitContainer splitContainer;
        private UserControls.GuestInfoCtrl guestInfoCtrl;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnCancelRegisterOrUpdate;
        private EgoDevil.Utilities.UI.AquaButtons.AquaButton btnRegisterOrUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGenderStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGuestTypeStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLabels;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntourage;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEntourageNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSeatNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsAttendStr;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAttendTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCachGift;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGuestType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colImagePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIsAttend;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreateTime;
    }
}