using ee.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeddingGreeting.Forms
{
    public partial class FrmTableLayout : Form
    {

        private int tableWidth = 200;
        public FrmTableLayout()
        {
            InitializeComponent();
            InitStage();
            InitTables();
        }
        private void InitStage()
        {
            eplStage.Location = new Point(Width / 2 - eplStage.Width / 2, 10);
        }

        private void InitTables()
        {
            if (TableConfig.TableControls.Any())
            {
                AddTables(TableConfig.TableControls);
            }
            else
            {
                AddTables();
            }
        }

        public void AddTable(string name, Point location, Size size, Color backColor, Color guestNameColor, Color tableColor, Color tableNameColor, string tableNo, string tableName, List<string> guests)
        {
            var no = tableNo;
            no = no.Replace("3A", "4");
            Int32.TryParse(no, out int index);


            var ctrl = new WeddingGreeting.UserControls.TableControl
            {
                TabIndex = index,
                Name = "tableControl" + tableNo,
                TableNo = tableNo,
                TableName = tableName,
                Location = location,
                Size = size,
                BackColor = backColor,
                GuestNameColor = guestNameColor,
                TableColor = tableColor,
                TableNameColor = tableNameColor,


            };
            if (guests != null)
            {
                foreach (var guest in guests)
                {
                    ctrl.NameList.Add(guest);
                }
            }
            this.Controls.Add(ctrl);

        }
        public void AddTable(TableControlData data)
        {
            AddTable(data.Name, data.Location, data.Size, data.BackColor, data.GuestNameColor, data.TableColor, data.TableNameColor, data.TableNo, data.TableName, data.Guests);
        }
        public void AddTable(string tableNo, string tableName, List<string> guests)
        {
            var no = tableNo;
            no = no.Replace("3A", "4");
            Int32.TryParse(no, out int index);

            Point location = new Point();
            if ((index + 1) % 2 == 0)
            {
                location = new Point((int)(Width / 2 - 1.5 * tableWidth), (tableWidth + 49) * ((index + 1) / 2));
            }
            else
            {
                location = new Point(Width / 2 + tableWidth / 2, (tableWidth + 49) * ((index + 1) / 2));
            }

            var name = "tableControl" + tableNo;
            var size = new System.Drawing.Size(tableWidth, tableWidth);
            var backColor = System.Drawing.Color.Transparent;
            var guestNameColor = System.Drawing.Color.Black;
            var tableColor = System.Drawing.Color.PeachPuff;
            var tableNameColor = System.Drawing.Color.Red;

            AddTable(name, location, size, backColor, guestNameColor, tableColor, tableNameColor, tableNo, tableName, guests);
            TableConfig.TableControls.Add(new TableControlData()
            {
                TabIndex = index,
                Name = "tableControl" + tableNo,
                TableNo = tableNo,
                TableName = tableName,
                Location = location,
                Size = size,
                BackColor = backColor,
                GuestNameColor = guestNameColor,
                TableColor = tableColor,
                TableNameColor = tableNameColor,
                Guests = guests,
            });
        }

        public void AddTables(List<TableControlData> tableDatas)
        {
            foreach (var data in tableDatas)
            {
                AddTable(data.Name, data.Location, data.Size, data.BackColor, data.GuestNameColor, data.TableColor, data.TableNameColor, data.TableNo, data.TableName, data.Guests);
            }
        }
        public void AddTables()
        {
            foreach (var table in GlobalConfigs.Configurations.Tables)
            {
                var guests = GlobalConfigs.Guests.Where(x => x.TableNo == table.Key).ToList().Select(x => x.Name).ToList();
                AddTable(table.Key, table.Value, guests);

            }
        }

        private void FrmTableLayout_FormClosing(object sender, FormClosingEventArgs e)
        {
            GlobalConfigMgr.SaveTables();
        }
    }
}
