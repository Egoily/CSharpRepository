using System;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.MessageForm
{
    public partial class MsgForm : EForm.EForm
    {
        private bool display = true; //定义窗体是显示(true)还是消失(false)
        private int S_width = 0; //定义屏幕宽度
        private int S_height = 0; //定义屏幕高度
        private double count = 0; //定义一个用于延时的计数器

        private void Setting()
        {
            //指定窗体显示位置
            S_width = SystemInformation.WorkingArea.Width;//获取屏幕宽度
            S_height = SystemInformation.WorkingArea.Height;//获取屏幕高度
            //控制窗体渐变出现效果
            this.timer1.Enabled = true;//获取当前运行时间
            this.Opacity = 0.0;//获取当前窗体的透明度级别;

            labelTime.Text = DateTime.Now.ToString();
        }

        public MsgForm()
        {
            InitializeComponent();

            Setting();
        }

        public MsgForm(string message)
        {
            InitializeComponent();

            labelMsg.Text = message;
            this.Text = "";

            Setting();
        }

        public MsgForm(string message, string title)
        {
            InitializeComponent();

            labelMsg.Text = message;
            this.Text = title;
            this.pictureBox.Image = SystemIcons.Information.ToBitmap();

            Setting();
        }

        public MsgForm(string message, string title, MessageBoxIcon icon)
        {
            InitializeComponent();

            labelMsg.Text = message;
            this.Text = title;
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    this.MenuIcon = SystemIcons.Information.ToBitmap().GetThumbnailImage(28, 28, null, IntPtr.Zero);
                    this.pictureBox.Image = SystemIcons.Information.ToBitmap();
                    break;

                case MessageBoxIcon.Error:
                    this.MenuIcon = SystemIcons.Error.ToBitmap().GetThumbnailImage(28, 28, null, IntPtr.Zero);
                    this.pictureBox.Image = SystemIcons.Error.ToBitmap();
                    break;

                case MessageBoxIcon.Question:
                    this.pictureBox.Image = SystemIcons.Question.ToBitmap();
                    break;

                case MessageBoxIcon.Warning:
                    this.pictureBox.Image = SystemIcons.Warning.ToBitmap();
                    break;
            }

            Setting();
        }

        //窗体渐变出现效果
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (display)
            {
                if (this.Opacity < 1)
                {
                    this.Opacity = this.Opacity + 0.1;//窗体以0.1的速度渐变显示
                }
                else
                {
                    count = count + 0.1;
                    if (count >= 5) display = false; //当完全显示后，延时5秒自动渐变消失
                }
            }
            else
            {
                if (this.Opacity > 0)
                {
                    this.Opacity = this.Opacity - 0.1;//窗体以0.1的速度渐变消失
                }
                else
                {
                    this.timer1.Enabled = false;//时间为false
                    Close();//关闭窗体
                }
            }
            if (this.Opacity <= 1)
            {
                //指定窗体显示在右下角
                this.Location = new Point(S_width - this.Width, S_height - Convert.ToInt32(this.Height * this.Opacity));
            }
        }

        #region (* Static Methods *)

        public static void Show(string message)
        {
            MsgForm frmMsg = new MsgForm(message);
            frmMsg.Show();
        }

        public static void Show(string message, string title)
        {
            MsgForm frmMsg = new MsgForm(message, title);
            frmMsg.Show();
        }

        public static void Show(string message, string title, MessageBoxIcon icon)
        {
            MsgForm frmMsg = new MsgForm(message, title, icon);
            frmMsg.Show();
        }

        #endregion
    }
}