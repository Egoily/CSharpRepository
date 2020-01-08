using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects
{
    [AutoMap(typeof(Db.Entities.Foundation.Picklist))]
    public class Picker
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Value { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string SubValue { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [SourceMember(nameof(Db.Entities.Foundation.Picklist.ParentId))]
        public virtual int? ParentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IList<Picker> Children { get; set; }

        public Picker()
        {

        }
    }
}
