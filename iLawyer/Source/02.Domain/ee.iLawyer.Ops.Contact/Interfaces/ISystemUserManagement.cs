using ee.Core.Framework.Schema;
using ee.Core.Net;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface ISystemUserManagement
    {


        QueryResponse<PermissionModule> GetPermissionModules(RequestBase request);
        QueryResponse<Role> GetRoles(GetRolesRequest request);
        QueryResponse<User> QueryUser(QueryUserRequest request);


        ResponseBase Register(RegisterRequest request);
        ObjectResponse<User> Login(LoginRequest request);
        ResponseBase Logout(LogoutRequest request);

        ResponseBase Grant(GrantRequest request);

        ResponseBase UpdateUser(UpdateUserRequest request);
        ResponseBase ChangePassword(ChangePasswordRequest request);
    }
}
