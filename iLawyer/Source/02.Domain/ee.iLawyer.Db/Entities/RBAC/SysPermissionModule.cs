using ee.Core.Data;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.RBAC
{
    public class SysPermissionModule : RecursionEntity<SysPermissionModule, string>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public virtual int OrderNo { get; set; }
        /// <summary>
        /// 是否是模块
        /// </summary>
        public virtual bool IsModule { get; set; }
        /// <summary>
        /// 是否是节点
        /// </summary>
        public virtual bool IsNode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

      

        public virtual IList<SysRole> Roles { get; set; }
        public virtual IList<SysUser> Users { get; set; }
    }
}
