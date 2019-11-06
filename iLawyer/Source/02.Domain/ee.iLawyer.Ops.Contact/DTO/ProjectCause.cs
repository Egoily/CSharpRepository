using AutoMapper;

namespace ee.iLawyer.Ops.Contact.DTO
{
    /// <summary>
    /// 案由
    /// </summary>
    [AutoMap(typeof(Db.Entities.Foundation.Picklist))]
    public class ProjectCause
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual int OrderNo { get; set; }

    }
}
