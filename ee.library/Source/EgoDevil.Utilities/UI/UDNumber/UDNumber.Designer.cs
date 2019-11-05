using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.UDNumber
{
    partial class UDNumber
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNumber = new TextBox();
            this.btnMinus = new Button();
            this.btnPlus = new Button();
            this.SuspendLayout();
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new Point(14, 0);
            this.txtNumber.Margin = new Padding(0);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new Size(38, 21);
            this.txtNumber.TabIndex = 0;
            this.txtNumber.Text = "0";
            this.txtNumber.TextChanged += new EventHandler(this.txtNumber_TextChanged);
            this.txtNumber.Leave += new EventHandler(this.UDNumber_Leave);
            this.txtNumber.KeyPress += new KeyPressEventHandler(this.textBox_KeyPress);
            this.txtNumber.Enter += new EventHandler(this.UDNumber_Enter);
            // 
            // btnMinus
            // 
            this.btnMinus.BackColor = Color.Transparent;
            this.btnMinus.Cursor = Cursors.PanWest;
            this.btnMinus.FlatStyle = FlatStyle.Popup;
            this.btnMinus.Image = Resources.down;
            this.btnMinus.Location = new Point(0, 0);
            this.btnMinus.Margin = new Padding(0);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new Size(14, 21);
            this.btnMinus.TabIndex = 1;
            this.btnMinus.UseVisualStyleBackColor = false;
            this.btnMinus.Click += new EventHandler(this.btnMinus_Click);
            this.btnMinus.Leave += new EventHandler(this.UDNumber_Leave);
            this.btnMinus.Enter += new EventHandler(this.UDNumber_Enter);
            // 
            // btnPlus
            // 
            this.btnPlus.BackColor = Color.Transparent;
            this.btnPlus.Cursor = Cursors.PanEast;
            this.btnPlus.FlatStyle = FlatStyle.Popup;
            this.btnPlus.Image = Resources.up;
            this.btnPlus.Location = new Point(52, 0);
            this.btnPlus.Margin = new Padding(0);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new Size(14, 21);
            this.btnPlus.TabIndex = 1;
            this.btnPlus.UseVisualStyleBackColor = false;
            this.btnPlus.Click += new EventHandler(this.btnPlus_Click);
            this.btnPlus.Leave += new EventHandler(this.UDNumber_Leave);
            this.btnPlus.Enter += new EventHandler(this.UDNumber_Enter);
            // 
            // UDNumber
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.btnPlus);
            this.Controls.Add(this.btnMinus);
            this.Controls.Add(this.txtNumber);
            this.Margin = new Padding(0);
            this.Name = "UDNumber";
            this.Size = new Size(66, 19);
            this.Load += new EventHandler(this.UDNumber_Load);
            this.Leave += new EventHandler(this.UDNumber_Leave);
            this.Enter += new EventHandler(this.UDNumber_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtNumber;
        private Button btnMinus;
        private Button btnPlus;
    }
}
