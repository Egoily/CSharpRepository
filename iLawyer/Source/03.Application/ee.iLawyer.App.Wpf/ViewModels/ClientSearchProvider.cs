using ee.Core.Caching;
using ee.Core.Framework;
using ee.Core.Wpf.ExControls;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO;
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

        private static readonly ILawyerServiceProvider ServiceProvider=new ILawyerServiceProvider();

        public SearchResult DoSearch(string searchTerm)
        {
            var results = new List<object>();
            var objs = !string.IsNullOrEmpty(searchTerm) ?
                 DataSource.Where(item => item.DisplayText.ToUpper().Contains(searchTerm.ToUpper()))
                 : DataSource.Take(3);
            objs?.ForEach(x => results.AddIfNotContains(x));
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
            objs?.ForEach(x => results.AddIfNotContains(x));
            return new SearchResult
            {
                SearchTerm = null,
                Results = results,
            };
        }

        public static ObservableCollection<MultiItemSelectorItemInfo> dataSource;
        public static ObservableCollection<MultiItemSelectorItemInfo> DataSource
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
                                var list = new ObservableCollection<MultiItemSelectorItemInfo>();
                                response.QueryList.ToList().ForEach(x => list.Add(new MultiItemSelectorItemInfo()
                                {
                                    Id = x.Id,
                                    DisplayText = x.Name,
                                    Description = x.Abbreviation + x.Impression,

                                }));
                                return list;
                            }
                            return new ObservableCollection<MultiItemSelectorItemInfo>();
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
                        var list = new ObservableCollection<MultiItemSelectorItemInfo>();
                        response.QueryList.ToList().ForEach(x => list.Add(new MultiItemSelectorItemInfo()
                        {
                            Id = x.Id,
                            DisplayText = x.Name,
                            Description = x.Abbreviation + x.Impression,
                        }));
                        return list;
                    }
                    return new ObservableCollection<MultiItemSelectorItemInfo>();
                },
                new TimeSpan(12, 0, 0),//过期时间
                null,
                true//立即更新
                );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
