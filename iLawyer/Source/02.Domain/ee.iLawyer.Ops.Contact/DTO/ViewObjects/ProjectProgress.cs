using AutoMapper;
using ee.Core.Data;
using System;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    /// <summary>
    /// 项目进展
    /// </summary>
    [AutoMap(typeof(Db.Entities.ProjectProgress))]
    [AutoMap(typeof(Db.Entities.ProjectProgress), ReverseMap = true)]
    public class ProjectProgress : CloneableObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        public virtual DateTime HandleTime { get; set; }

        /// <summary>
        /// 进展内容
        /// </summary>
        public virtual string Content { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}
