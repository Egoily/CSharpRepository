using ee.iLawyer.Db.Entities.Law;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings.Law
{
    public class LawRevisionMap : ClassMap<LawRevision>
    {
        public LawRevisionMap()
        {
            Table("Law_Revisions");
            LazyLoad();
            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.Context);
            Map(x => x.CreatedDate);
            References(x => x.LegalProvision).Column("LawId").Cascade.SaveUpdate().NotFound.Ignore();
        }
    }
}
