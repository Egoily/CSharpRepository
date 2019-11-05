using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.UDNumber
{
    public partial class UDNumber : UserControl
    {
        private int _value;
        private int _maximum;
        private int _minimum;
        private int _increment;

        [Category("Data"), Description("Current value")]
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;

                OnValueChanged(null);
            }
        }

        [Category("Data"), Description("Maximum value")]
        public int Maximum
        {
            get
            { return _maximum; }
            set
            {
                _maximum = value;
                if (_maximum < _minimum)
                {
                    _maximum = _minimum;
                }
            }
        }

        [Category("Data"), Description("Minimum value ")]
        public int Minimum
        {
            get
            { return _minimum; }
            set
            {
                _minimum = value;
                if (_minimum > _maximum)
                {
                    _minimum = _maximum;
                }
            }
        }

        [Category("Data"), Description("Increment")]
        public int Increment
        {
            get
            {
                return _increment;
            }
            set
            {
                _increment = value;
                if (_increment <= 0)
                {
                    _increment = 1;
                }
            }
        }

        private Color _btnColor;

        public Color btnColor
        {
            get
            {
                return _btnColor;
            }
            set
            {
                _btnColor = value;
                btnMinus.BackColor = btnPlus.BackColor = _btnColor;
                this.Refresh();
            }
        }

        [Category("Data"), Description("The enable of changed event")]
        public bool IsFocused
        {
            get;
            set;
        }

        public delegate void CurrentValueChanged(object sender, EventArgs e);

        public event CurrentValueChanged ValueChanged;

        private void OnValueChanged(EventArgs e)
        {
            this.txtNumber.Text = Value.ToString();
            if (txtNumber.Text == _maximum.ToString())
            {
                btnPlus.Enabled = false;
            }
            else
            {
                btnPlus.Enabled = true;
            }
            if (txtNumber.Text == _minimum.ToString())
            {
                btnMinus.Enabled = false;
            }
            else
            {
                btnMinus.Enabled = true;
            }
            if (this.ValueChanged != null)
                this.ValueChanged(this, e);
        }

        public UDNumber()
        {
            InitializeComponent();
        }

        private void UDNumber_Load(object sender, EventArgs e)
        {
            _maximum = 10000;
            _minimum = 0;
            _increment = 1;
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            txtNumber.Text = (Math.Max(_minimum, Int32.Parse(txtNumber.Text.Trim()) - Increment)).ToString();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            txtNumber.Text = (Math.Min(_maximum, Int32.Parse(txtNumber.Text.Trim()) + Increment)).ToString();
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Value = Int32.Parse(txtNumber.Text);
            }
            catch (Exception ex)
            {
                this.Value = 0;
                Debug.Write(ex.Message);
            }
        }

        //textBox控件的KeyPress事件:只允许接受数字
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //IsNumber：指定字符串中位于指定位置的字符是否属于数字类别
            //IsPunctuation：指定字符串中位于指定位置的字符是否属于标点符号类别
            //IsControl：指定字符串中位于指定位置的字符是否属于控制字符类别
            if (!Char.IsNumber(e.KeyChar) && !Char.IsPunctuation(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;　//获取或设置一个值，指示是否处理过System.Windows.Forms.Control.KeyPress事件
            }
            else if (Char.IsPunctuation(e.KeyChar))
            {
                //不允许小数点
                e.Handled = true;
                //======================================================
                ////允许小数点
                //if (e.KeyChar == '.')
                //{
                //    if (((TextBox)sender).Text.LastIndexOf('.') != -1)
                //    {
                //        e.Handled = true;
                //    }
                //}
                //else
                //{
                //    e.Handled = true;
                //}
                //=====================================================
            }
        }

        private void UDNumber_Enter(object sender, EventArgs e)
        {
            IsFocused = true;
        }

        private void UDNumber_Leave(object sender, EventArgs e)
        {
            IsFocused = false;
        }
    }
}