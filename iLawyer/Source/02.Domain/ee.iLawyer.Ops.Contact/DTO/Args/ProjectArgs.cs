using AutoMapper;
using AutoMapper.Configuration.Annotations;
using ee.Core.ComponentModel;
using ee.Core.Framework.Schema;
using ee.Core.Net;
using System;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class SaveOrUpdateProjectRequest : RequestBase
    {
        /// <summary>
        /// 项目类型代码
        /// </summary>
        [Required]
        public virtual int CategoryId { get; set; }
        /// <summary>
        /// 项目案由代码
        /// </summary>
        [Required]
        public virtual int CauseId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string Code { get; set; }
        [Required]
        /// <summary>
        /// 项目等级
        /// </summary>
        public virtual string Level { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }
        /// <summary>
        /// 收案日期
        /// </summary>
        public virtual DateTime DealDate { get; set; }

        /// <summary>
        /// 关联客户
        /// </summary>
        [Ignore]
        public virtual IList<int> InvolvedClientIds { get; set; }
        /// <summary>
        /// 其他当事人
        /// </summary>
        public virtual string OtherLitigant { get; set; }
        /// <summary>
        /// 相关方
        /// </summary>
        public virtual string InterestedParty { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public virtual int OwnerId { get; set; }
        /// <summary>
        /// 项目帐目
        /// </summary>
        public virtual DTO.ViewObjects.ProjectAccount Account { get; set; }
        /// <summary>
        /// 待办事项
        /// </summary>
        public virtual IEnumerable<DTO.ViewObjects.Schedule> TodoList { get; set; }
        /// <summary>
        /// 项目进展
        /// </summary>
        public virtual IEnumerable<DTO.ViewObjects.ProjectProgress> Progresses { get; set; }

    }
    [AutoMap(typeof(DTO.ViewObjects.Project))]
    [AutoMap(typeof(Db.Entities.Project), ReverseMap = true)]
    public class CreateProjectRequest : SaveOrUpdateProjectRequest
    {

    }
    [AutoMap(typeof(DTO.ViewObjects.Project))]
    public class UpdateProjectRequest : SaveOrUpdateProjectRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }

        /// <summary>
        /// 联系人/电话
        /// </summary>
        public virtual string Contacts { get; set; }


    }
    public class RemoveProjectRequest : RequestBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryProjectRequest : PageRequest
    {
        public virtual int[] Keys { get; set; }


        /// <summary>
        /// 项目类别码
        /// </summary>
        public virtual string CategoryCode { get; set; }
        /// <summary>
        /// 项目类别码
        /// </summary>
        public virtual string CauseCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 项目等级
        /// </summary>
        public virtual string Level { get; set; }
        /// <summary>
        /// 收案日期(开始)
        /// </summary>
        public virtual string DealDateFrom { get; set; }
        /// <summary>
        /// 收案日期(结束)
        /// </summary>
        public virtual string DealDateTo { get; set; }

        /// <summary>
        /// 所属
        /// </summary>
        public virtual int? OwnerId { get; set; }


    }


    public class GetProjectCategoriesRequest : RequestBase
    {

    }
    public class GetProjectCausesRequest : RequestBase
    {

    }
    public class SaveOrUpdateProjectTodoListRequest : RequestBase
    {
        public virtual int ProjectId { get; set; }
        public virtual IEnumerable<DTO.ViewObjects.Schedule> TodoList { get; set; }
    }
    public class SaveOrUpdateProjectProgressRequest : RequestBase
    {
        public virtual int ProjectId { get; set; }
        public virtual IEnumerable<DTO.ViewObjects.ProjectProgress> Progresses { get; set; }
    }
}
