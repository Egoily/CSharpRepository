using System.Collections.Generic;

namespace ee.Core.Framework.DataProvider
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public IList<object> Results { get; set; }
    }

  
}
