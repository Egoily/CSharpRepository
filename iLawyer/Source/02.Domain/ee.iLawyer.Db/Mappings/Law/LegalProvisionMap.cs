using ee.iLawyer.Db.Entities.Law;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings.Law
{
    public class LegalProvisionMap : ClassMap<LegalProvision>
    {
        public LegalProvisionMap()
        {
            Table("Law_LegalProvisions");
            LazyLoad();
            Id(x => x.Id);
            //.GeneratedBy.Assigned();  
            Map(x => x.Title);
            Map(x => x.Subtitle);
            Map(x => x.Publisher);
            Map(x => x.PublishNumber);
            Map(x => x.PublishDate);
            Map(x => x.EffectiveDate);
            Map(x => x.ExpiryDate);
            Map(x => x.KeyWords);

            References(x => x.Category).Column("CategoryId").Cascade.SaveUpdate().NotFound.Ignore();
            HasMany(x => x.LegalProvisionDetails).Cascade.All().Inverse();
            HasMany(x => x.Revisions).Cascade.All().Inverse();
            HasMany(x => x.Announcements).Cascade.All().Inverse();
        }
    }
}
