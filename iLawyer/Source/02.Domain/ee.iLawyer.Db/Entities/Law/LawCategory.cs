using ee.Core.Data;

namespace ee.iLawyer.Db.Entities.Law
{
    public class LawCategory : RecursionEntity<LawCategory, int?>
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Enabled { get; set; }

    }
}
