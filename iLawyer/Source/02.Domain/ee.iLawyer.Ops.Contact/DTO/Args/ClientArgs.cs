using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact;
using System.Collections.Generic;
using ee.Core.ComponentModel;
using AutoMapper;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;

namespace ee.iLawyer.Ops.Contact.Args
{

    public class SaveOrUpdateClientRequest : BaseRequest
    {
        /// <summary>
        /// 客户名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 属性列表
        /// </summary>
        [Required]
        public virtual IEnumerable<ClientProperty> ClientProperties { get; set; }

        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool IsNP { get; set; }
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

    }
    [AutoMap(typeof(DTO.ViewObjects.Client))]
    [AutoMap(typeof(Db.Entities.Client), ReverseMap = true)]
    public class CreateClientRequest : SaveOrUpdateClientRequest
    {
    }
    [AutoMap(typeof(DTO.ViewObjects.Client))]
    public class UpdateClientRequest : SaveOrUpdateClientRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
       
    }




    public class RemoveClientRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryClientRequest : BasePageRequest
    {
        public virtual int[] Keys { get; set; }
        /// <summary>
        /// 是否是自然人
        /// </summary>
        public virtual bool? IsNP { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
    }

}
