using ee.Core.Data;
using System;

namespace ee.iLawyer.Db.Entities.Law
{
    public class LawRevision : DbEntity
    {
        public virtual int Id { get; set; }
        public virtual string Context { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual LegalProvision LegalProvision { get; set; }
    }
}
