﻿using AutoMapper;
using ee.Core.Data;
using System;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    /// <summary>
    /// 待办事项
    /// </summary>
    [AutoMap(typeof(Db.Entities.Schedule))]
    [AutoMap(typeof(Db.Entities.Schedule), ReverseMap = true)]
    public class Schedule : CloneableObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }

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

        public Schedule()
        {
            ExpiredTime = DateTime.Now;
            CreateTime = DateTime.Now;
        }


    }
}
