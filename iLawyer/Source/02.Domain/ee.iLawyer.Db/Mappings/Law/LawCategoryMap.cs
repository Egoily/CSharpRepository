using ee.iLawyer.Db.Entities.Law;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings.Law
{
    public class LawCategoryMap : ClassMap<LawCategory>
    {
        public LawCategoryMap()
        {
            Table("Law_Categories");
            LazyLoad();
            Id(x => x.Id);

            Map(x => x.Code);
            Map(x => x.Name);
            Map(x => x.Enabled);
            Map(x => x.ParentId);
        }
    }
}
