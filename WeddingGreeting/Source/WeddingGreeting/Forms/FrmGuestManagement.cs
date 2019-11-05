using ee.Models;
using EgoDevil.Utilities.UI.MessageForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WeddingGreeting.Forms
{
    public partial class FrmGuestManagement : Form
    {

        private bool isLoaded = false;
        public FrmGuestManagement()
        {
            InitializeComponent();
            dgvGuests.AutoGenerateColumns = false;
            tscbbFilter.SelectedIndex = 0;
            splitContainer.Panel2Collapsed = true;

            guestInfoCtrl.Tables = GlobalConfigs.Configurations.Tables;
        }


        private void SetAttendInformation()
        {

            var total = GlobalConfigs.Guests.Count(x => x.TableNo != "");
            var attendance = GlobalConfigs.Guests.Count(x => x.IsAttend);
            var notAttend = GlobalConfigs.Guests.Count(x => x.TableNo == "");

            tspbPercentage.Value = attendance * 100 / (total == 0 ? 1 : total);
            tspbPercentage.Text = $"{attendance}/{total}";
            tslbStatisticInfo.Text = $"总数:{total} 已到:{attendance} 未到:{total - attendance}";
        }
        private void FrmGuestManagement_Load(object sender, EventArgs e)
        {

            dgvGuests.DataSource = QueryGuests();

            SetAttendInformation();
            isLoaded = true;
        }




        private void cbbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            QueryByFilter();
        }

        private void QueryByFilter()
        {
            if (!isLoaded) return;
            if (tscbbFilter.Text == "已签到")
            {
                dgvGuests.DataSource = QueryGuestsByAttendance(true);

            }
            else if (tscbbFilter.Text == "未签到")
            {
                dgvGuests.DataSource = QueryGuestsByAttendance(false);
            }
            else
            {
                dgvGuests.DataSource = QueryGuestsByAttendance();
            }
            dgvGuests.Refresh();
        }
        private void QueryByName()
        {
            if (!isLoaded) return;

            bool? isAttend = null;
            if (tscbbFilter.Text == "已签到")
            {
                isAttend = true;
            }
            else if (tscbbFilter.Text == "未签到")
            {
                isAttend = false;
            }

            dgvGuests.DataSource = QueryGuests(isAttend, tstxtName.Text);
            dgvGuests.Refresh();
        }


        private List<GuestInfo> QueryGuestsByAttendance(bool? isAttend = null)
        {
            TableComparer comparer = new TableComparer();
            if (isAttend.HasValue)
            {
                return GlobalConfigs.Guests.Where(x => x.IsAttend == isAttend.Value).OrderBy(x => x.TableNo,comparer).ToList();
            }
            return GlobalConfigs.Guests.OrderBy(x => x.TableNo, comparer).ToList();
        }
        private List<GuestInfo> QueryGuests(bool? isAttend = null, string name = null)
        {
            var list = GlobalConfigs.Guests;
            if (isAttend.HasValue)
            {
                list = list.Where(x => x.IsAttend == isAttend.Value).ToList();
            }
            if (!string.IsNullOrEmpty(name))
            {
                list = list.Where(x => x.Name.Contains(name)).ToList();
            }
            TableComparer comparer = new TableComparer();
            return list.OrderBy(x => x.TableNo, comparer).ToList();
        }



        private void tsbNew_Click(object sender, EventArgs e)
        {

            splitContainer.Panel2Collapsed = false;
            guestInfoCtrl.Information = null;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (dgvGuests.SelectedRows == null || dgvGuests.SelectedRows.Count == 0) return;
            splitContainer.Panel2Collapsed = false;

            ShowCurrentRowData();
        }

        private void ShowCurrentRowData()
        {
            if (dgvGuests.SelectedRows == null || dgvGuests.SelectedRows.Count == 0) return;
            var obj = dgvGuests.SelectedRows[0];

            var guest = obj.DataBoundItem as GuestInfo;
            guestInfoCtrl.Information = guest;
        }

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            if (dgvGuests.SelectedRows == null) return;
            var obj = dgvGuests.SelectedRows[0];

            var guest = obj.DataBoundItem as GuestInfo;

            if (MessageBox.Show(this, $"确定要删除{guest.Name} 及其随行人员吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                GuestMgr.RemoveGuest(guest);
                QueryByFilter();
                dgvGuests.ClearSelection();
                splitContainer.Panel2Collapsed = true;
                LocateScrollBar();
            }
        }

        private void LocateScrollBar()
        {
            //滚动条定位
            dgvGuests.FirstDisplayedScrollingRowIndex = verticalScrollIndex;
            dgvGuests.HorizontalScrollingOffset = horizontalOffset;
        }

        private void tsbQuery_Click(object sender, EventArgs e)
        {
            QueryByName();
        }
        private void btnRegisterOrUpdate_Click(object sender, EventArgs e)
        {
            var val = guestInfoCtrl.Validation();
            if (val)
            {
                var para = guestInfoCtrl.Information;
                var thread = new EgoDevil.Utilities.BkWorker.BackgroundThread(Register);
                thread.RunWorkerCompleted += Thread_RunWorkerCompleted;
                thread.Start(para);
              
            }
            SetAttendInformation();
        }

        private void Thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool success = (bool)(e.Result);

            var message = success ? "录入成功" : "录入失败";
            MsgForm.Show(message, "提示", success ? MessageBoxIcon.None : MessageBoxIcon.Error);
            splitContainer.InvokeIfRequired(c => c.Panel2Collapsed = success);
            dgvGuests.InvokeIfRequired(c => QueryByFilter());
            LocateScrollBar();
        }

        private void btnCancelRegisterOrUpdate_Click(object sender, EventArgs e)
        {
            splitContainer.Panel2Collapsed = true;
        }

        private object Register(object obj)
        {
            var success = GuestMgr.SaveOrUpdateFace(obj as GuestInfo);

            return success;
        }
        private object Update(object obj)
        {
            var success = GuestMgr.UpdateGuestInfo(obj as GuestInfo);

            return success;
        }


        private void dgvGuests_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            ShowCurrentRowData();
        }

        private void dgvGuests_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGuests.SelectedRows == null || dgvGuests.SelectedRows.Count == 0) return;
            splitContainer.Panel2Collapsed = false;

            ShowCurrentRowData();
        }

        private void dgvGuests_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (!GlobalConfigs.Configurations.IsGroupTable) return;
            if (e.RowIndex > -1)
            {
                var tableNoString = (this.dgvGuests.Rows[e.RowIndex].Cells["colTableNo"].Value?.ToString());
                if (!string.IsNullOrEmpty(tableNoString))
                {
                    tableNoString = tableNoString.Replace("3A", "4");
                    tableNoString = tableNoString.Replace("6A", "7");
                    Int32.TryParse(tableNoString, out int tableNo);
                    if (tableNo % 2 == 0)
                    {
                        dgvGuests.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.AntiqueWhite;
                    }
                    else
                    {
                        dgvGuests.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Aqua;
                    }
                }
                else
                {
                    dgvGuests.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
                }

                //-------------------
                var isAttend = (this.dgvGuests.Rows[e.RowIndex].Cells["colIsAttend"].Value?.ToString() ?? "");
                if (isAttend.Equals("True"))
                {
                    //new Font("宋体", 9, FontStyle.Bold);
                    var font = new Font(dgvGuests.DefaultCellStyle.Font, FontStyle.Bold);
                    dgvGuests.Rows[e.RowIndex].DefaultCellStyle.Font = font;
                    //dgvGuests.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Red;
                }

            }

        }

        private void dgvGuests_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (isCosing) return;
            if (!GlobalConfigs.Configurations.IsHideHeaderCellIndex)
            {
                //显示在HeaderCell上
                for (int i = 0; i < this.dgvGuests.Rows.Count; i++)
                {
                    DataGridViewRow r = this.dgvGuests.Rows[i];
                    r.HeaderCell.Value = string.Format("{0}", i + 1);
                }

                this.dgvGuests.Refresh();
            }
        }
        bool isCosing = false;
        private void FrmGuestManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            isCosing = true;
        }
        int verticalScrollIndex = 0;
        int horizontalOffset = 0;
        private void dgvGuests_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                verticalScrollIndex = e.NewValue;
            }
            else if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                horizontalOffset = e.NewValue;
            }
        }
    }


    public class TableComparer : System.Collections.Generic.IComparer<string>
    {
        public int Compare(string x, string y)
        {
            if (x == "") x = "999";
            if (y == "") y = "999";
            return String.CompareOrdinal(x, y);
        }
    }
}
