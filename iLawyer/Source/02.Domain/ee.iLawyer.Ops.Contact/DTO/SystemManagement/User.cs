using AutoMapper;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ee.iLawyer.Ops.Contact.DTO.SystemManagement
{
    [AutoMap(typeof(Db.Entities.RBAC.SysUser))]
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNo { get; set; }
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
        /// 是否是超级管理员
        /// </summary>
        public virtual bool IsAdmin { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否需要重置密码
        /// </summary>
        public virtual bool NeedResetPwd { get; set; }
        /// <summary>
        /// 所属角色
        /// </summary>
        public virtual IList<Role> Roles { get; set; }


        /// <summary>
        /// 拥有或限制的私人权限(限制的权限优先级高于拥有的相同权限)
        /// </summary>
        [ValueResolver(typeof(AutoMapper.UserPermissionsValueResolver))]
        public virtual IList<PermissionModule> PermissionModules { get; set; }



        /// <summary>
        /// 资源,用于控制权限
        /// </summary>
        public virtual IList<string> Resources
        {
            get
            {
                var resources = PermissionModules?.Where(x => x.IsDisabled == true)?.Select(x => x.Id);
                return resources?.Distinct()?.ToList();
            }
        }

    }
}
