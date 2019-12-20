using ee.Core.Caching;
using ee.Core.Framework;
using ee.Core.Wpf.ExControls;
using ee.iLawyer.App.Wpf.UserControls.Pickers;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ee.iLawyer.App.Wpf.ViewModels
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

        private static readonly ILawyerServiceProvider ServiceProvider = new ILawyerServiceProvider();

        public SearchResult DoSearch(string searchTerm, string[] excludes)
        {
            var results = new List<object>();
            if (excludes != null && excludes.Any())
            {
                var filter = DataSource.Where(x => !excludes.Contains(x.Name));
                var objs = !string.IsNullOrEmpty(searchTerm) ?
                    filter.Where(item => item.Name.ToUpper().Contains(searchTerm.ToUpper()))
                    : filter.Take(3);
                objs?.ForEach(x => results.AddIfNotContains(new MultiItemSelectorItem() { Id = x.Id, DisplayText = x.Name, Description = x.Impression }));
            }
            else
            {
                var objs = !string.IsNullOrEmpty(searchTerm) ?
                     DataSource.Where(item => item.Name.ToUpper().Contains(searchTerm.ToUpper()))
                     : DataSource.Take(3);
                objs?.ForEach(x => results.AddIfNotContains(new MultiItemSelectorItem() { Id = x.Id, DisplayText = x.Name, Description = x.Impression }));
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
            var objs = DataSource.Where(item => item.Id.ToString() == Key.ToString());
            objs?.ForEach(x => results.AddIfNotContains(new MultiItemSelectorItem() { Id = x.Id, DisplayText = x.Name, Description = x.Impression }));
            return new SearchResult
            {
                SearchTerm = null,
                Results = results,
            };
        }

        public static ObservableCollection<Client> dataSource;
        public static ObservableCollection<Client> DataSource
        {
            get
            {
                if (dataSource == null || !dataSource.Any())
                {
                    try
                    {
                        dataSource = MemoryCacher.CacheItem(CacheKeys.Clients,
                        delegate ()
                        {

                            var response = ServiceProvider.QueryClient(new Ops.Contact.Args.QueryClientRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                            {
                                return new ObservableCollection<Client>(response.QueryList.ToList());
                            }
                            return new ObservableCollection<Client>();
                        },
                        new TimeSpan(12, 0, 0));//过期时间
                    }
                    catch (Exception)
                    {
                    }
                }
                return dataSource;

            }
        }

        public static void UpdateClients()
        {
            try
            {
                dataSource = MemoryCacher.CacheItem(CacheKeys.Clients,
                delegate ()
                {

                    var response = ServiceProvider.QueryClient(new Ops.Contact.Args.QueryClientRequest());
                    if (response.Code == ErrorCodes.Ok && response.QueryList.Any())
                    {
                        return new ObservableCollection<Client>(response.QueryList.ToList());
                    }
                    return new ObservableCollection<Client>();
                },
                new TimeSpan(12, 0, 0),//过期时间
                null,
                true//立即更新
                );

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}
