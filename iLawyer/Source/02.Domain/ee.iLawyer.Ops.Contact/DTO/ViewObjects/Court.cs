using AutoMapper;
using ee.Core.ComponentModel;
using ee.Core.Data;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    /// <summary>
    /// 法院信息
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    [DataContract]
    [AutoMap(typeof(Db.Entities.Court))]
    public class Court: CloneableObject
    {

        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [DataGridColumn("Id",0)]
        public int Id { get; set; }

        /// <summary>
        /// 法院名
        /// </summary>
        [DataMember]
        [DataGridColumn("法院名", 1)]
        public string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [DataMember]
        [DataGridColumn("级别", 2)]
        public CourtRank Rank { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        [DataGridColumn("省份", 3)]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        [DataGridColumn("城市", 4)]
        public string City { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        [DataMember]
        [DataGridColumn("区域", 5)]
        public string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DataMember]
        [DataGridColumn("地址", 6)]
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DataMember]
        [DataGridColumn("联系电话", 7)]
        public string ContactNo { get; set; }


    }
}
