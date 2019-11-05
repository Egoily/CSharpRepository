using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGreeting
{
    public class TableConfig
    {


        public static List<TableControlData> TableControls { get; set; }
    }


    public class TableControlData
    {
        public int TabIndex { get; set; }
        public string Name { get; set; }
        public string TableNo { get; set; }
        public string TableName { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Color BackColor { get; set; }
        public Color GuestNameColor { get; set; }
        public Color TableColor { get; set; }
        public Color TableNameColor { get; set; }
        public List<string> Guests { get; set; }
    }
}
