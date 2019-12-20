using ee.Core.Utility;
using System;

namespace ee.Core.Data
{
    public class CloneableObject : ICloneable
    {
        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public virtual bool AreEqual(object obj, params string[] ignore)
        {
            return Extessions.PropertiesAreEqual(this,obj, ignore);
        }
    }
}
