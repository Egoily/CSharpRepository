using AutoMapper;
using AutoMapper.Configuration.Annotations;
using ee.Core.Data;
using System;
using System.Collections.ObjectModel;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    [AutoMap(typeof(Db.Entities.Client))]
    public class Client : CloneableObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool IsNP { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 联系号码
        /// </summary>
        public virtual string ContactNo { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public virtual string Abbreviation { get; set; }
        /// <summary>
        /// 印象
        /// </summary>
        public virtual string Impression { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        [SourceMember(nameof(Db.Entities.Client.Properties))]
        public virtual ObservableCollection<ClientProperty> ClientProperties { get; set; }


        public Client()
        {
            ClientProperties = new ObservableCollection<ClientProperty>();
        }

    }
}
