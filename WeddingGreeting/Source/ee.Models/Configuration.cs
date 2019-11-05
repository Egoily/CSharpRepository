using ee.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.Models
{

    public class Configuration
    {
        /// <summary>
        /// 
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public float Threshold { get; set; }
        public float Speed { get; set; }
        /// <summary>
        /// 是否朗读
        /// </summary>
        public bool IsSpeechable { get; set; }
        /// <summary>
        /// 是否隐藏行头显示序号
        /// </summary>
        public bool IsHideHeaderCellIndex { get; set; }
        public bool IsGroupTable { get; set; }
        /// <summary>
        /// 问候语格式
        /// </summary>
        public string GreetFormat { get; set; }

        public Dictionary<string, string> Tables { get; set; }


        public Configuration()
        {
            GroupId = "EE";
            Threshold = 50f;
            Speed = 20f;
            IsSpeechable = true;
            GreetFormat = "{0},{1},欢迎光临黄广毅/高小娜夫妇的婚礼,请就坐 {2} 号桌.谢谢!";
            Tables = new Dictionary<string, string>() { };
            IsHideHeaderCellIndex = false;

        }





    }
}
