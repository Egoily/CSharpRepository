using AutoMapper;
using ee.iLawyer.Db.Entities.RBAC;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement;
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

    public class UserPermissionsValueResolver : IValueResolver<Db.Entities.RBAC.SysUser, DTO.ViewObjects.SystemManagement.User, IList<PermissionModule>>
    {
        public IList<PermissionModule> Resolve(SysUser source, User destination, IList<PermissionModule> destMember, ResolutionContext context)
        {
            var result = new List<PermissionModule>();
            var permissions = source?.PermissionModules.Union(source?.Roles?.SelectMany(x => x.PermissionModules));

            var permissionRestrictions = source?.Restrictions;

            permissions?.Distinct()?.ToList().ForEach(x => result.Add(new PermissionModule(x, true)));
            permissionRestrictions?.Distinct()?.ToList().ForEach(x => result.Add(new PermissionModule(x, false)));


            return result;
        }
    }

    public class PickerIdValueResolver : IValueResolver<Db.Entities.ClientProperty, DTO.ViewObjects.ClientProperty, int>
    {
        public int Resolve(Db.Entities.ClientProperty source, DTO.ViewObjects.ClientProperty destination, int destMember, ResolutionContext context)
        {
            return source?.Picker?.Id ?? 0;
        }
    }
    public class PickerNameValueResolver : IValueResolver<Db.Entities.ClientProperty, DTO.ViewObjects.ClientProperty, string>
    {
        public string Resolve(Db.Entities.ClientProperty source, DTO.ViewObjects.ClientProperty destination, string destMember, ResolutionContext context)
        {
            return source?.Picker?.Name ?? "";
        }
    }



}
