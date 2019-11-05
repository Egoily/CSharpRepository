using ee.Core.Data;
using System;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.Law
{
    public class LegalProvision : DbEntity
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Subtitle { get; set; }
        public virtual string Publisher { get; set; }
        public virtual string PublishNumber { get; set; }
        public virtual DateTime? PublishDate { get; set; }
        public virtual DateTime? EffectiveDate { get; set; }
        public virtual DateTime? ExpiryDate { get; set; }
        public virtual string KeyWords { get; set; }


        public virtual LawCategory Category { get; set; }
        public virtual IList<LegalProvisionDetail> LegalProvisionDetails { get; set; }

        public virtual IList<LawRevision> Revisions { get; set; }
        public virtual IList<LawAnnouncement> Announcements { get; set; }
    }
}
