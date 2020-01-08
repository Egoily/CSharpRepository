using AutoMapper;
using ee.Core.Data;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{

    /// <summary>
    /// 法官信息
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    [AutoMap(typeof(Db.Entities.Judge))]
    public class Judge : CloneableObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public virtual Gender Gender { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public virtual string Duty { get; set; }
        /// <summary>
        /// 法官等级
        /// </summary>
        public virtual JudgeGrade Grade { get; set; }
        /// <summary>
        /// 所属法院Id
        /// </summary>
        public virtual int InCourtId { get; set; }
        /// <summary>
        /// 所属法院名称
        /// </summary>
        public virtual string InCourtName { get; set; }

        public Judge()
        {

        }
    }
}
