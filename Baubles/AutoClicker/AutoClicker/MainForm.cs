using ee.Windows.Apis;
using ee.Windows.Enums;
using ee.Windows.Structs;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoClicker
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            //退出菜单项  
            MenuItem exit = new MenuItem("退出");
            exit.Click += new EventHandler(Exit);

            ////关联托盘控件  
            MenuItem[] childen = new MenuItem[] { exit };
            notifyIcon.ContextMenu = new ContextMenu(childen);
            RegisterHotKey();
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0312)
            {
                switch (m.WParam.ToInt32())
                {
                    case 1://启动 
                        BtnStart.PerformClick();
                        break;
                    case 2://停止
                        BtnStop.PerformClick();
                        break;
                    case 3://停止或退出
                        if (BtnStop.Enabled)
                        {
                            BtnStop.PerformClick();
                        }
                        else
                        {
                            Exit(null, null);
                        }

                        break;
                    case 4://设置光标
                        SetPosition();
                        break;
                    default:
                        break;
                }
                return;
            }
            base.WndProc(ref m);
        }



        private void Exit(object sender, EventArgs e)
        {
            //退出程序  
            System.Environment.Exit(0);
        }

        private void RegisterHotKey()
        {
            User32Api.RegisterHotKey(Handle, 1, KeyModifiers.None, Keys.F5);// 启动 
            User32Api.RegisterHotKey(Handle, 2, KeyModifiers.None, Keys.F9);//停止
            User32Api.RegisterHotKey(Handle, 3, KeyModifiers.None, Keys.Escape);//停止或退出
            User32Api.RegisterHotKey(Handle, 4, KeyModifiers.None, Keys.F8);//设置光标

        }

        private void AutoClick(Point point)
        {
            POINT p = new POINT();
            User32Api.GetCursorPos(out p);
            try
            {
                User32Api.SetCursorPos(point.X, point.Y);
                User32Api.mouse_event((MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                User32Api.mouse_event((MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
            }
            finally
            {
                User32Api.SetCursorPos(p.X, p.Y);
            }
        }

        //主线控制程对象
        private Thread controlThread;
        //线程主要处理的函数
        private void ThreadRunMethod()
        {
            while (true)
            {
                Point? cursorPosition = null;
                if (RbFollowCursor.Checked)
                {
                    SetPosition();
                    cursorPosition = Cursor.Position;
                }
                else
                {
                    if (TsslCursorPos.Tag != null && TsslCursorPos.Tag is Point)
                    {
                        cursorPosition = ((Point)TsslCursorPos.Tag);
                    }
                }
                if (cursorPosition.HasValue)
                {
                    AutoClick(cursorPosition.Value);
                }

                Thread.Sleep((int)NudTimeInterval.Value);
            }
        }

        private void Start()
        {
            try
            {
                controlThread = new Thread(new ThreadStart(ThreadRunMethod));
                controlThread.Start();
            }
            catch (Exception)
            {
                Application.DoEvents();
            }
            BtnStop.Enabled = true;
            BtnStart.Enabled = false;
            GbClickMode.Enabled = false;
            TsslStatus.Text = "启动";
        }

        private void Stop()
        {
            try
            {
                if (controlThread != null)
                {
                    controlThread.Abort();
                }
            }
            catch (Exception)
            {
                Application.DoEvents();
            }
            BtnStart.Enabled = true;
            GbClickMode.Enabled = true;
            BtnStop.Enabled = false;
            TsslStatus.Text = "停止";
        }

        private void SetPosition()
        {

            this.InvokeIfRequired(c =>
            {
                TsslCursorPos.Text = $"{Cursor.Position.X},{Cursor.Position.Y}";
                if (RbFixedCursor.Checked)
                {
                    TsslCursorPos.Tag = Cursor.Position;
                    TsslStatus.Text = "固定光标已设定";
                }

            });

        }


        private void BtnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            //鼠标左键单击  
            if (e.Button == MouseButtons.Left)
            {
                //如果窗体是可见的，那么鼠标左击托盘区图标后，窗体为不可见  
                if (this.Visible == true)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                    this.Activate();
                }
            }
        }
    }




}
