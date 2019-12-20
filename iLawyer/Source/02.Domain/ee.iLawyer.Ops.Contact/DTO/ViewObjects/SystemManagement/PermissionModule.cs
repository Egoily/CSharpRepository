using AutoMapper;
using AutoMapper.Configuration.Annotations;

namespace ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement
{
    [AutoMap(typeof(Db.Entities.RBAC.SysPermissionModule))]
    public class PermissionModule
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual string Id { get; set; }

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


        [Ignore]
        public virtual bool? IsDisabled { get; set; }


        public PermissionModule()
        {

        }

        public PermissionModule(Db.Entities.RBAC.SysPermissionModule source, bool isDisabled)
        {
            Id = source.Id;
            Name = source.Name;
            OrderNo = source.OrderNo;
            IsModule = source.IsModule;
            IsNode = source.IsNode;
            Description = source.Description;
            IsDisabled = isDisabled;

        }

    }
}
