using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.ColorLabel
{
    public partial class ColorLabel : Label
    {
        public ColorLabel()
        {
            InitializeComponent();
            this.TextAlign = ContentAlignment.MiddleLeft;
        }

        public ColorLabel(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            this.TextAlign = ContentAlignment.MiddleLeft;
        }

        protected override void OnClick(EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            dlg.AnyColor = true;
            dlg.AllowFullOpen = true;
            dlg.Color = this.BackColor;
            if (dlg.ShowDialog() == DialogResult.OK)
                this.BackColor = dlg.Color;
            base.OnClick(e);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            this.Text = this.BackColor.Name;
            if (BackColor.GetBrightness() > 0.5)
                this.ForeColor = Color.Black;
            else
                this.ForeColor = Color.White;
            base.OnBackColorChanged(e);
        }
    }
}