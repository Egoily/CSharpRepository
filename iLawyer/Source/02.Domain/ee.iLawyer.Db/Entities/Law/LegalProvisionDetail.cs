using ee.Core.Data;

namespace ee.iLawyer.Db.Entities.Law
{
    public class LegalProvisionDetail : DbEntity
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Text { get; set; }
        public virtual LegalProvision LegalProvision { get; set; }
        public virtual LawCatalog Catalog { get; set; }
    }
}
