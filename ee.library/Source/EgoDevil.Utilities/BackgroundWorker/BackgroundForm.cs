using System;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.BkWorker
{
    public partial class BackgroundForm : Form
    {
        public BackgroundForm()
        {
            InitializeComponent();
        }

        public BackgroundForm(Size size)
        {
            InitializeComponent();
            this.Size = size;
            panel1.Location = new Point((int)Math.Abs(size.Width - panel1.Width) / 2, (int)Math.Abs(size.Height - panel1.Height) / 2);
        }
    }
}