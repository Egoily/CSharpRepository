using System;
using System.Drawing;
using System.Windows.Forms;
using ee.Models;
using EgoDevil.Utilities.UI.MessageForm;
using System.Linq;
using System.Collections.Generic;
using WeddingGreeting.Forms;

namespace WeddingGreeting.UserControls
{
    public partial class GuestInfoCtrl : UserControl
    {


        private string currentImagePath;

        private GuestInfo information;
        public GuestInfo Information
        {
            get
            {
                if (information == null)
                {
                    if (picbFacePicture.Image == null)
                    {
                        currentImagePath = null;
                    }
                    information = new GuestInfo()
                    {
                        IsAttend = false,
                        AttendTime = null,
                        CreateTime = DateTime.Now,
                    };
                }

                information.Id = txtID.Text.Trim();
                information.Name = txtName.Text;
                information.Gender = cbbGender.SelectedIndex;
                information.GuestType = cbbGuestType.SelectedIndex;
                information.Labels = txtLabels.Text;
                information.TableNo = cbbTables.Text;
                information.SeatNo = txtSeatNo.Text;
                information.ImagePath = currentImagePath;
                if (!string.IsNullOrEmpty(information.ParentId))
                {
                    information.Entourage = "";
                }
                else
                {
                    information.Entourage = txtEntourage.Text ?? "";
                }
                information.CashGift = txtCashGift.Text;

                return information;
            }
            set
            {
                information = value?.Clone();
                IsPictureChanged = false;
                if (information != null)
                {
                    txtID.ReadOnly = true;
                    txtID.Text = information.Id;
                    txtName.Text = information.Name;
                    cbbGender.SelectedIndex = information.Gender;
                    cbbGuestType.SelectedIndex = information.GuestType;
                    txtLabels.Text = information.Labels;
                    cbbTables.Text = information.TableNo;

                    if (!string.IsNullOrEmpty(information.ParentId))
                    {
                        var parent = GlobalConfigs.Guests.FirstOrDefault(x => x.Id == information.ParentId);
                        lbEntourage.Text = "跟随:";
                        txtEntourage.Text = parent?.Name;
                        txtEntourage.ReadOnly = true;
                        lbEntourageDesc.Visible = false;
                        txtCashGift.Enabled = false;
                    }
                    else
                    {
                        lbEntourage.Text = "随行人员:";
                        txtEntourage.Text = information.Entourage;
                        txtEntourage.ReadOnly = false;
                        lbEntourageDesc.Visible = true;
                        txtCashGift.Enabled = true;
                    }



                    txtCashGift.Text = information.CashGift;

                    currentImagePath = information.ImagePath;

                    if (!string.IsNullOrEmpty(currentImagePath))
                    {
                        var img = Bitmap.FromFile(currentImagePath);
                        picbFacePicture.Image = new Bitmap(img);
                        img.Dispose();
                    }
                    else
                    {
                        picbFacePicture.Image = null;
                    }
                    btnAttendAction.Visible = true;
                    SetAttend(value.IsAttend);
                }
                else
                {
                    txtID.ReadOnly = false;
                    cbbGender.SelectedIndex = 0;
                    cbbGuestType.SelectedIndex = 0;
                    txtID.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtLabels.Text = string.Empty;
                    cbbTables.SelectedIndex = -1;
                    txtEntourage.Text = string.Empty;
                    txtCashGift.Text = string.Empty;
                    picbFacePicture.Image = null;
                    currentImagePath = null;
                    SetAttend(false);
                    btnAttendAction.Visible = false;

                    lbEntourage.Text = "随行人员:";
                    txtEntourage.ReadOnly = false;
                    lbEntourageDesc.Visible = true;
                    txtCashGift.Enabled = true;
                }

            }
        }



        public bool IsPictureChanged { get; private set; }
        public Dictionary<string, string> tables;
        public Dictionary<string, string> Tables
        {
            get => tables;
            set
            {
                tables = value;
                cbbTables.DisplayMember = "Key";   // Text，即显式的文本
                cbbTables.ValueMember = "Vaue";    // Value，即实际的值
                cbbTables.DataSource = tables?.ToArray() ?? null;
                Refresh();
            }
        }


        public GuestInfoCtrl()
        {
            InitializeComponent();
        }
        private void SetAttend(bool isAttend)
        {
            if (isAttend)
            {
                btnAttendAction.Text = "取消签到";
            }
            else
            {
                btnAttendAction.Text = "签到";
            }
        }

        public bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();
                MsgForm.Show("请输入姓名");
                return false;
            }
            if (string.IsNullOrEmpty(txtLabels.Text))
            {
                txtLabels.Focus();
                MsgForm.Show("请输入标签");
                return false;
            }
            //if (string.IsNullOrEmpty(cbbTables.Text))
            //{
            //    cbbTables.Focus();
            //    MsgForm.Show("请选择桌号");
            //    return false;
            //}
            //if (picbFacePicture.Image == null && string.IsNullOrEmpty(information.ParentId))
            //{
            //    picbFacePicture.Focus();
            //    MsgForm.Show("请选择相片");
            //    return false;

            //}
            return true;
        }

        public void Clear()
        {

        }
        private void ShowTableInfo(string tableNo)
        {
            lbTableName.Text = "";
            if (GlobalConfigs.Configurations != null && GlobalConfigs.Configurations.Tables != null && GlobalConfigs.Configurations.Tables.Any())
            {
                var tableName = GlobalConfigs.Configurations.Tables.FirstOrDefault(x => x.Key == tableNo);
                if (tableName.Key != null)
                {
                    lbTableName.Text = tableName.Value;

                    var count = GlobalConfigs.Guests.Count(x => x.TableNo == tableNo);
                    lbTableName.Text += $"  [已坐 {count} 人]";
                }
            }
        }

        private void GuestInfoCtrl_Load(object sender, EventArgs e)
        {
            cbbGender.SelectedIndex = 0;
            cbbGuestType.SelectedIndex = 0;
        }
        private void picbFacePicture_DoubleClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "(*.jpg,*.png,*.jpeg,*.bmp)|*.jpg;*.png;*.jpeg;*.bmp",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = ofd.FileName;
                var image = Bitmap.FromFile(ofd.FileName);
                if (image != null)
                {
                    picbFacePicture.Image = new Bitmap(image);
                    image.Dispose();
                }
                IsPictureChanged = true;
            }
        }

        private void picbFacePicture_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void picbFacePicture_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void picbFacePicture_LoadCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            IsPictureChanged = true;
        }

        private void btnAttendAction_Click(object sender, EventArgs e)
        {
            btnAttendAction.Enabled = false;
            if (btnAttendAction.Text == "签到")
            {
                btnAttendAction.Text = "取消签到";
                information.IsAttend = true;
                information.AttendTime = DateTime.Now;
            }
            else
            {
                btnAttendAction.Text = "签到";
                information.IsAttend = false;
                information.AttendTime = null;
            }
            btnAttendAction.Enabled = true;

        }



        private void cbbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowTableInfo(cbbTables.Text);
        }

        private void btnSnap_Click(object sender, EventArgs e)
        {
            var frm = new FrmVideoSnap();
            frm.ShowDialog(this);
            if (frm.DlgResult == DialogResult.OK)
            {
                picbFacePicture.Image = frm.Image;
                currentImagePath = frm.ImagePath;
            }
        }
    }
}
