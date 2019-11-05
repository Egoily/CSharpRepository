using System.Collections.Generic;

namespace ee.Core.Data
{
    public class RecursionEntity<T, TKeyType> : DbEntity
    {
        public virtual TKeyType Id { get; set; }
        public virtual TKeyType ParentId { get; set; }
        /// <summary>
        /// Parent
        /// </summary>
        public virtual T Parent { get; set; }
        /// <summary>
        /// Children
        /// </summary>
        public virtual IList<T> Children { get; set; }
    }
}
