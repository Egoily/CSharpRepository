using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using PropertyChanged;
namespace ee.iLawyer.Ops.Contact.DTO
{
    [AddINotifyPropertyChangedInterface]
    [DataContract(IsReference = true)]
    [AutoMap(typeof(Db.Entities.Foundation.Area), PreserveReferences = true)]//This is equivalent to a CreateMap<Db.Entities.Foundation.Area, DTO.Area>() configuration
    public class Area : ICloneable
    {
        [DataMember]
        [SourceMember(nameof(Db.Entities.Foundation.Area.Id))]
        /// <summary>
        /// 行政区划代码
        /// </summary>
        public virtual string AreaCode { get; set; }
        [DataMember]
        /// <summary>
        ///  行政区划名
        /// </summary>
        public virtual string Name { get; set; }
        [DataMember]
        /// <summary>
        /// 父行政区划
        /// </summary>
        public virtual Area Parent { get; set; }
        [DataMember]
        /// <summary>
        /// 辖下行政区划
        /// </summary>
        public virtual IList<Area> Children { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
