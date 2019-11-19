using System;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.RBAC
{
    /// <summary>
    /// 系统用户信息
    /// </summary>
    public class SysUser
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// Code
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Nickname { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime? LastLoginTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool NeedResetPwd { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsAdmin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IList<SysRole> Roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IList<SysPermissionModule> PermissionModules { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IList<SysPermissionModule> Restrictions { get; set; }

    }
}
