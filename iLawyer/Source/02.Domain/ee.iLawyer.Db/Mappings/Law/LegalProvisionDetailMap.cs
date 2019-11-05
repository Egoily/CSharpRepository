using ee.iLawyer.Db.Entities.Law;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings.Law
{
    public class LegalProvisionDetailMap : ClassMap<LegalProvisionDetail>
    {
        public LegalProvisionDetailMap()
        {
            Table("Law_LegalProvisionDetails");
            LazyLoad();
            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.Title);
            Map(x => x.Text);
            References(x => x.LegalProvision).Column("LawId").Cascade.SaveUpdate().NotFound.Ignore();
            References(x => x.Catalog).Column("CatalogId").Cascade.SaveUpdate().NotFound.Ignore();
        }
    }
}
