using AutoMapper;
using AutoMapper.Configuration.Annotations;
using ee.Core.Data;
using System;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    /// <summary>
    /// 客户属性项
    /// </summary>
    [AutoMap(typeof(Db.Entities.ClientProperty))]
    public class ClientProperty : CloneableObject
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[SourceMember(nameof(Db.Entities.ClientProperty.Picker.Id))]
        [ValueResolver(typeof(AutoMapper.PickerIdValueResolver))]
        public virtual int PickerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //[SourceMember(nameof(Db.Entities.ClientProperty.Picker.Name))]
        [ValueResolver(typeof(AutoMapper.PickerNameValueResolver))]
        public virtual string PickerName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int OrderNo { get; set; }

    }
}
