using ee.iLawyer.Db.Entities.Law;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings.Law
{
    public class LawCatalogMap : ClassMap<LawCatalog>
    {
        public LawCatalogMap()
        {
            Table("Law_Catalogs");
            LazyLoad();
            Id(x => x.Id);

            Map(x => x.Text);
            Map(x => x.ParentId);
            HasMany(x => x.LegalProvisionDetails).Cascade.All().Inverse();
        }
    }
}
