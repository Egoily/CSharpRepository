using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface ISystemUserManagement
    {


        BaseQueryResponse<PermissionModule> GetPermissionModules(BaseRequest request);
        BaseQueryResponse<Role> GetRoles(GetRolesRequest request);
        BaseQueryResponse<User> QueryUser(QueryUserRequest request);


        BaseResponse Register(RegisterRequest request);
        BaseObjectResponse<User> Login(LoginRequest request);
        BaseResponse Logout(LogoutRequest request);

        BaseResponse Grant(GrantRequest request);

        BaseResponse UpdateUser(UpdateUserRequest request);
        BaseResponse ChangePassword(ChangePasswordRequest request);
    }
}
