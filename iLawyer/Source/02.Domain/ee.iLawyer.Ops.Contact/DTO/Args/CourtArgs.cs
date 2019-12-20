using ee.Core.Framework.Schema;
using System.Collections.Generic;
using ee.Core.ComponentModel;
using AutoMapper;

namespace ee.iLawyer.Ops.Contact.Args
{

    public class SaveOrUpdateCourtRequest : BaseRequest
    {
        /// <summary>
        /// 法院名
        /// </summary>
        [Required]
        public virtual string Name { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [Required]
        public virtual string Rank { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [Required]
        public virtual string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [Required]
        public virtual string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [Required]
        public virtual string County { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public virtual string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string ContactNo { get; set; }

    }
    [AutoMap(typeof(DTO.ViewObjects.Court))]
    [AutoMap(typeof(Db.Entities.Court), ReverseMap = true)]
    public class CreateCourtRequest : SaveOrUpdateCourtRequest
    {

    }
    [AutoMap(typeof(DTO.ViewObjects.Court))]
    public class UpdateCourtRequest : SaveOrUpdateCourtRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual int Id { get; set; }
       
    }
    public class RemoveCourtRequest : BaseRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public virtual IList<int> Ids { get; set; }
    }
    public class QueryCourtRequest : BasePageRequest
    {
        public virtual int[] Keys { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public string Rank { get; set; }
        /// <summary>
        /// 法院名
        /// </summary>
        public string Name { get; set; }



    }

}
