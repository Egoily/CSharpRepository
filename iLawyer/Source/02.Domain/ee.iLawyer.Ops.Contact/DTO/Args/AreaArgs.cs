﻿using ee.Core.Framework.Schema;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class GetAreasRequest : PageRequest
    {
        public virtual string[] Keys { get; set; }
        public virtual string Name { get; set; }
    }

}
