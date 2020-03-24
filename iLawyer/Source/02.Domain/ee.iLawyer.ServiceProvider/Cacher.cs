using ee.Core.Framework;
using ee.Core.Framework.Caching;
using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static ee.iLawyer.Ops.ILawyerService;

namespace ee.iLawyer.ServiceProvider
{
    public static class Cacher
    {

        public static User Loginer { get; set; }

        public static ObservableCollection<string> CurrentResources
        {
            get
            {
                if (Loginer == null || Loginer.Resources == null)
                {
                    return null;
                }
                return new ObservableCollection<string>(Loginer?.Resources);
            }
        }


        /// <summary>
        /// 过期时间
        /// </summary>
        public static TimeSpan ExpiredTimeSpan = new TimeSpan(12, 0, 0);
        public static void Load()
        {
            var projectCauses = ProjectCauses;
            var projectCategories = ProjectCategories;
            var areas = Areas;
            var courts = Courts;
            var clients = Cliects;

        }




        #region Courts

        public static ObservableCollection<Court> courts;
        public static ObservableCollection<Court> Courts
        {
            get
            {
                if (courts == null || !courts.Any())
                {
                    try
                    {
                        courts = MemoryCache.CacheItem(CacheKeys.Courts,
                        delegate ()
                        {
                            var server = new ILawyerServiceProvider();
                            var response = server.QueryCourt(new QueryCourtRequest());
                            if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                            {
                                var list = new ObservableCollection<Court>();
                                response.QueryList.ToList().ForEach(x => list.Add(new Court()
                                {
                                    Id = x.Id,
                                    Name = x.Name,
                                    Rank = x.Rank,
                                    Province = x.Province,
                                    City = x.City,
                                    County = x.County,
                                    Address = x.Address,
                                    ContactNo = x.ContactNo,
                                }));
                                return list;
                            }
                            return new ObservableCollection<Court>();
                        }, ExpiredTimeSpan);
                    }
                    catch (Exception)
                    {
                    }
                }
                return courts;
            }
        }
        public static void UpdateCourts()
        {
            try
            {
                courts = MemoryCache.CacheItem(CacheKeys.Courts,
                delegate ()
                {
                    var server = new ILawyerServiceProvider();
                    var response = server.QueryCourt(new QueryCourtRequest());
                    if (response.Code == ErrorCodes.Ok && response.QueryList.Any())
                    {
                        var list = new ObservableCollection<Court>();
                        response.QueryList.ToList().ForEach(x => list.Add(new Court()
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Rank = x.Rank,
                            Province = x.Province,
                            City = x.City,
                            County = x.County,
                            Address = x.Address,
                            ContactNo = x.ContactNo,
                        }));
                        return list;
                    }
                    return new ObservableCollection<Court>();
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

        #endregion

        #region * Clients
        private static ObservableCollection<Client> cliects;
        public static ObservableCollection<Client> Cliects
        {
            get
            {
                if (cliects == null || !cliects.Any())
                {
                    try
                    {
                        cliects = MemoryCache.CacheItem(CacheKeys.Clients,
                        delegate ()
                        {
                            var server = new ILawyerServiceProvider();
                            var response = server.QueryClient(new Ops.Contact.Args.QueryClientRequest());
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
                return cliects;

            }
        }

        public static void UpdateClients()
        {
            try
            {
                cliects = MemoryCache.CacheItem(CacheKeys.Clients,
                delegate ()
                {
                    var server = new ILawyerServiceProvider();
                    var response = server.QueryClient(new Ops.Contact.Args.QueryClientRequest());
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
        #endregion


        #region * Pickers

        /// <summary>
        /// 省市
        /// </summary>
        public static ObservableCollection<Area> Areas
        {
            get
            {
                try
                {
                    var areas = MemoryCache.CacheItem(CacheKeys.AreaInfo,
                        delegate ()
                        {
                            var server = new ILawyerServiceProvider();
                            var response = server.GetAreas(new GetAreasRequest());
                            if (response.IsOk() && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            else
                            {
                                return new List<Area>();
                            }
                        }, ExpiredTimeSpan);
                    if (areas != null && areas.Any())
                    {
                        return new ObservableCollection<Area>(areas);
                    }
                    else
                    {
                        return new ObservableCollection<Area>();
                    }
                }
                catch (Exception)
                {
                    return new ObservableCollection<Area>();
                }
            }
        }
        public static ObservableCollection<Picker> ProjectCategories
        {
            get
            {
                try
                {
                    var pickers = MemoryCache.CacheItem(CacheKeys.ProjectCategories,
                        delegate ()
                        {
                            var server = new ILawyerServiceProvider();
                            var response = server.GetPickers(new GetPickersRequest() { Category = PickerCategory.ProjectCategory.ToString() });
                            if (response.IsOk() && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            else
                            {
                                return new List<Picker>();
                            }
                        }, ExpiredTimeSpan);

                    if (pickers != null && pickers.Any())
                    {
                        return new ObservableCollection<Picker>(pickers);
                    }
                    else
                    {
                        return new ObservableCollection<Picker>();
                    }

                }
                catch (Exception)
                {
                    return new ObservableCollection<Picker>();
                }
            }
        }

        public static ObservableCollection<Picker> ProjectCauses
        {
            get
            {
                try
                {
                    var pickers = MemoryCache.CacheItem(CacheKeys.ProjectCauses,
                        delegate ()
                        {
                            var server = new ILawyerServiceProvider();
                            var response = server.GetPickers(new GetPickersRequest() { Category = PickerCategory.ProjectCause.ToString() });
                            if (response.IsOk() && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            return new List<Picker>();
                        }, ExpiredTimeSpan);

                    if (pickers != null && pickers.Any())
                    {
                        return new ObservableCollection<Picker>(pickers);
                    }
                    else
                    {
                        return new ObservableCollection<Picker>();
                    }

                }
                catch (Exception)
                {
                    return new ObservableCollection<Picker>();
                }
            }
        }

        public static ObservableCollection<Picker> PropertyPickerItemsSource
        {
            get
            {
                try
                {
                    var pickers = MemoryCache.CacheItem<IList<Picker>>(CacheKeys.PropertyItemCategories,
                        delegate ()
                        {
                            var server = new ILawyerServiceProvider();
                            var response = server.GetPickers(new GetPickersRequest() { Category = PickerCategory.PropertyPick.ToString() });
                            if (response.IsOk() && (response.QueryList?.Any() ?? false))
                            {
                                return response.QueryList.ToList();
                            }
                            else
                            {
                                return new List<Picker>();
                            }
                        }, ExpiredTimeSpan);
                    if (pickers != null && pickers.Any())
                    {
                        return new ObservableCollection<Picker>(pickers);
                    }
                    else
                    {
                        return new ObservableCollection<Picker>();
                    }
                }
                catch (Exception)
                {
                    return new ObservableCollection<Picker>();
                }



            }
        }



        #endregion













    }
}
