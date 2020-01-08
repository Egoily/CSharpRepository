using AutoMapper;
using AutoMapper.Configuration.Annotations;
using ee.Core.Data;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    [AddINotifyPropertyChangedInterface]
    [AutoMap(typeof(Db.Entities.Project))]
    public class Project : CloneableObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual Picker Category { get; set; }
        /// <summary>
        /// 案由
        /// </summary>
        public virtual Picker Cause { get; set; }

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
        public virtual ProjectLevel Level { get; set; }
        /// <summary>
        /// 关联客户列表
        /// </summary>
        [Ignore]
        public virtual ObservableCollection<Client> InvolvedClients { get; set; }
        /// <summary>
        /// 其他当事人
        /// </summary>
        public virtual string OtherLitigant { get; set; }
        /// <summary>
        /// 相关方
        /// </summary>
        public virtual string InterestedParty { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public virtual string Details { get; set; }
        /// <summary>
        /// 收案日期
        /// </summary>
        public virtual DateTime DealDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 帐目
        /// </summary>
        [Ignore]
        public virtual ProjectAccount Account { get; set; }
        /// <summary>
        /// 待办事项
        /// </summary>
        [Ignore]
        public virtual ObservableCollection<Schedule> TodoList { get; set; }
        /// <summary>
        /// 项目进展
        /// </summary>
        [Ignore]
        public virtual ObservableCollection<ProjectProgress> Progresses { get; set; }

        public Project()
        {
            Account = new ProjectAccount();
            InvolvedClients = new ObservableCollection<Client>();
            DealDate = DateTime.Now;
            CreateTime = DateTime.Now;
            TodoList = new ObservableCollection<Schedule>();
            Progresses = new ObservableCollection<ProjectProgress>();

        }

    }
}
