using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Linq;

namespace ee.Models
{
    /// <summary>
    /// 贵宾信息
    /// </summary>
    public class GuestInfo
    {
        public virtual string Id { get; set; }
        public virtual string ParentId { get; set; }
        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        /// <summary>
        /// 性别:0未定义,1男,2女
        /// </summary>
        public virtual int Gender { get; set; }
        [JsonIgnore]
        public virtual string GenderStr
        {
            get
            {
                if (Gender == 1) return "男";
                else if (Gender == 2) return "女";
                else return "未定义";
            }
        }


        /// <summary>
        /// 标签
        /// </summary>
        public virtual string Labels { get; set; }

        /// <summary>
        /// 贵宾类型(0其他方,1新郎方,2,新娘方)
        /// </summary>
        public virtual int GuestType { get; set; }
        [JsonIgnore]
        public virtual string GuestTypeStr
        {
            get
            {
                if (GuestType == 1) return "新郎方";
                else if (GuestType == 2) return "新娘方";
                else return "其他方";
            }
        }
        /// <summary>
        /// 随行人员(以逗号分隔)
        /// </summary>
        public virtual string Entourage { get; set; }

        /// <summary>
        /// 随行人人数
        /// </summary>
        public virtual int EntourageNum { get; set; }

        /// <summary>
        /// 桌号
        /// </summary>
        public virtual string TableNo { get; set; }
        [JsonIgnore]
        public virtual string TableName
        {
            get
            {
                if (GlobalConfigs.Configurations == null || GlobalConfigs.Configurations.Tables == null)
                {
                    return TableNo;
                }
                var tableName = GlobalConfigs.Configurations.Tables.FirstOrDefault(x => x.Key == TableNo);
                if (tableName.Key == null)
                    return TableNo;
                else
                    return tableName.Value;
            }

        }

        /// <summary>
        /// 座位号
        /// </summary>
        public virtual string SeatNo { get; set; }

        /// <summary>
        /// 是否已出席
        /// </summary>
        public virtual bool IsAttend { get; set; }
        [JsonIgnore]
        public virtual string IsAttendStr
        {
            get
            {
                if (IsAttend) return "是";
                else return "否";
            }
        }
        /// <summary>
        /// 签到时间
        /// </summary>
        public virtual DateTime? AttendTime { get; set; }
        /// <summary>
        /// 相片路径
        /// </summary>
        public virtual string ImagePath { get; set; }
        [JsonIgnore]
        public virtual Image Image
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath)) return null;
                try
                {
                    var img = Bitmap.FromFile(ImagePath);

                    var bmp = new Bitmap(img);
                    img.Dispose();
                    return bmp;
                }
                catch (Exception)
                {
                    return null;

                }

            }
        }

        /// <summary>
        /// 礼金
        /// </summary>
        public virtual string CashGift { get; set; }

        public virtual DateTime CreateTime { get; set; }


        public virtual GuestInfo Clone()
        {
            return new GuestInfo()
            {
                Id = this.Id,
                ParentId = this.ParentId,
                Name = this.Name,
                FullName = this.FullName,
                Labels = this.Labels,
                Gender = this.Gender,
                GuestType = this.GuestType,
                TableNo = this.TableNo,
                SeatNo = this.SeatNo,
                CashGift = this.CashGift,
                ImagePath = this.ImagePath,
                Entourage = this.Entourage,
                EntourageNum = this.EntourageNum,
                IsAttend = this.IsAttend,
                AttendTime = this.AttendTime,
                CreateTime = this.CreateTime,
            };
        }
    }
}