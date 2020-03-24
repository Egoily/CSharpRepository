using ee.Core.Framework.Schema;
using ee.Core.Net;

namespace ee.iLawyer.Ops.Contact.Args
{


    public class GetPickersRequest : RequestBase
    {
        public virtual string Category { get; set; }
    }

}
