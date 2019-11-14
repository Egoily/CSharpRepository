using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.DTO.SystemManagement
{
    [AutoMap(typeof(Db.Entities.RBAC.SysPermissionGroup))]
    public class PermissionGroup
    {
        public virtual int Id { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public virtual string Name { get; set; }

        public virtual int OrderNo { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        public virtual PermissionGroup Parent { get; set; }
        public virtual IList<PermissionGroup> Children { get; set; }
        public virtual IList<Module> Permissions { get; set; }

        [Ignore]
        public virtual bool? IsDisabled { get; set; }


        public PermissionGroup()
        {

        }

        public PermissionGroup(Db.Entities.RBAC.SysPermissionGroup source, bool isDisabled)
        {
            Id = (int)source.Id;
            Name = source.Name;
            OrderNo = source.OrderNo;
            Description = source.Description;
            Parent = AutoMapper.DtoConverter.Mapper.Map<PermissionGroup>(source.Parent);
            Children = AutoMapper.DtoConverter.Mapper.Map<IList<PermissionGroup>>(source.Children);
            Permissions = AutoMapper.DtoConverter.Mapper.Map<IList<Module>>(source.Permissions);
            this.IsDisabled = isDisabled;

        }

    }
}
