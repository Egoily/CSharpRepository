using ee.Core.Framework.Schema;
using System.Collections.Generic;
using ee.Core.ComponentModel;
using AutoMapper;
using ee.Core.Net;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class SaveOrUpdateJudgeRequest : RequestBase
    {
        /// <summary>
        /// 法官名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }
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
    }
    [AutoMap(typeof(DTO.ViewObjects.Judge))]
    [AutoMap(typeof(Db.Entities.Judge), ReverseMap = true)]
    public class CreateJudgeRequest : SaveOrUpdateJudgeRequest
    {
        
    }
    [AutoMap(typeof(DTO.ViewObjects.Judge))]
    public class UpdateJudgeRequest : SaveOrUpdateJudgeRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
   
    }
    public class RemoveJudgeRequest : RequestBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryJudgeRequest : PageRequest
    {
        public virtual int[] Keys { get; set; }
        /// <summary>
        /// 法官名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 法官等级
        /// </summary>
        public virtual string Grade { get; set; }
        /// <summary>
        /// 所属法院Id
        /// </summary>
        public virtual int? InCourtId { get; set; }
    }

}
