using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.Docking
{
    internal class DummyControl : Control
    {
        public DummyControl()
        {
            SetStyle(ControlStyles.Selectable, false);
        }
    }
}