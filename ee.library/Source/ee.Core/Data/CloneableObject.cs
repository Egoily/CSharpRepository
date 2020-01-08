using ee.Core.DeepCopy;
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
        public virtual object DeepClone()
        {
            return this.DeepCopy();
        }

        public virtual bool AreEqual(object obj, params string[] ignore)
        {
            return AssertExtessions.PropertiesAreEqual(this, obj, ignore);
        }
    }
}
