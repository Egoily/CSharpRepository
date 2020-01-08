using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.Core.Framework.DataProvider
{
    public interface ISearchDataProvider
    {
        SearchResult DoSearch(string searchTerm, string[] excludes);
        SearchResult SearchByKey(object Key);
    }
}
