using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace ee.Core.Wpf.ExControls.DateTimePicker
{
    //在工具箱中 隐藏该窗体 
    [System.ComponentModel.DesignTimeVisible(false)]
    /// <summary>
    /// TDateTime.xaml 的交互逻辑
    /// </summary>
    public partial class TDateTimeView : UserControl
    {
        public string FormatString { get; set; }

        public TDateTimeView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="txt"></param>
        public TDateTimeView(string txt)
            : this()
        {
            this.dateTimeString = txt;
        }

        #region 全局变量

        /// <summary>
        /// 从 DateTimePicker 传入的日期时间字符串
        /// </summary>
        private string dateTimeString = string.Empty;


        #endregion   

        #region 事件

        /// <summary>
        /// TDateTimeView 窗体登录事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dt = DateTime.Now;
            if (!string.IsNullOrEmpty(dateTimeString))
            {
                DateTime.TryParse(dateTimeString, out dt);
            }

            calDate.SelectedDate = dt;
            calDate.DisplayDate = dt;
            textBlockhh.Text = dt.Hour.ToString("#00");
            textBlockmm.Text = dt.Minute.ToString("#00");
            textBlockss.Text = dt.Second.ToString("#00");

        }


        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBtnCloseView_Click(object sender, RoutedEventArgs e)
        {
            OnDateTimeContent(this.dateTimeString);
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DateTime? dt = new DateTime?();

            if (calDate.SelectedDate == null)
            {
                dt = DateTime.Now.Date;
            }
            else
            {
                dt = calDate.SelectedDate;
            }

            DateTime dtCal = Convert.ToDateTime(dt);

            string timeStr = "00:00:00";
            timeStr = textBlockhh.Text + ":" + textBlockmm.Text + ":" + textBlockss.Text;

            string dateStr;
            dateStr = dtCal.ToString("yyyy-MM-dd");

            string dateTimeStr;
            dateTimeStr = dateStr + " " + timeStr;


            if (!string.IsNullOrEmpty(FormatString))
            {
                var dateTime = DateTime.Parse(dateTimeStr);
                dateTimeStr = dateTime.ToString(FormatString);
            }
            OnDateTimeContent(dateTimeStr);

        }

        /// <summary>
        /// 当前按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNow_Click(object sender, RoutedEventArgs e)
        {
            popChioce.IsOpen = false;//THourView 或 TMinSexView 所在pop 的关闭动作

            if (btnNow.Content.Equals("零点"))
            {
                textBlockhh.Text = "00";
                textBlockmm.Text = "00";
                textBlockss.Text = "00";
                btnNow.Content = "当前";
                btnNow.Background = System.Windows.Media.Brushes.LightBlue;
            }
            else
            {
                DateTime dt = DateTime.Now;
                calDate.SelectedDate = dt;
                calDate.DisplayDate = dt;
                textBlockhh.Text = dt.Hour.ToString().PadLeft(2, '0');
                textBlockmm.Text = dt.Minute.ToString().PadLeft(2, '0');
                textBlockss.Text = dt.Second.ToString().PadLeft(2, '0');
                btnNow.Content = "零点";
                btnNow.Background = System.Windows.Media.Brushes.LightGreen;
            }


        }

        /// <summary>
        /// 小时点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBlockhh_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }
            else
            {
                THourView hourView = new THourView(textBlockhh.Text);// THourView 构造函数传递小时数据
                hourView.HourClick += (hourstr) => //THourView 点击所选小时后的 传递动作
                {

                    textBlockhh.Text = hourstr;
                    popChioce.IsOpen = false;//THourView 所在pop 的关闭动作
                };

                popChioce.Child = hourView;
                popChioce.IsOpen = true;
            }

        }
        private void textBlockhh_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            int value = Int32.Parse(tb.Text) + ((e.Delta < 0) ? -1 : 1);
            if (value < 0)
            {
                value = 23;
            }
            else if (value > 23)
            {
                value = 0;
            }

            tb.Text = value.ToString("#00");
        }

        /// <summary>
        /// 分钟点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBlockmm_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }
            else
            {
                TMinSecView minView = new TMinSecView(textBlockmm.Text);//TMinSexView 构造函数传递 分钟数据
                minView.MinClick += (minStr) => //TMinSexView 中 点击选择的分钟数据的 传递动作
                {

                    textBlockmm.Text = minStr;
                    popChioce.IsOpen = false;//TMinSexView 所在的 pop 关闭动作
                };

                popChioce.Child = minView;
                popChioce.IsOpen = true;
            }
        }
        private void textBlockmm_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            int value = Int32.Parse(tb.Text) + ((e.Delta < 0) ? -1 : 1);
            if (value < 0)
            {
                value = 59;
            }
            else if (value > 59)
            {
                value = 0;
            }

            tb.Text = value.ToString("#00");
        }

        /// <summary>
        /// 秒钟点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBlockss_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (popChioce.IsOpen == true)
            {
                popChioce.IsOpen = false;
            }
            else
            {
                //秒钟 跟分钟 都是60，所有秒钟共用 分钟的窗体即可
                TMinSecView secView = new TMinSecView(textBlockss.Text);//TMinSexView 构造函数 传入秒钟数据
                secView.textBlockTitle.Text = "秒    钟";//修改 TMinSecView 的标题名称为秒钟
                secView.MinClick += (secStr) => //TMinSexView 中 所选择确定的 秒钟数据 的传递动作
                {
                    textBlockss.Text = secStr;
                    popChioce.IsOpen = false;//TMinSexView 所在的 pop 关闭动作
                };

                popChioce.Child = secView;
                popChioce.IsOpen = true;
            }
        }
        private void textBlockss_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            int value = Int32.Parse(tb.Text) + ((e.Delta < 0) ? -1 : 1);
            if (value < 0)
            {
                value = 59;
            }
            else if (value > 59)
            {
                value = 0;
            }

            tb.Text = value.ToString("#00");
        }

        /// <summary>
        /// 解除calendar点击后 影响其他按钮首次点击无效的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calDate_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                Mouse.Capture(null);
            }
        }


        #endregion

        #region Action交互

        /// <summary>
        /// 时间确定后的传递事件
        /// </summary>
        public Action<string> DateTimeOK;

        /// <summary>
        /// 时间确定后传递的时间内容
        /// </summary>
        /// <param name="dateTimeStr"></param>
        protected void OnDateTimeContent(string dateTimeStr)
        {
            if (DateTimeOK != null)
            {
                DateTimeOK(dateTimeStr);
            }
        }






        #endregion


    }
}

