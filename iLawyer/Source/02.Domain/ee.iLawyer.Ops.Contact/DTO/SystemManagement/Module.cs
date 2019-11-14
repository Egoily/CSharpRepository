using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace ee.iLawyer.Ops.Contact.DTO.SystemManagement
{
    [AutoMap(typeof(Db.Entities.RBAC.SysModule))]
    public class Module
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }


        [Ignore]
        public virtual bool? IsDisabled { get; set; }


        public Module()
        {

        }

        public Module(Db.Entities.RBAC.SysModule source, bool isDisabled)
        {
            Id = source.Id;
            Code = source.Code;
            Name = source.Name;
            Description = source.Description;
            this.IsDisabled = isDisabled;

        }

    }
}
