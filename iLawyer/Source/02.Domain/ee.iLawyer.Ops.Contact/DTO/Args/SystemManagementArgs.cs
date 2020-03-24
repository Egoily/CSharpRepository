using ee.Core.Framework.Schema;
using ee.Core.Net;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.Args
{

    public class GetRolesRequest : RequestBase
    {
        public virtual bool WithPermissionModule { get; set; }
    }
    public class QueryUserRequest : RequestBase
    {
        public virtual string UserName { get; set; }
        public virtual IEnumerable<int> RoleIds { get; set; }
    }

    public class RegisterRequest : RequestBase
    {
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }

        public virtual string PhoneNo { get; set; }
        public virtual IEnumerable<int> RoleIds { get; set; }
    }

    public class LoginRequest : RequestBase
    {
        public virtual string LoginName { get; set; }
        public virtual string Password { get; set; }
    }

    public class LogoutRequest : RequestBase
    {
        public virtual int UserId { get; set; }
    }
    public class GrantRequest : RequestBase
    {
        public virtual OperatePattern Pattern { get; set; }
        public virtual int UserId { get; set; }
        public virtual IEnumerable<int> RoleIds { get; set; }
        public virtual IEnumerable<string> PermissionIds { get; set; }
        public virtual IEnumerable<string> PermissionRestrictionIds { get; set; }
    }

    public class UpdateUserRequest : RequestBase
    {
        public virtual int UserId { get; set; }
        public virtual string NickName { get; set; }
        public virtual int? Status { get; set; }
    }
    public class ChangePasswordRequest : RequestBase
    {
        public virtual int UserId { get; set; }
        public virtual string OldPassword { get; set; }
        public virtual string NewPassword { get; set; }
    }

}
