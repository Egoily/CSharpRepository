﻿using ee.Core.Data;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.RBAC
{
    public class SysRole : RecursionEntity<SysRole, int?>
    {
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        ///  名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        public virtual IList<SysPermissionModule> PermissionModules { get; set; }

        public virtual IList<SysUser> Users { get; set; }

    }
}
