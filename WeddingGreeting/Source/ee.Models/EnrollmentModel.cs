using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.Models
{
    public class EnrollmentModel
    {
        public string ID { get; set; }
        public string 姓名 { get; set; }
        public string 性别 { get; set; }
        public string 宾客类型 { get; set; }
        public string 标签 { get; set; }
        public string 随行人员 { get; set; }
        public string 桌号 { get; set; }
        public byte[] 相片 { get; set; }



        public string Name
        {
            get
            {
                return 姓名;
            }
        }

        public int Gender
        {
            get
            {
                if (性别 == "男") return 1;
                else if (性别 == "女") return 2;
                else return 0;
            }

        }


        public int GuestType
        {
            get
            {
                if (宾客类型 == "新郎方") return 1;
                else if (宾客类型 == "新娘方") return 2;
                else return 0;
            }
        }

        public string Labels
        {
            get
            {
                return 标签;
            }
        }

        public string Entourage
        {
            get
            {
                return 随行人员;
            }
        }
        public string TableNo
        {
            get
            {
                return 桌号;
            }
        }
        public Image FaceImage
        {
            get
            {
                if (this.相片 == null) return null;
                try
                {
                    Image photo = null;
                    var bytes = 相片;
                    using (var ms = new MemoryStream(bytes))
                    {
                        ms.Write(bytes, 0, bytes.Length);
                        photo = Image.FromStream(ms, true);
                    }
                    return photo;
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

    }
}
