using System.Collections.Generic;

namespace ee.Core.Wpf.ExControls
{
    public class SearchResult
    {
        public string SearchTerm { get; set; }
        public IList<object> Results { get; set; }
    }

    public interface ISearchDataProvider
    {
        SearchResult DoSearch(string searchTerm, string[] excludes);
        SearchResult SearchByKey(object Key);
    }
}
