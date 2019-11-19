using ee.iLawyer.Db.Entities.RBAC;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.RBAC
{
    public class SysPermissionModuleMap : ClassMap<SysPermissionModule>
    {
        public SysPermissionModuleMap()
        {
            Table("Sys_PermissionModules");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.OrderNo);
            Map(x => x.IsModule);
            Map(x => x.IsNode);
            Map(x => x.Description);
            Map(x => x.ParentId).Column("ParentId");

            HasManyToMany(x => x.Roles).LazyLoad().ParentKeyColumn("PmId").ChildKeyColumn("RoleId").Table("Sys_RolePermission").NotFound.Ignore();
            HasManyToMany(x => x.Users).LazyLoad().ParentKeyColumn("PmId").ChildKeyColumn("UserId").Table("Sys_UserPermission").NotFound.Ignore();


        }
    }
}
