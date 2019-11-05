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
    public partial class FrmConfigVideoWindow : Form
    {

        private Control control;
        private bool isLoaded = false;
        public FrmConfigVideoWindow(Control ctl)
        {
            InitializeComponent();
            control = ctl;
            nudWinWidth.Value = ctl.Width;
            nudWinHeight.Value = ctl.Height;
            nudLocationX.Value = ctl.Location.X;
            nudLocationY.Value = ctl.Location.Y;
            if (ctl.Visible)
            {
                btnSetVisiable.Text = "隐藏";
            }
            else
            {
                btnSetVisiable.Text = "显示";
            }
            isLoaded = true;
        }

        private void btnSetVisiable_Click(object sender, EventArgs e)
        {
            if (btnSetVisiable.Text == "显示")
            {
                control.Visible = true;
                btnSetVisiable.Text = "隐藏";
            }
            else
            {
                control.Visible = false;
                btnSetVisiable.Text = "显示";
            }
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {
            if (isLoaded)
            {
                control.Size = new Size((int)nudWinWidth.Value, (int)nudWinHeight.Value);
                control.Location = new Point((int)nudLocationX.Value, (int)nudLocationY.Value);
            }
        }
    }
}
