using ee.Core.Framework.DataProvider;
using ee.iLawyer.App.Wpf.UserControls.Pickers;
using ee.iLawyer.ServiceProvider;
using System.Collections.Generic;
using System.Linq;

namespace ee.iLawyer.App.Wpf.DataProviders
{
    public class ClientSearchProvider : ISearchDataProvider
    {

        private static object Locker = new object();


        private static ClientSearchProvider instance = null;


        public static ClientSearchProvider Instance
        {
            get
            {
                lock (Locker)
                {
                    return (instance ?? (instance = new ClientSearchProvider()));
                }
            }

        }

        public SearchResult DoSearch(string searchTerm, string[] excludes)
        {
            var results = new List<object>();
            if (excludes != null && excludes.Any())
            {
                var filter = Cacher.Cliects.Where(x => !excludes.Contains(x.Name));
                var objs = !string.IsNullOrEmpty(searchTerm) ?
                    filter.Where(item => item.Name.ToUpper().Contains(searchTerm.ToUpper()))
                    : filter.Take(5);
                objs?.ForEach(x => results.AddIfNotContains(new SelectorItem() { Id = x.Id, DisplayText = x.Name, Description = x.Impression }));
            }
            else
            {
                var objs = !string.IsNullOrEmpty(searchTerm) ?
                     Cacher.Cliects.Where(item => item.Name.ToUpper().Contains(searchTerm.ToUpper()))
                     : Cacher.Cliects.Take(5);
                objs?.ForEach(x => results.AddIfNotContains(new SelectorItem() { Id = x.Id, DisplayText = x.Name, Description = x.Impression }));
            }
            return new SearchResult
            {
                SearchTerm = searchTerm,
                Results = results,
            };
        }

        public SearchResult SearchByKey(object Key)
        {
            var results = new List<object>();
            var objs = Cacher.Cliects.Where(item => item.Id.ToString() == Key.ToString());
            objs?.ForEach(x => results.AddIfNotContains(new SelectorItem() { Id = x.Id, DisplayText = x.Name, Description = x.Impression }));
            return new SearchResult
            {
                SearchTerm = null,
                Results = results,
            };
        }


    }
}
