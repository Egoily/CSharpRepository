using ee.Core.Data;
using System;

namespace ee.iLawyer.Db.Entities.Law
{
    public class LawAnnouncement : DbEntity
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string MainBody { get; set; }
        public virtual string Signature { get; set; }
        public virtual DateTime SignDate { get; set; }

        public virtual LegalProvision LegalProvision { get; set; }
    }
}
