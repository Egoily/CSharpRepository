using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace ee.Core.Wpf.ExControls.DateTimePicker
{
    public enum DateTimePickerFormat { Long, Short, Time, Custom }

    [DefaultBindingProperty("Value")]
    [ToolboxBitmap(typeof(DateTimePicker), "DateTimePicker.bmp")]
    public class DateTimePicker : Control
    {
        private CheckBox _checkBox;
        internal TextBox _textBox;

        private Button _button;
        private Popup _popUp;
        private Calendar _calendar;
        private BlockManager _blockManager;
        private string _defaultFormat = "yyyy-MM-HH hh:mm:ss tt";

        [Category("DateTimePicker")]
        public bool ShowCheckBox
        {
            get { return _checkBox.Visibility == System.Windows.Visibility.Visible ? true : false; }
            set
            {
                if (value)
                {
                    _checkBox.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    _checkBox.Visibility = System.Windows.Visibility.Collapsed;
                    Checked = true;
                }
            }
        }


        [Category("DateTimePicker")]
        public bool ShowCalendarButton
        {
            get { return _button.Visibility == Visibility.Visible ? true : false; }
            set
            {
                if (value)
                {
                    _button.Visibility = Visibility.Visible;
                }
                else
                {
                    _button.Visibility = Visibility.Collapsed;
                }
            }
        }

        [Category("DateTimePicker")]
        public bool Checked
        {
            get { return _checkBox.IsChecked.HasValue ? _checkBox.IsChecked.Value : false; }
            set { _checkBox.IsChecked = value; }
        }
        [Category("DateTimePicker")]
        private string FormatString
        {
            get
            {
                switch (Format)
                {
                    case DateTimePickerFormat.Long:
                        return "dddd, MMMM dd, yyyy";
                    case DateTimePickerFormat.Short:
                        return "M/d/yyyy";
                    case DateTimePickerFormat.Time:
                        return "h:mm:ss tt";
                    case DateTimePickerFormat.Custom:
                        if (string.IsNullOrEmpty(CustomFormat))
                        {
                            return _defaultFormat;
                        }
                        else
                        {
                            return CustomFormat;
                        }

                    default:
                        return _defaultFormat;
                }
            }
        }
        private string _customFormat;
        [Category("DateTimePicker")]
        public string CustomFormat
        {
            get { return _customFormat; }
            set
            {
                _customFormat = value;
                _blockManager = new BlockManager(this, FormatString);
            }
        }
        private DateTimePickerFormat _format;
        [Category("DateTimePicker")]
        public DateTimePickerFormat Format
        {
            get { return _format; }
            set
            {
                _format = value;
                _blockManager = new BlockManager(this, FormatString);
            }
        }
        [Category("DateTimePicker")]
        public DateTime? Value
        {
            get
            {
                if (!Checked)
                {
                    return null;
                }

                return (DateTime?)GetValue(ValueProperty);
            }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?), typeof(DateTimePicker), new FrameworkPropertyMetadata(DateTime.Now, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(DateTimePicker.OnValueChanged), new CoerceValueCallback(DateTimePicker.CoerceValue), true, System.Windows.Data.UpdateSourceTrigger.PropertyChanged));

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as DateTimePicker)._blockManager.Render();
        }

        private static object CoerceValue(DependencyObject d, object value)
        {
            return value;
        }

        internal DateTime InternalValue
        {
            get
            {
                DateTime? value = Value;
                if (value.HasValue)
                {
                    return value.Value;
                }

                return DateTime.MinValue;
            }
        }

        public DateTimePicker()
        {
            Initializ();
            _blockManager = new BlockManager(this, FormatString);
        }

        private void Initializ()
        {
            Template = GetTemplate();
            ApplyTemplate();
            _checkBox = (CheckBox)Template.FindName("checkBox", this);
            _textBox = (TextBox)Template.FindName("textBox", this);

            _button = (Button)Template.FindName("button", this);
            _calendar = new Calendar();
            _popUp = new Popup();
            _popUp.PlacementTarget = _textBox;
            _popUp.Placement = PlacementMode.Bottom;
            _popUp.PopupAnimation = PopupAnimation.Fade;
            _popUp.StaysOpen = true;
            //_popUp.Child = _calendar;
            _checkBox.Checked += new RoutedEventHandler(_checkBox_Checked);
            _checkBox.Unchecked += new RoutedEventHandler(_checkBox_Checked);
            MouseWheel += new System.Windows.Input.MouseWheelEventHandler(DateTimePicker_MouseWheel);
            Focusable = false;
            _textBox.Cursor = System.Windows.Input.Cursors.Arrow;
            _textBox.AllowDrop = false;
            _textBox.GotFocus += new System.Windows.RoutedEventHandler(_textBox_GotFocus);
            _textBox.PreviewMouseUp += new System.Windows.Input.MouseButtonEventHandler(_textBox_PreviewMouseUp);
            _textBox.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(_textBox_PreviewKeyDown);
            _textBox.ContextMenu = null;
            _textBox.IsEnabled = Checked;
            _textBox.IsReadOnly = true;
            _textBox.IsReadOnlyCaretVisible = false;

            _button.Click += BtnCalender_Click;

            //_calendar.SelectedDatesChanged += new EventHandler<SelectionChangedEventArgs>(calendar_SelectedDatesChanged);
        }


        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalender_Click(object sender, RoutedEventArgs e)
        {
            if (_popUp.IsOpen)
            {
                _popUp.IsOpen = false;
            }
            else
            {
                TDateTimeView dtView = new TDateTimeView(_textBox.Text);

                dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
                {
                    Value = DateTime.Parse(dateTimeStr);
                    _blockManager.Render();
                    _popUp.IsOpen = false;
                };
                _popUp.Child = dtView;
                _popUp.IsOpen = true;

            }
        }

        private void _checkBox_Checked(object sender, RoutedEventArgs e)
        {
            _textBox.IsEnabled = Checked;
        }

        private void DateTimePicker_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            _blockManager.Change(((e.Delta < 0) ? -1 : 1), true);
        }

        private void _textBox_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            _blockManager.ReSelect();
        }

        private void _textBox_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _blockManager.ReSelect();
        }

        private void _textBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            byte b = (byte)e.Key;

            if (e.Key == System.Windows.Input.Key.Left)
            {
                _blockManager.Left();
            }
            else if (e.Key == System.Windows.Input.Key.Right)
            {
                _blockManager.Right();
            }
            else if (e.Key == System.Windows.Input.Key.Up)
            {
                _blockManager.Change(1, true);
            }
            else if (e.Key == System.Windows.Input.Key.Down)
            {
                _blockManager.Change(-1, true);
            }

            if (b >= 34 && b <= 43)
            {
                _blockManager.ChangeValue(b - 34);
            }

            if (b >= 74 && b <= 83)
            {
                _blockManager.ChangeValue(b - 74);
            }

            if (!(e.Key == System.Windows.Input.Key.Tab))
            {
                e.Handled = true;
            }
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Checked = true;
            ((sender as Calendar).Parent as Popup).IsOpen = false;
            DateTime selectedDate = (DateTime)e.AddedItems[0];
            Value = new DateTime(selectedDate.Year, selectedDate.Month, selectedDate.Day, InternalValue.Hour, InternalValue.Minute, InternalValue.Second);
            _blockManager.Render();
        }

        private ControlTemplate GetTemplate()
        {
            return (ControlTemplate)XamlReader.Parse(@"
        <ControlTemplate  xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                          xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
            <Border BorderBrush=""Black"" BorderThickness=""0"" CornerRadius=""1"">
                <StackPanel Orientation=""Horizontal"" VerticalAlignment=""Center"" HorizontalAlignment=""Left"" Background=""White"">
                    <CheckBox Name=""checkBox"" VerticalAlignment=""Center"" />
                    <TextBox Name=""textBox"" BorderThickness=""0""/>
                 

                   <Button x:Name=""button""                
                         Height= ""20""
                         Width= ""20""
                         HorizontalAlignment=""Right"">
                       <Button.Template>
                          <ControlTemplate TargetType=""{x:Type ButtonBase}"">
                            <Grid>
                                <Image Source=""/ee.Core.Wpf;component/ExControls/DateTimePicker/Resources/Calendar.png""/>
                            </Grid>
                          </ControlTemplate >
                        </Button.Template>
                   </Button>



                </StackPanel>
            </Border>
        </ControlTemplate>");

        }

        public override string ToString()
        {
            return InternalValue.ToString();
        }
    }

    internal class BlockManager
    {
        internal DateTimePicker _DateTimePicker;
        private List<Block> _blocks;
        private string _format;
        private Block _selectedBlock;
        private int _selectedIndex;
        public event EventHandler NeglectProposed;
        private string[] _supportedFormats = new string[] {
                "yyyy", "MMMM", "dddd",
                "yyy", "MMM", "ddd",
                "yy", "MM", "dd",
                "y", "M", "d",
                "HH", "H", "hh", "h",
                "mm", "m",
                "ss", "s",
                "tt", "t",
                "fff", "ff", "f",
                "K", "g"};

        public BlockManager(DateTimePicker DateTimePicker, string format)
        {
            Debug.WriteLine("BlockManager");
            _DateTimePicker = DateTimePicker;
            _format = format;
            _DateTimePicker.LostFocus += new RoutedEventHandler(_DateTimePicker_LostFocus);
            _blocks = new List<Block>();
            InitBlocks();
        }

        private void InitBlocks()
        {
            Debug.WriteLine("InitBlocks");
            foreach (string f in _supportedFormats)
            {
                _blocks.AddRange(GetBlocks(f));
            }

            _blocks = _blocks.OrderBy((a) => a.Index).ToList();
            _selectedBlock = _blocks[0];
            Render();
        }

        internal void Render()
        {
            int accum = 0;
            StringBuilder sb = new StringBuilder(_format);
            foreach (Block b in _blocks)
            {
                b.Render(ref accum, sb);
            }

            _DateTimePicker._textBox.Text = _format = sb.ToString();
            Select(_selectedBlock);
        }

        private List<Block> GetBlocks(string pattern)
        {
            Debug.WriteLine("GetBlocks");
            List<Block> bCol = new List<Block>();

            int index = -1;
            while ((index = _format.IndexOf(pattern, ++index)) > -1)
            {
                bCol.Add(new Block(this, pattern, index));
            }

            _format = _format.Replace(pattern, (0).ToString().PadRight(pattern.Length, '0'));
            return bCol;
        }

        internal void ChangeValue(int p)
        {
            _selectedBlock.Proposed = p;
            Change(_selectedBlock.Proposed, false);
        }

        internal void Change(int value, bool upDown)
        {
            _DateTimePicker.Value = _selectedBlock.Change(_DateTimePicker.InternalValue, value, upDown);
            if (upDown)
            {
                OnNeglectProposed();
            }

            Render();
        }

        internal void Right()
        {
            if (_selectedIndex + 1 < _blocks.Count)
            {
                Select(_selectedIndex + 1);
            }
        }

        internal void Left()
        {
            Debug.WriteLine("Left");
            if (_selectedIndex > 0)
            {
                Select(_selectedIndex - 1);
            }
        }

        private void _DateTimePicker_LostFocus(object sender, RoutedEventArgs e)
        {
            OnNeglectProposed();
        }

        protected virtual void OnNeglectProposed()
        {
            NeglectProposed?.Invoke(this, EventArgs.Empty);
        }

        internal void ReSelect()
        {
            foreach (Block b in _blocks)
            {
                if ((b.Index <= _DateTimePicker._textBox.SelectionStart) && ((b.Index + b.Length) >= _DateTimePicker._textBox.SelectionStart))
                { Select(b); return; }
            }

            Block bb = _blocks.Where((a) => a.Index < _DateTimePicker._textBox.SelectionStart).LastOrDefault();
            if (bb == null)
            {
                Select(0);
            }
            else
            {
                Select(bb);
            }
        }

        private void Select(int blockIndex)
        {
            if (_blocks.Count > blockIndex)
            {
                Select(_blocks[blockIndex]);
            }
        }

        private void Select(Block block)
        {
            if (!(_selectedBlock == block))
            {
                OnNeglectProposed();
            }

            _selectedIndex = _blocks.IndexOf(block);
            _selectedBlock = block;
            _DateTimePicker._textBox.Select(block.Index, block.Length);
        }
    }

    internal class Block
    {
        private BlockManager _blockManager;
        internal string Pattern { get; set; }
        internal int Index { get; set; }
        private int _length;
        internal int Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }
        private int _maxLength;
        private string _proposed;
        internal int Proposed
        {
            get
            {
                string p = _proposed;
                return int.Parse(p.PadLeft(Length, '0'));
            }
            set
            {
                if (!(_proposed == null) && _proposed.Length >= _maxLength)
                {
                    _proposed = value.ToString();
                }
                else
                {
                    _proposed = string.Format("{0}{1}", _proposed, value);
                }
            }
        }

        public Block(BlockManager blockManager, string pattern, int index)
        {
            _blockManager = blockManager;
            _blockManager.NeglectProposed += new EventHandler(_blockManager_NeglectProposed);
            Pattern = pattern;
            Index = index;
            Length = Pattern.Length;
            _maxLength = GetMaxLength(Pattern);
        }

        private int GetMaxLength(string p)
        {
            switch (p)
            {
                case "y":
                case "M":
                case "d":
                case "h":
                case "m":
                case "s":
                case "H":
                    return 2;
                case "yyy":
                    return 4;
                default:
                    return p.Length;
            }
        }

        private void _blockManager_NeglectProposed(object sender, EventArgs e)
        {
            _proposed = null;
        }

        internal DateTime Change(DateTime dateTime, int value, bool upDown)
        {
            if (!upDown && !CanChange())
            {
                return dateTime;
            }

            int y, m, d, h, n, s;
            y = dateTime.Year;
            m = dateTime.Month;
            d = dateTime.Day;
            h = dateTime.Hour;
            n = dateTime.Minute;
            s = dateTime.Second;

            if (Pattern.Contains("y"))
            {
                y = ((upDown) ? dateTime.Year + value : value);
            }
            else if (Pattern.Contains("M"))
            {
                m = ((upDown) ? dateTime.Month + value : value);
            }
            else if (Pattern.Contains("d"))
            {
                d = ((upDown) ? dateTime.Day + value : value);
            }
            else if (Pattern.Contains("h") || Pattern.Contains("H"))
            {
                h = ((upDown) ? dateTime.Hour + value : value);
            }
            else if (Pattern.Contains("m"))
            {
                n = ((upDown) ? dateTime.Minute + value : value);
            }
            else if (Pattern.Contains("s"))
            {
                s = ((upDown) ? dateTime.Second + value : value);
            }
            else if (Pattern.Contains("t"))
            {
                h = ((h < 12) ? (h + 12) : (h - 12));
            }

            if (y > 9999)
            {
                y = 1;
            }

            if (y < 1)
            {
                y = 9999;
            }

            if (m > 12)
            {
                m = 1;
            }

            if (m < 1)
            {
                m = 12;
            }

            if (d > DateTime.DaysInMonth(y, m))
            {
                d = 1;
            }

            if (d < 1)
            {
                d = DateTime.DaysInMonth(y, m);
            }

            if (h > 23)
            {
                h = 0;
            }

            if (h < 0)
            {
                h = 23;
            }

            if (n > 59)
            {
                n = 0;
            }

            if (n < 0)
            {
                n = 59;
            }

            if (s > 59)
            {
                s = 0;
            }

            if (s < 0)
            {
                s = 59;
            }

            return new DateTime(y, m, d, h, n, s);
        }

        private bool CanChange()
        {
            switch (Pattern)
            {
                case "MMMM":
                case "dddd":
                case "MMM":
                case "ddd":
                case "g":
                    return false;
                default:
                    return true;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Pattern, Index);
        }

        internal void Render(ref int accum, StringBuilder sb)
        {
            Index += accum;

            string f = _blockManager._DateTimePicker.InternalValue.ToString(Pattern + ",").TrimEnd(',');
            sb.Remove(Index, Length);
            sb.Insert(Index, f);
            accum += f.Length - Length;

            Length = f.Length;
        }
    }
}
