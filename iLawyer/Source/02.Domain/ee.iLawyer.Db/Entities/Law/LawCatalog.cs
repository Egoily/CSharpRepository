using ee.Core.Data;
using System.Collections.Generic;

namespace ee.iLawyer.Db.Entities.Law
{
    public class LawCatalog : RecursionEntity<LawCatalog, int?>
    {
        public virtual string Text { get; set; }


        public virtual IList<LegalProvisionDetail> LegalProvisionDetails { get; set; }
    }
}
