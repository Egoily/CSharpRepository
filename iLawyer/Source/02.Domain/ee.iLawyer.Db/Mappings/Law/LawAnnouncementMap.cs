using ee.iLawyer.Db.Entities.Law;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.Law
{
    public class LawAnnouncementMap : ClassMap<LawAnnouncement>
    {
        public LawAnnouncementMap()
        {
            Table("Law_Announcements");
            LazyLoad();
            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.Title);
            Map(x => x.Subtitle);
            Map(x => x.MainBody);
            Map(x => x.Signature);
            Map(x => x.SignDate);
            References(x => x.LegalProvision).Column("LawId").Cascade.SaveUpdate().NotFound.Ignore();
        }
    }
}
