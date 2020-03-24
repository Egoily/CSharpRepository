using AutoMapper;
using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.Core.Net;
using System;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class SaveOrUpdateScheduleRequest : RequestBase
    {
        /// <summary>
        /// 事项名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public virtual UrgencyDegreeOfTodoItem Priority { get; set; }
        /// <summary>
        /// 是否设置提醒
        /// </summary>
        public virtual bool IsSetRemind { get; set; }
        /// <summary>
        /// 提醒时间
        /// </summary>
        public virtual DateTime? RemindTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public virtual DateTime ExpiredTime { get; set; }
        /// <summary>
        /// 事项内容
        /// </summary>
        public virtual string Content { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual StatusOfTodoItem Status { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime? CompletedTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 所属项目Id
        /// </summary>
        public virtual int? InProjectId { get; set; }
    }
    [AutoMap(typeof(DTO.ViewObjects.Schedule))]
    [AutoMap(typeof(Db.Entities.Schedule), ReverseMap = true)]
    public class CreateScheduleRequest : SaveOrUpdateScheduleRequest
    {

    }
    [AutoMap(typeof(DTO.ViewObjects.Judge))]
    public class UpdateScheduleRequest : SaveOrUpdateScheduleRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual string Id { get; set; }

    }
    public class RemoveScheduleRequest : RequestBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<string> Ids { get; set; }
    }
    public class QueryScheduleRequest : PageRequest
    {
        public virtual int[] Keys { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
    }

}
