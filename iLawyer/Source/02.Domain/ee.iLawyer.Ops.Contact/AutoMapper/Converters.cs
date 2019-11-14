using AutoMapper;
using ee.iLawyer.Db.Entities;
using ee.iLawyer.Db.Entities.RBAC;
using ee.iLawyer.Ops.Contact.DTO.SystemManagement;
using System.Collections.Generic;
using System.Linq;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class GenderTypeConverter : ITypeConverter<int, string>
    {
        public string Convert(int source, string destination, ResolutionContext context)
        {
            switch (source)
            {
                case 1:
                    destination = "男";
                    break;
                case 2:
                    destination = "女";
                    break;
                default:
                    destination = "未知";
                    break;
            }
            return destination;
        }
    }

    public class ClientPropertyItemTypeConverter : ITypeConverter<IList<ClientProperties>, List<DTO.CategoryValue>>
    {
        public List<DTO.CategoryValue> Convert(IList<ClientProperties> source, List<DTO.CategoryValue> destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new List<DTO.CategoryValue>();
            }

            if (source != null)
            {
                foreach (var item in source)
                {
                    if (item.Picker != null)
                    {
                        destination.Add(new DTO.CategoryValue(item.Picker.Id ?? 0, item.Picker.Name, item.Value, item.Id));
                    }
                }
            }
            return destination;
        }
    }



    public class UserResourcesValueResolver : IValueResolver<Db.Entities.RBAC.SysUser, DTO.SystemManagement.User, IList<string>>
    {
        public IList<string> Resolve(SysUser source, User destination, IList<string> destMember, ResolutionContext context)
        {
            var resources = (from c in source.Permissions select (c.Code))
                .Union(from c in source.Roles.SelectMany(x => x.Permissions) select (c.Code))
                .Except(from c in source.PermissionRestrictions select (c.Code));
            return resources?.Distinct()?.ToList();
        }
    }
    public class PermissionGroupsValueResolver : IValueResolver<Db.Entities.RBAC.SysUser, DTO.SystemManagement.User, IList<string>>
    {
        public IList<string> Resolve(SysUser source, User destination, IList<string> destMember, ResolutionContext context)
        {
            var permissionGroups = source.PermissionGroups.Select(x => x.Name);
            return permissionGroups?.Distinct()?.ToList();
        }
    }







    public class UserPermissionsValueResolver : IValueResolver<Db.Entities.RBAC.SysUser, DTO.SystemManagement.User, IList<Module>>
    {
        public IList<Module> Resolve(SysUser source, User destination, IList<Module> destMember, ResolutionContext context)
        {
            var result = new List<Module>();
            var permissions = source?.Permissions.Union(source?.Roles?.SelectMany(x => x.Permissions));

            var permissionRestrictions = source?.PermissionRestrictions;

            permissions?.Distinct()?.ToList().ForEach(x => result.Add(new Module(x, true)));
            permissionRestrictions?.Distinct()?.ToList().ForEach(x => result.Add(new Module(x, false)));


            return result;
        }
    }

    public class UserPermissionGroupsValueResolver : IValueResolver<Db.Entities.RBAC.SysUser, DTO.SystemManagement.User, IList<PermissionGroup>>
    {
        public IList<PermissionGroup> Resolve(SysUser source, User destination, IList<PermissionGroup> destMember, ResolutionContext context)
        {
            var result = new List<PermissionGroup>();
            var permissionGroups = source?.PermissionGroups.Union(source?.Roles?.SelectMany(x => x.PermissionGroup)); ;

            var permissionGroupRestrictions = source?.PermissionGroupRestrictions;

            permissionGroups?.Distinct()?.ToList().ForEach(x => result.Add(new PermissionGroup(x, true)));
            permissionGroupRestrictions?.Distinct()?.ToList().ForEach(x => result.Add(new PermissionGroup(x, false)));


            return result;
        }
    }







}
