using ee.Core.Data;
using ee.Core.NhDataAccess;
using ee.Core.NhDataAccess.Repository;
using ee.Core.Exceptions;
using ee.Core.Framework;
using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.Ops.Contact.Interfaces;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ee.Core.Net;

namespace ee.iLawyer.Ops
{
    public class ILawyerService : IILawyerService, IFoundation, ISystemUserManagement
    {
        public enum PickerCategory
        {
            ProjectCategory,
            ProjectCause,
            PropertyPick,
        }
        private static bool IsInit = false;
        public static void Build()
        {
            if (IsInit)
            {
                return;
            }

            // create our  session factory
            //SessionManager.Builder = new SqliteSessionFactoryBuilder();
            //SessionManager.Builder.Build(@"E:\EgoGit\iLawyer\03.Application\ee.iLawyer.WebApi\bin\database.db");
            SessionManager.Builder = new DataAccessBuilder.SqlServer.DataAccessBuilder();

            IsInit = true;

        }
        public ILawyerService()
        {
            Build();

        }


        public ResponseBase Test(RequestBase request)
        {
            return ServiceProcessor.CreateProcessor<RequestBase, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                .Process(req =>
                {
                    var response = new ResponseBase();

                    //System.Threading.Tasks.Parallel.For(1, 100, index =>
                    //{
                    //    var repo = new NhEntityRepository<Db.Entities.ProjectCause>();

                    //    System.Threading.Thread.Sleep(10);

                    //});
                    for (int i = 0; i < 100; i++)
                    {
                        var repo = new NhEntityRepository<Db.Entities.Foundation.Picklist>();
                        System.Threading.Thread.Sleep(10);
                    }
                    return response;
                }
                ).Build();
        }




        public QueryResponse<Contact.DTO.ViewObjects.Area> GetAreas(GetAreasRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetAreasRequest, QueryResponse<Contact.DTO.ViewObjects.Area>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new QueryResponse<Contact.DTO.ViewObjects.Area>();

                       var queryCriterions = new List<ICriterion>();

                       using (var repo = new NhEntityRepository<Db.Entities.Foundation.Area>())
                       {
                           if (req.Keys != null && req.Keys.Any())
                           {
                               queryCriterions.Add(Restrictions.On<Db.Entities.Foundation.Area>(y => y.Id).IsIn(req.Keys));
                           }
                           else
                           {
                               if (!string.IsNullOrEmpty(req.Name))
                               {
                                   queryCriterions.Add(Restrictions.On<Db.Entities.Foundation.Area>(y => y.Name).IsLike(req.Name, MatchMode.Anywhere));
                               }
                               else
                               {
                                   //queryCriterions.Add(Restrictions.Where<Db.Entities.Area>(x => x.Parent == null));
                               }

                           }
                           var query = repo.QueryByPageInCriterion(queryCriterions, x => x.Id, false, req.PageIndex, req.PageSize, PageQueryOption.Both, true);

                           if (query.Item1.Any())
                           {
                               var tree = RecursionUtility.GetTree<Db.Entities.Foundation.Area, string>(query.Item1);


                               response.QueryList = tree.Select(DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Area>).ToList();
                           }
                       }
                       return response;
                   }
                  ).Build();
        }

        public IEnumerable<Db.Entities.Foundation.Picklist> GetAllPicklist()
        {
            using (var repo = new NhEntityRepository<Db.Entities.Foundation.Picklist>())
            {
                var queryCriterions = new List<ICriterion>();
                queryCriterions.Add(Restrictions.Where<Db.Entities.Foundation.Picklist>(x => x.Enabled));
                var query = repo.QueryByPageInCriterion(queryCriterions, x => x.OrderNo, false, 0, 0, PageQueryOption.Content, true);

                return query.Item1;
            }


        }


        public QueryResponse<Contact.DTO.ViewObjects.Picker> GetPickers(GetPickersRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetPickersRequest, QueryResponse<Contact.DTO.ViewObjects.Picker>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                  {
                      var response = new QueryResponse<Contact.DTO.ViewObjects.Picker>();
                      using (var repo = new NhEntityRepository<Db.Entities.Foundation.Picklist>())
                      {
                          (IEnumerable<Db.Entities.Foundation.Picklist>, int?) query;
                          if (!string.IsNullOrEmpty(req.Category))
                          {
                              query = repo.QueryByPage(x => x.Enabled && x.Category == req.Category, x => x.Id, false, 0, 0, PageQueryOption.Content, true);
                          }
                          else
                          {
                              query = repo.QueryByPage(x => x.Enabled, x => x.Id, false, 0, 0, PageQueryOption.Content, true);
                          }
                          if (query.Item1?.Any() ?? false)
                          {
                              var tree = RecursionUtility.GetTree<Db.Entities.Foundation.Picklist, int?>(query.Item1);
                              response.QueryList = tree?.ToList()?.Select(DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Picker>).ToList();
                              response.Total = query.Item2;
                          }

                      }
                      return response;
                  }
                  ).Build();

        }




        #region * ISystemUserManagement

        public QueryResponse<Contact.DTO.ViewObjects.SystemManagement.PermissionModule> GetPermissionModules(RequestBase request)
        {
            throw new NotImplementedException();
        }

        public QueryResponse<Contact.DTO.ViewObjects.SystemManagement.Role> GetRoles(GetRolesRequest request)
        {
            throw new NotImplementedException();
        }

        public QueryResponse<Contact.DTO.ViewObjects.SystemManagement.User> QueryUser(QueryUserRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase Register(RegisterRequest request)
        {
            return ServiceProcessor.CreateProcessor<RegisterRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                 .Process(req =>
                 {
                     var response = new ResponseBase();
                     using (var repo = new NhGlobalRepository())
                     {
                         var users = repo.Query<Db.Entities.RBAC.SysUser>(x => x.UserName == req.UserName);
                         if (users != null && users.Any())
                         {
                             throw new EeException(ErrorCodes.ExistedUser, "User is existed.");
                         }
                         var queryCriterions = new List<ICriterion>
                         {
                             Restrictions.On<Db.Entities.RBAC.SysRole>(y => y.Id).IsIn(req.RoleIds.ToArray())
                         };
                         var roles = repo.Query<Db.Entities.RBAC.SysRole>(queryCriterions);


                         var user = new Db.Entities.RBAC.SysUser()
                         {
                             Roles = roles.ToList(),
                             UserName = req.UserName,
                             Password = req.Password,
                             Code = req.PhoneNo,
                             Status = 1,
                             CreateTime = DateTime.Now,
                         };
                         repo.Create(user);
                     }
                     return response;
                 }
                 ).Build();
        }

        public ObjectResponse<Contact.DTO.ViewObjects.SystemManagement.User> Login(LoginRequest request)
        {
            return ServiceProcessor.CreateProcessor<LoginRequest, ObjectResponse<Contact.DTO.ViewObjects.SystemManagement.User>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                  {
                      var response = new ObjectResponse<Contact.DTO.ViewObjects.SystemManagement.User>();
                      using (var repo = new NhGlobalRepository())
                      {

                          var users = repo.Query<Db.Entities.RBAC.SysUser>(x => x.UserName == req.LoginName);
                          if (users == null || !users.Any())
                          {
                              throw new EeException(ErrorCodes.NotExistedUser, "User is not existed.");
                          }
                          var user = users.FirstOrDefault(x => x.Password == req.Password);
                          if (user == null)
                          {
                              throw new EeException(ErrorCodes.IllegalCertificate, "The password is incorrect.");
                          }
                          if (user.Status != 1)
                          {
                              throw new EeException(ErrorCodes.IllegalStatus, "The status of user is illegal.");
                          }
                          //DON't do that because that code will insert rows into database.
                          //if (user.IsAdmin)
                          //{
                          //    user.Permissions = repo.Query<Db.Entities.RBAC.SysModule>()?.ToList();
                          //}
                          response.Object = DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.SystemManagement.User>(user);



                          user.LastLoginTime = DateTime.Now;
                          repo.Update(user);
                      }
                      return response;
                  }
                  ).Build();
        }

        public ResponseBase Logout(LogoutRequest request)
        {
            throw new NotImplementedException();
        }


        public ResponseBase Grant(GrantRequest request)
        {
            return ServiceProcessor.CreateProcessor<GrantRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                .Inbound(() =>
                {
                    if (request.RoleIds == null)
                    {
                        request.RoleIds = new List<int>();
                    }
                    if (request.PermissionIds == null)
                    {
                        request.PermissionIds = new List<string>();
                    }

                    if (request.PermissionRestrictionIds == null)
                    {
                        request.PermissionRestrictionIds = new List<string>();
                    }
                })
                 .Process(req =>
                 {
                     var response = new ResponseBase();
                     using (var repo = new NhGlobalRepository())
                     {

                         var user = repo.GetById<Db.Entities.RBAC.SysUser>(req.UserId);
                         if (user == null)
                         {
                             throw new EeException(ErrorCodes.NotExistedUser, "User is not existed.");
                         }
                         var addedRoleIds = new List<int>();
                         var removedRoleIds = new List<int>();
                         var addedPermissionIds = new List<string>();
                         var removedPermissionIds = new List<string>();

                         var addedPermissionRestrictionIds = new List<string>();
                         var removedPermissionRestrictionIds = new List<string>();

                         switch (req.Pattern)
                         {
                             case Contact.OperatePattern.Hybrid:
                                 removedRoleIds = user.Roles.Select(x => x.Id.Value).Except(req.RoleIds).ToList();
                                 addedRoleIds = req.RoleIds.Except(user.Roles.Select(x => x.Id.Value)).ToList();
                                 removedPermissionIds = user.PermissionModules.Select(x => x.Id).Except(req.PermissionIds).ToList();
                                 addedPermissionIds = req.PermissionIds.Except(user.PermissionModules.Select(x => x.Id)).ToList();

                                 removedPermissionRestrictionIds = user.Restrictions.Select(x => x.Id).Except(req.PermissionRestrictionIds).ToList();
                                 addedPermissionRestrictionIds = req.PermissionRestrictionIds.Except(user.Restrictions.Select(x => x.Id)).ToList();
                                 break;
                             case Contact.OperatePattern.Increase:
                                 addedRoleIds = req.RoleIds.ToList();
                                 addedPermissionIds = req.PermissionIds.ToList();

                                 addedPermissionRestrictionIds = req.PermissionRestrictionIds.ToList();
                                 break;
                             case Contact.OperatePattern.Decrease:
                                 removedRoleIds = req.RoleIds.ToList();
                                 removedPermissionIds = req.PermissionIds.ToList();

                                 removedPermissionRestrictionIds = req.PermissionRestrictionIds.ToList();
                                 break;
                             default:
                                 break;
                         }
                         #region *Role

                         if (removedRoleIds.Any())
                         {
                             user.Roles.RemoveWhere(x => removedRoleIds.Contains(x.Id.Value));
                         }
                         if (addedRoleIds.Any())
                         {
                             var queryCriterions = new List<ICriterion>
                             {
                                 Restrictions.On<Db.Entities.RBAC.SysRole>(y => y.Id).IsIn(req.RoleIds.ToArray())
                             };
                             var addedRoles = repo.Query<Db.Entities.RBAC.SysRole>(queryCriterions);
                             if (addedRoles != null && addedRoles.Any())
                             {
                                 user.Roles.AddRangeIfNotContains(addedRoles.ToArray());
                             }
                         }
                         #endregion

                         #region *Permission

                         if (removedPermissionIds.Any())
                         {
                             user.PermissionModules.RemoveWhere(x => removedPermissionIds.Contains(x.Id));
                         }
                         if (addedPermissionIds.Any())
                         {
                             var queryCriterions = new List<ICriterion>
                             {
                                 Restrictions.On<Db.Entities.RBAC.SysPermissionModule>(y => y.Id).IsIn(req.PermissionIds.ToArray())
                             };
                             var addedPermissions = repo.Query<Db.Entities.RBAC.SysPermissionModule>(queryCriterions);
                             if (addedPermissions != null && addedPermissions.Any())
                             {
                                 user.PermissionModules.AddRangeIfNotContains(addedPermissions.ToArray());
                             }
                         }
                         #endregion



                         #region *Permission restriction

                         if (removedPermissionRestrictionIds.Any())
                         {
                             user.Restrictions.RemoveWhere(x => removedPermissionRestrictionIds.Contains(x.Id));
                         }
                         if (addedPermissionRestrictionIds.Any())
                         {
                             var queryCriterions = new List<ICriterion>
                             {
                                 Restrictions.On<Db.Entities.RBAC.SysPermissionModule>(y => y.Id).IsIn(req.PermissionRestrictionIds.ToArray())
                             };
                             var addedPermissionRestrictions = repo.Query<Db.Entities.RBAC.SysPermissionModule>(queryCriterions);
                             if (addedPermissionRestrictions != null && addedPermissionRestrictions.Any())
                             {
                                 user.Restrictions.AddRangeIfNotContains(addedPermissionRestrictions.ToArray());
                             }
                         }
                         #endregion


                         repo.Update(user);
                     }
                     return response;
                 }
                 ).Build();
        }

        public ResponseBase UpdateUser(UpdateUserRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateUserRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                  {
                      var response = new ResponseBase();
                      using (var repo = new NhGlobalRepository())
                      {

                          var user = repo.GetById<Db.Entities.RBAC.SysUser>(req.UserId);
                          if (user == null)
                          {
                              throw new EeException(ErrorCodes.NotExistedUser, "User is not existed.");
                          }

                          if (!string.IsNullOrEmpty(req.NickName))
                          {
                              user.Nickname = req.NickName;
                          }
                          if (req.Status.HasValue)
                          {
                              user.Status = req.Status.Value;
                          }

                          user.UpdateTime = DateTime.Now;
                          repo.Update(user);
                      }
                      return response;
                  }
                  ).Build();
        }

        public ResponseBase ChangePassword(ChangePasswordRequest request)
        {
            return ServiceProcessor.CreateProcessor<ChangePasswordRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                .Inbound(
                () =>
                {
                    if (string.IsNullOrEmpty(request.NewPassword))
                    {
                        throw new EeException(ErrorCodes.IllegalCertificate, "The new password is null.");
                    }
                })
                 .Process(req =>
                 {
                     var response = new ResponseBase();
                     using (var repo = new NhGlobalRepository())
                     {

                         var user = repo.GetById<Db.Entities.RBAC.SysUser>(req.UserId);
                         if (user == null)
                         {
                             throw new EeException(ErrorCodes.NotExistedUser, "User is not existed.");
                         }
                         if (user.Password != req.OldPassword)
                         {
                             throw new EeException(ErrorCodes.IllegalCertificate, "The old password is incorrect.");
                         }
                         user.Password = req.NewPassword;
                         user.UpdateTime = DateTime.Now;
                         repo.Update(user);
                     }
                     return response;
                 }
                 ).Build();
        }


        #endregion


        public ResponseBase CreateCourt(CreateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateCourtRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhEntityRepository<Db.Entities.Court>())
                       {
                           var court = repo.Query(x => x.Name == req.Name)?.FirstOrDefault();
                           if (court != null)
                           {
                               throw new EeException(ErrorCodes.Existed, "Object is existed.");
                           }
                           //court = new Db.Entities.Court()
                           //{
                           //    Name = req.Name,
                           //    Province = req.Province,
                           //    City = req.City,
                           //    County = req.County,
                           //    Address = req.Address,
                           //    Rank = req.Rank,
                           //    ContactNo = req.ContactNo,
                           //};

                           court = DtoConverter.Mapper.Map<Db.Entities.Court>(req);

                           repo.Create(court);
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase UpdateCourt(UpdateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateCourtRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhEntityRepository<Db.Entities.Court>())
                       {
                           var court = repo.GetById(req.Id);
                           if (court == null)
                           {
                               throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                           }
                           court.Name = req.Name;
                           court.Rank = req.Rank;
                           court.Province = req.Province;
                           court.City = req.City;
                           court.County = req.County;
                           court.Address = req.Address;
                           court.ContactNo = req.ContactNo;
                           repo.Update(court);
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase RemoveCourt(RemoveCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveCourtRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhEntityRepository<Db.Entities.Court>())
                       {
                           foreach (var id in req.Ids)
                           {
                               var court = repo.GetById(id);
                               if (court != null)
                               {
                                   repo.Delete(court);
                               }
                           }
                       }
                       return response;
                   }
                  ).Build();
        }
        public ObjectResponse<Contact.DTO.ViewObjects.Court> GetCourt(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, ObjectResponse<Contact.DTO.ViewObjects.Court>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ObjectResponse<Contact.DTO.ViewObjects.Court>();
                       using (var repo = new NhEntityRepository<Db.Entities.Court>())
                       {
                           var entity = repo.GetById(request.Id);
                           response.Object = DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Court>(entity);
                       }
                       return response;
                   }
                  ).Build();
        }
        public QueryResponse<Contact.DTO.ViewObjects.Court> QueryCourt(QueryCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryCourtRequest, QueryResponse<Contact.DTO.ViewObjects.Court>>(MethodBase.GetCurrentMethod(), request)
                  .Inbound(() =>
                   {
                       if (request.Rank != null)
                       {
                           bool isDefined = false;
                           if (int.TryParse(request.Rank, out int rankValue))
                           {
                               isDefined = Enum.IsDefined(typeof(Ops.Contact.CourtRank), rankValue);
                               if (isDefined)
                               {
                                   request.Rank = ((Ops.Contact.CourtRank)Enum.ToObject(typeof(Ops.Contact.CourtRank), rankValue)).ToString();
                               }
                           }
                           else
                           {
                               isDefined = Enum.IsDefined(typeof(Ops.Contact.CourtRank), request.Rank);
                           }

                           if (!isDefined)
                           {
                               throw new EeException(ErrorCodes.InvalidParameter, "rank is invalid.");
                           }
                       }
                   })
                  .Process(req =>
                   {
                       var response = new QueryResponse<Contact.DTO.ViewObjects.Court>();

                       var queryCriterions = new List<ICriterion>();


                       using (var repo = new NhEntityRepository<Db.Entities.Court>())
                       {
                           if (req.Keys != null && req.Keys.Any())
                           {
                               queryCriterions.Add(Restrictions.On<Db.Entities.Court>(y => y.Id).IsIn(req.Keys));
                           }
                           else
                           {
                               if (!string.IsNullOrEmpty(req.Name))
                               {
                                   queryCriterions.Add(Restrictions.On<Db.Entities.Court>(y => y.Name).IsLike(req.Name, MatchMode.Anywhere));
                               }
                               if (!string.IsNullOrEmpty(req.Province))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Court>(x => x.Province == req.Province));
                               }
                               if (!string.IsNullOrEmpty(req.City))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Court>(x => x.City == req.City));
                               }
                               if (!string.IsNullOrEmpty(req.County))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Court>(x => x.County == req.County));
                               }
                               if (!string.IsNullOrEmpty(req.Rank))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Court>(x => x.Rank == req.Rank));
                               }

                           }
                           var query = repo.QueryByPageInCriterion(queryCriterions, x => x.Id, false, req.PageIndex, req.PageSize);
                           response.Total = query.Item2;
                           if (query.Item1.Any())
                           {
                               response.QueryList = query.Item1.ToList().Select(DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Court>).ToList();
                           }
                       }
                       return response;
                   }
                  ).Build();

        }


        public ResponseBase CreateJudge(CreateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateJudgeRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhGlobalRepository())
                       {
                           var judge = repo.Query<Db.Entities.Judge>(x => x.Name == req.Name && x.ContactNo == req.ContactNo).FirstOrDefault();
                           if (judge != null)
                           {
                               throw new EeException(ErrorCodes.Existed, "Object is existed.");
                           }
                           var court = repo.GetById<Db.Entities.Court>(req.InCourtId);
                           //judge = new Db.Entities.Judge()
                           //{
                           //    Name = req.Name,
                           //    ContactNo = req.ContactNo,
                           //    Gender = (int)req.Gender,
                           //    Grade = req.Grade.ToString(),
                           //    Duty = req.Duty,
                           //    InCourt = court,
                           //};

                           judge = DtoConverter.Mapper.Map<Db.Entities.Judge>(req);

                           repo.Create(judge);
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase UpdateJudge(UpdateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateJudgeRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhGlobalRepository())
                       {
                           var judge = repo.GetById<Db.Entities.Judge>(req.Id);
                           if (judge == null)
                           {
                               throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                           }

                           judge.Name = req.Name;
                           judge.ContactNo = req.ContactNo;
                           judge.Gender = (int)req.Gender;
                           judge.Grade = req.Grade.ToString();
                           judge.Duty = req.Duty;
                           judge.InCourt = repo.GetById<Db.Entities.Court>(req.InCourtId) ?? throw new EeException(ErrorCodes.NotFound, "Court is not found.");

                           repo.Update(judge);
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase RemoveJudge(RemoveJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveJudgeRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhEntityRepository<Db.Entities.Judge>())
                       {
                           foreach (var id in req.Ids)
                           {
                               var judge = repo.GetById(id);
                               if (judge != null)
                               {
                                   repo.Delete(judge);
                               }
                           }
                       }
                       return response;
                   }
                  ).Build();
        }
        public ObjectResponse<Contact.DTO.ViewObjects.Judge> GetJudge(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, ObjectResponse<Contact.DTO.ViewObjects.Judge>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ObjectResponse<Contact.DTO.ViewObjects.Judge>();
                       using (var repo = new NhEntityRepository<Db.Entities.Judge>())
                       {
                           var entity = repo.GetById(request.Id);
                           response.Object = DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Judge>(entity);
                       }
                       return response;
                   }
                  ).Build();
        }
        public QueryResponse<Contact.DTO.ViewObjects.Judge> QueryJudge(QueryJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryJudgeRequest, QueryResponse<Contact.DTO.ViewObjects.Judge>>(MethodBase.GetCurrentMethod(), request)
                  .Inbound(() =>
                   {
                       if (request.Grade != null)
                       {
                           bool isDefined = false;
                           if (int.TryParse(request.Grade, out int rankValue))
                           {
                               isDefined = Enum.IsDefined(typeof(Ops.Contact.JudgeGrade), rankValue);
                               if (isDefined)
                               {
                                   request.Grade = ((Ops.Contact.JudgeGrade)Enum.ToObject(typeof(Ops.Contact.JudgeGrade), rankValue)).ToString();
                               }
                           }
                           else
                           {
                               isDefined = Enum.IsDefined(typeof(Ops.Contact.JudgeGrade), request.Grade);
                           }

                           if (!isDefined)
                           {
                               throw new EeException(ErrorCodes.InvalidParameter, "grade is invalid.");
                           }
                       }

                   })
                  .Process(req =>
                   {
                       var response = new QueryResponse<Contact.DTO.ViewObjects.Judge>();

                       var queryCriterions = new List<ICriterion>();

                       using (var repo = new NhEntityRepository<Db.Entities.Judge>())
                       {
                           if (req.Keys != null && req.Keys.Any())
                           {
                               queryCriterions.Add(Restrictions.On<Db.Entities.Court>(y => y.Id).IsIn(req.Keys));
                           }
                           else
                           {
                               if (!string.IsNullOrEmpty(req.Name))
                               {
                                   queryCriterions.Add(Restrictions.On<Db.Entities.Judge>(y => y.Name).IsLike(req.Name, MatchMode.Anywhere));
                               }

                               if (!string.IsNullOrEmpty(req.Grade))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Judge>(x => x.Grade == req.Grade));
                               }
                               if (req.InCourtId.HasValue)
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Judge>(x => x.InCourt.Id == req.InCourtId.Value));
                               }
                           }
                           var query = repo.QueryByPageInCriterion(queryCriterions, x => x.Id, false, req.PageIndex, req.PageSize);
                           response.Total = query.Item2;
                           if (query.Item1.Any())
                           {
                               response.QueryList = query.Item1.ToList().Select(DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Judge>).ToList();
                           }
                       }
                       return response;
                   }
                  ).Build();
        }


        public ResponseBase CreateClient(CreateClientRequest request)
        {
            var now = DateTime.Now;

            return ServiceProcessor.CreateProcessor<CreateClientRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhGlobalRepository())
                       {

                           var client = repo.Query<Db.Entities.Client>(x => x.Name == req.Name && x.Abbreviation == req.Abbreviation).FirstOrDefault();
                           if (client != null)
                           {
                               throw new EeException(ErrorCodes.Existed, "Object is existed.");
                           }

                           //client = new Db.Entities.Client()
                           //{
                           //    Name = req.Name,
                           //    ContactNo = req.ContactNo,
                           //    Abbreviation = req.Abbreviation,
                           //    Impression = req.Impression,
                           //    IsNP = req.IsNP,

                           //    CreateTime = now,

                           //};

                           client = DtoConverter.Mapper.Map<Db.Entities.Client>(req);
                           client.CreateTime = now;


                           var properties = new List<Db.Entities.ClientProperty>();
                           foreach (var p in req.ClientProperties)
                           {
                               var clientPropertyItem = new Db.Entities.ClientProperty()
                               {
                                   CreateTime = now,
                                   Value = p.Value,
                                   Picker = repo.GetById<Db.Entities.Foundation.Picklist>(p.PickerId),
                                   Client = client,
                               };
                               repo.Create(clientPropertyItem);
                               properties.Add(clientPropertyItem);
                           }
                           client.Properties = properties;
                           repo.Create(client);

                       }
                       return response;
                   }
                  ).Build();

        }
        public ResponseBase UpdateClient(UpdateClientRequest request)
        {
            var now = DateTime.Now;
            return ServiceProcessor.CreateProcessor<UpdateClientRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhGlobalRepository())
                       {
                           var client = repo.GetById<Db.Entities.Client>(req.Id);
                           if (client == null)
                           {
                               throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                           }

                           client.Name = req.Name;
                           client.ContactNo = req.ContactNo;
                           client.Abbreviation = req.Abbreviation;
                           client.Impression = req.Impression;
                           client.IsNP = req.IsNP;

                           //remove
                           var toRemove = client.Properties.Where(x => !req.ClientProperties.Select(o => o.Id).Contains(x.Id));
                           if (toRemove != null && toRemove.Any())
                           {
                               foreach (var item in toRemove.ToList())
                               {
                                   client.Properties.Remove(item);
                                   var clientPropertyItem = repo.GetById<Db.Entities.ClientProperty>(item.Id);
                                   if (clientPropertyItem != null)
                                   {
                                       repo.Delete(clientPropertyItem);
                                   }
                               }
                           }

                           foreach (var p in req.ClientProperties.Where(x => x.PickerId > 0))
                           {
                               var existedObj = client.Properties.FirstOrDefault(x => x.Id == p.Id);
                               //update
                               if (existedObj != null && p.Id > 0)
                               {
                                   var hasUpdated = false;
                                   if (existedObj.Value != p.Value)
                                   {
                                       existedObj.Value = p.Value;
                                       hasUpdated = true;
                                   }
                                   if (existedObj.Picker.Id != p.PickerId && p.PickerId > 0)
                                   {
                                       var category = repo.GetById<Db.Entities.Foundation.Picklist>(p.PickerId);
                                       existedObj.Picker = category;
                                       hasUpdated = true;
                                   }
                                   if (hasUpdated)
                                   {
                                       existedObj.UpdateTime = now;
                                   }
                               }
                               //add
                               else
                               {
                                   var clientPropertyItem = new Db.Entities.ClientProperty()
                                   {
                                       CreateTime = now,
                                       Value = p.Value,
                                       Picker = repo.GetById<Db.Entities.Foundation.Picklist>(p.PickerId),
                                       Client = client,
                                   };
                                   client.Properties.Add(clientPropertyItem);
                               }
                           }


                           repo.Update(client);
                       }
                       return response;
                   }
                  ).Build();

        }
        public ResponseBase RemoveClient(RemoveClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveClientRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhEntityRepository<Db.Entities.Client>())
                       {
                           foreach (var id in req.Ids)
                           {
                               var client = repo.GetById(id);
                               if (client != null)
                               {
                                   repo.Delete(client);
                               }
                           }
                       }
                       return response;
                   }
                  ).Build();
        }
        public ObjectResponse<Contact.DTO.ViewObjects.Client> GetClient(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, ObjectResponse<Contact.DTO.ViewObjects.Client>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ObjectResponse<Contact.DTO.ViewObjects.Client>();
                       using (var repo = new NhEntityRepository<Db.Entities.Client>())
                       {
                           var entity = repo.GetById(request.Id);
                           response.Object = DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Client>(entity);
                       }
                       return response;
                   }
                  ).Build();

        }
        public QueryResponse<Contact.DTO.ViewObjects.Client> QueryClient(QueryClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryClientRequest, QueryResponse<Contact.DTO.ViewObjects.Client>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new QueryResponse<Contact.DTO.ViewObjects.Client>();

                       var queryCriterions = new List<ICriterion>();

                       using (var repo = new NhEntityRepository<Db.Entities.Client>())
                       {

                           if (req.Keys != null && req.Keys.Any())
                           {
                               queryCriterions.Add(Restrictions.On<Db.Entities.Court>(y => y.Id).IsIn(req.Keys));
                           }
                           else
                           {
                               if (!string.IsNullOrEmpty(req.Name))
                               {
                                   queryCriterions.Add(Restrictions.On<Db.Entities.Client>(y => y.Name).IsLike(req.Name, MatchMode.Anywhere));
                               }

                               if (req.IsNP.HasValue)
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Client>(x => x.IsNP == req.IsNP.Value));
                               }
                           }
                           var query = repo.QueryByPageInCriterion(queryCriterions, x => x.Id, false, req.PageIndex, req.PageSize);
                           response.Total = query.Item2;
                           if (query.Item1.Any())
                           {
                               response.QueryList = query.Item1.ToList().Select(DtoConverter.Mapper.Map<Contact.DTO.ViewObjects.Client>).ToList();
                           }
                       }
                       return response;
                   }
                  ).Build();
        }


        public ResponseBase CreateProject(CreateProjectRequest request)
        {
            var now = DateTime.Now;

            return ServiceProcessor.CreateProcessor<CreateProjectRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhGlobalRepository())
                       {

                           var project = repo.Query<Db.Entities.Project>(x => x.Name == req.Name && x.Code == req.Code).FirstOrDefault();
                           if (project != null)
                           {
                               throw new EeException(ErrorCodes.Existed, "Object is existed.");
                           }

                           project = DtoConverter.Mapper.Map<Db.Entities.Project>(req);
                           project.CreateTime = now;
                           project.Category = repo.GetById<Db.Entities.Foundation.Picklist>(req.CategoryId);
                           project.Cause = repo.GetById<Db.Entities.Foundation.Picklist>(req.CauseId);
                           project.Owner = repo.GetById<Db.Entities.RBAC.SysUser>(req.OwnerId);


                           project.AddAccount(DtoConverter.Mapper.Map<Db.Entities.ProjectAccount>(req.Account));

                           var involvedClients = new List<Db.Entities.ProjectClient>();
                           var todoList = new List<Db.Entities.Schedule>();
                           var progresses = new List<Db.Entities.ProjectProgress>();

                           if (req.InvolvedClientIds?.Any() ?? false)
                           {
                               int projectClientOrderNo = 0;
                               foreach (var id in req.InvolvedClientIds)
                               {
                                   if (id > 0)
                                   {
                                       var existedClient = repo.GetById<Db.Entities.Client>(id);
                                       if (existedClient != null)
                                       {
                                           var projectClient = new Db.Entities.ProjectClient()
                                           {
                                               Id = 0,
                                               InProject = project,
                                               Client = existedClient,
                                               CreateTime = now,
                                               OrderNo = projectClientOrderNo++,
                                           };
                                           involvedClients.Add(projectClient);
                                       }
                                   }

                               }
                           }

                           req.TodoList?.ToList().ForEach(x => todoList.Add(DtoConverter.Convert(x, project)));
                           req.Progresses?.ToList().ForEach(x => progresses.Add(DtoConverter.Convert(x, project)));

                           project.AddInvolvedClients(involvedClients);
                           project.AddTodoList(todoList);
                           project.AddProgresses(progresses);
                           repo.Create(project);
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase UpdateProject(UpdateProjectRequest request)
        {
            var now = DateTime.Now;

            return ServiceProcessor.CreateProcessor<UpdateProjectRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhGlobalRepository())
                       {
                           var project = repo.GetById<Db.Entities.Project>(req.Id);
                           if (project == null)
                           {
                               throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                           }
                           project.Name = req.Name;

                           project.Code = req.Code;
                           project.Details = req.Details;
                           project.Level = req.Level;
                           project.OtherLitigant = req.OtherLitigant;
                           project.InterestedParty = req.InterestedParty;
                           project.DealDate = req.DealDate;
                           project.UpdateTime = DateTime.Now;

                           project.UpdateAccount(DtoConverter.Mapper.Map<Db.Entities.ProjectAccount>(req.Account));

                           if (req.CategoryId > 0 && project.Category.Id != req.CategoryId)
                           {
                               project.Category = repo.GetById<Db.Entities.Foundation.Picklist>(req.CategoryId);
                           }
                           if (req.CauseId > 0 && project.Cause.Id != req.CauseId)
                           {
                               project.Cause = repo.GetById<Db.Entities.Foundation.Picklist>(req.CauseId);
                           }
                           if (req.OwnerId > 0 && project.Owner.Id != req.OwnerId)
                           {
                               project.Owner = repo.GetById<Db.Entities.RBAC.SysUser>(req.OwnerId);
                           }

                           var involvedClients = new List<Db.Entities.ProjectClient>();
                           if (req.InvolvedClientIds?.Any() ?? false)
                           {

                               var existedClientIds = project.InvolvedClients?.Select(x => x.Client)?.Select(x => x.Id) ?? new List<int>();
                               var toRemoveClientIds = existedClientIds?.Where(x => !req.InvolvedClientIds.Contains(x));

                               if (toRemoveClientIds != null && toRemoveClientIds.Any())
                               {

                                   toRemoveClientIds.ToList().ForEach(x =>
                                   {
                                       var pc = project.InvolvedClients.FirstOrDefault(o => o.Client.Id == x);
                                       repo.Delete(pc);
                                   });
                                   project.InvolvedClients = null;
                               }


                               var toAddClientIds = req.InvolvedClientIds?.Distinct()?.Where(x => !existedClientIds.Contains(x));

                               if (toAddClientIds != null && toAddClientIds.Any())
                               {
                                   int projectClientOrderNo = 0;
                                   foreach (var id in toAddClientIds)
                                   {
                                       if (id > 0)
                                       {
                                           var existedClient = repo.GetById<Db.Entities.Client>(id);
                                           if (existedClient != null)
                                           {
                                               var projectClient = new Db.Entities.ProjectClient()
                                               {
                                                   Id = 0,
                                                   InProject = project,
                                                   Client = existedClient,
                                                   CreateTime = now,
                                                   OrderNo = projectClientOrderNo++,
                                               };
                                               involvedClients.Add(projectClient);
                                           }
                                       }

                                   }
                                   project.InvolvedClients = involvedClients;
                               }
                           }
                           else
                           {
                               if (project.InvolvedClients?.Any() ?? false)
                               {
                                   for (int i = 0; i < project.InvolvedClients.ToList().Count; i++)
                                   {
                                       repo.Delete(project.InvolvedClients[i]);
                                   }

                                   project.InvolvedClients = null;
                               }
                           }

                           //update todolist
                           if (req.TodoList != null && req.TodoList.Any())
                           {
                               if (project.TodoList != null && project.TodoList.Any())
                               {
                                   var toRemove = project.TodoList.Where(x => !req.TodoList.Select(o => o.Id).Contains(x.Id));
                                   if (toRemove != null && toRemove.Any())
                                   {
                                       foreach (var item in toRemove.ToList())
                                       {
                                           project.TodoList.Remove(item);
                                           repo.Delete(item);
                                       }

                                   }
                                   foreach (var item in req.TodoList)
                                   {
                                       var existedObj = project.TodoList.FirstOrDefault(x => x.Id == item.Id);
                                       //update
                                       if (existedObj != null)
                                       {
                                           existedObj.Name = item.Name;
                                           existedObj.Priority = (int)item.Priority;
                                           existedObj.IsSetRemind = item.IsSetRemind;
                                           existedObj.RemindTime = item.RemindTime;
                                           existedObj.ExpiredTime = item.ExpiredTime;
                                           existedObj.Content = item.Content;
                                           existedObj.Status = (int)item.Status;
                                           existedObj.CompletedTime = item.CompletedTime;
                                       }
                                       //add
                                       else
                                       {
                                           var todoItem = new Db.Entities.Schedule()
                                           {
                                               Id = item.Id,
                                               InProject = project,
                                               Name = item.Name,
                                               Priority = (int)item.Priority,
                                               IsSetRemind = item.IsSetRemind,
                                               RemindTime = item.RemindTime,
                                               ExpiredTime = item.ExpiredTime,
                                               Content = item.Content,
                                               Status = (int)item.Status,
                                               CompletedTime = item.CompletedTime,
                                               CreateTime = now,
                                           };
                                           project.TodoList.Add(todoItem);
                                       }
                                   }
                               }
                               else
                               {
                                   project.TodoList = new List<Db.Entities.Schedule>();
                                   req.TodoList.ToList().ForEach(x => project.TodoList.Add(DtoConverter.Convert(x, project)));
                               }
                           }
                           else
                           {
                               if (project.TodoList != null && project.TodoList.Any())
                               {
                                   project.TodoList.ToList().ForEach(x => repo.Delete(x));
                                   project.TodoList = null;
                               }
                           }
                           //update progresses
                           if (req.Progresses != null && req.Progresses.Any())
                           {
                               if (project.Progresses != null && project.Progresses.Any())
                               {
                                   var toRemove = project.Progresses.Where(x => !req.Progresses.Select(o => o.Id).Contains(x.Id));
                                   if (toRemove != null && toRemove.Any())
                                   {
                                       foreach (var item in toRemove.ToList())
                                       {
                                           project.Progresses.Remove(item);
                                           repo.Delete(item);
                                       }

                                   }
                                   foreach (var item in req.Progresses)
                                   {
                                       var existedObj = project.Progresses.FirstOrDefault(x => x.Id == item.Id);
                                       //update
                                       if (existedObj != null)
                                       {
                                           existedObj.Content = item.Content;
                                           existedObj.HandleTime = item.HandleTime;
                                       }
                                       //add
                                       else
                                       {
                                           var todoItem = new Db.Entities.ProjectProgress()
                                           {
                                               Id = item.Id,
                                               InProject = project,
                                               Content = item.Content,
                                               HandleTime = item.HandleTime,
                                               CreateTime = now,
                                           };
                                           project.Progresses.Add(todoItem);
                                       }
                                   }
                                   repo.Update(project);
                               }
                               else
                               {
                                   project.Progresses = new List<Db.Entities.ProjectProgress>();
                                   req.Progresses.ToList().ForEach(x => project.Progresses.Add(DtoConverter.Convert(x, project)));
                                   repo.Update(project);
                               }
                           }
                           else
                           {
                               if (project.Progresses != null && project.Progresses.Any())
                               {
                                   project.Progresses.ToList().ForEach(x => repo.Delete(x));
                                   project.Progresses = null;
                               }
                           }
                           repo.Update(project);
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase RemoveProject(RemoveProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveProjectRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       using (var repo = new NhEntityRepository<Db.Entities.Project>())
                       {
                           foreach (var id in req.Ids)
                           {
                               var project = repo.GetById(id);
                               if (project != null)
                               {
                                   repo.Delete(project);
                               }
                           }
                       }
                       return response;
                   }
                  ).Build();

        }
        public ObjectResponse<Contact.DTO.ViewObjects.Project> GetProject(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, ObjectResponse<Contact.DTO.ViewObjects.Project>>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ObjectResponse<Contact.DTO.ViewObjects.Project>();
                       using (var repo = new NhEntityRepository<Db.Entities.Project>())
                       {
                           var entity = repo.GetById(request.Id);
                           response.Object = DtoConverter.Convert(entity);
                       }
                       return response;
                   }
                  ).Build();
        }
        public QueryResponse<Contact.DTO.ViewObjects.Project> QueryProject(QueryProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryProjectRequest, QueryResponse<Contact.DTO.ViewObjects.Project>>(MethodBase.GetCurrentMethod(), request)
                  .Inbound(() =>
                   {
                       if (!string.IsNullOrEmpty(request.DealDateFrom))
                       {
                           var result = DateTime.TryParse(request.DealDateFrom, out DateTime fromDate);
                           if (!result)
                           {
                               throw new EeException(ErrorCodes.InvalidParameter, "dealDateFrom invalid.");
                           }
                       }
                       if (!string.IsNullOrEmpty(request.DealDateTo))
                       {
                           var result = DateTime.TryParse(request.DealDateTo, out DateTime toDate);
                           if (!result)
                           {
                               throw new EeException(ErrorCodes.InvalidParameter, "dealDateTo invalid.");
                           }
                       }
                   })
                  .Process(req =>
                   {
                       var response = new QueryResponse<Contact.DTO.ViewObjects.Project>();
                       var queryCriterions = new List<ICriterion>();


                       using (var repo = new NhEntityRepository<Db.Entities.Project>())
                       {

                           if (req.Keys != null && req.Keys.Any())
                           {
                               queryCriterions.Add(Restrictions.On<Db.Entities.Court>(y => y.Id).IsIn(req.Keys));
                           }
                           else
                           {
                               if (!string.IsNullOrEmpty(req.CategoryCode))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Project>(x => x.Category.Code == req.CategoryCode));
                               }
                               if (!string.IsNullOrEmpty(req.Level))
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Project>(x => x.Level == req.Level));
                               }
                               if (!string.IsNullOrEmpty(req.Code))
                               {
                                   queryCriterions.Add(Restrictions.On<Db.Entities.Project>(y => y.Code).IsLike(req.Code, MatchMode.Anywhere));
                               }

                               if (!string.IsNullOrEmpty(req.Name))
                               {
                                   queryCriterions.Add(Restrictions.On<Db.Entities.Project>(y => y.Name).IsLike(req.Name, MatchMode.Anywhere));
                               }

                               if (req.OwnerId.HasValue)
                               {
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Project>(x => x.Owner.Id == req.OwnerId.Value));
                               }

                               if (!string.IsNullOrEmpty(req.DealDateFrom))
                               {
                                   var fromDate = DateTime.Parse(req.DealDateFrom);
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Project>(x => x.DealDate >= fromDate));
                               }
                               if (!string.IsNullOrEmpty(req.DealDateTo))
                               {
                                   var toDate = DateTime.Parse(req.DealDateTo);
                                   queryCriterions.Add(Restrictions.Where<Db.Entities.Project>(x => x.DealDate <= toDate));
                               }

                           }
                           var query = repo.QueryByPageInCriterion(queryCriterions, x => x.Id, false, req.PageIndex, req.PageSize);
                           response.Total = query.Item2;
                           if (query.Item1.Any())
                           {
                               response.QueryList = query.Item1.ToList().Select(DtoConverter.Convert).ToList();
                           }
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase SaveOrUpdateProjectTodoList(SaveOrUpdateProjectTodoListRequest request)
        {
            var now = DateTime.Now;

            return ServiceProcessor.CreateProcessor<SaveOrUpdateProjectTodoListRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       //TODO:
                       using (var repo = new NhGlobalRepository())
                       {
                           var project = repo.GetById<Db.Entities.Project>(req.ProjectId);
                           if (project == null)
                           {
                               throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                           }
                           if (req.TodoList != null)
                           {
                               if (project.TodoList != null && project.TodoList.Any())
                               {
                                   var toRemove = project.TodoList.Where(x => !req.TodoList.Select(o => o.Id).Contains(x.Id));
                                   if (toRemove != null && toRemove.Any())
                                   {
                                       foreach (var item in toRemove.ToList())
                                       {
                                           project.TodoList.Remove(item);
                                           repo.Delete(item);
                                       }

                                   }
                                   foreach (var item in req.TodoList)
                                   {
                                       var existedObj = project.TodoList.FirstOrDefault(x => x.Id == item.Id);
                                       //update
                                       if (existedObj != null)
                                       {
                                           existedObj.Name = item.Name;
                                           existedObj.Priority = (int)item.Priority;
                                           existedObj.IsSetRemind = item.IsSetRemind;
                                           existedObj.RemindTime = item.RemindTime;
                                           existedObj.ExpiredTime = item.ExpiredTime;
                                           existedObj.Content = item.Content;
                                           existedObj.Status = (int)item.Status;
                                           existedObj.CompletedTime = item.CompletedTime;
                                       }
                                       //add
                                       else
                                       {
                                           var todoItem = new Db.Entities.Schedule()
                                           {
                                               Id = item.Id,
                                               InProject = project,
                                               Name = item.Name,
                                               Priority = (int)item.Priority,
                                               IsSetRemind = item.IsSetRemind,
                                               RemindTime = item.RemindTime,
                                               ExpiredTime = item.ExpiredTime,
                                               Content = item.Content,
                                               Status = (int)item.Status,
                                               CompletedTime = item.CompletedTime,
                                               CreateTime = now,
                                           };
                                           project.TodoList.Add(todoItem);
                                       }
                                   }
                                   repo.Update(project);
                               }
                               else
                               {
                                   project.TodoList = new List<Db.Entities.Schedule>();
                                   req.TodoList.ToList().ForEach(x => project.TodoList.Add(DtoConverter.Convert(x, project)));
                                   repo.Update(project);
                               }
                           }
                       }
                       return response;
                   }
                  ).Build();
        }
        public ResponseBase SaveOrUpdateProjectProgress(SaveOrUpdateProjectProgressRequest request)
        {
            var now = DateTime.Now;

            return ServiceProcessor.CreateProcessor<SaveOrUpdateProjectProgressRequest, ResponseBase>(MethodBase.GetCurrentMethod(), request)
                  .Process(req =>
                   {
                       var response = new ResponseBase();
                       //TODO:
                       using (var repo = new NhGlobalRepository())
                       {
                           var project = repo.GetById<Db.Entities.Project>(req.ProjectId);
                           if (project == null)
                           {
                               throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                           }
                           if (req.Progresses != null)
                           {
                               if (project.Progresses != null && project.Progresses.Any())
                               {
                                   var toRemove = project.Progresses.Where(x => !req.Progresses.Select(o => o.Id).Contains(x.Id));
                                   if (toRemove != null && toRemove.Any())
                                   {
                                       foreach (var item in toRemove.ToList())
                                       {
                                           project.Progresses.Remove(item);
                                           repo.Delete(item);
                                       }
                                   }
                                   foreach (var item in req.Progresses)
                                   {
                                       var existedObj = project.Progresses.FirstOrDefault(x => x.Id == item.Id);
                                       //update
                                       if (existedObj != null)
                                       {
                                           existedObj.Content = item.Content;
                                           existedObj.HandleTime = item.HandleTime;
                                       }
                                       //add
                                       else
                                       {
                                           var todoItem = new Db.Entities.ProjectProgress()
                                           {
                                               Id = item.Id,
                                               InProject = project,
                                               Content = item.Content,
                                               HandleTime = item.HandleTime,
                                               CreateTime = now,
                                           };
                                           project.Progresses.Add(todoItem);
                                       }
                                   }
                                   repo.Update(project);
                               }
                               else
                               {
                                   project.Progresses = new List<Db.Entities.ProjectProgress>();
                                   req.Progresses.ToList().ForEach(x => project.Progresses.Add(DtoConverter.Convert(x, project)));
                                   repo.Update(project);
                               }
                           }
                       }
                       return response;
                   }
                  ).Build();

        }

        public ResponseBase CreateSchedule(CreateScheduleRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase UpdateSchedule(UpdateScheduleRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase RemoveSchedule(RemoveScheduleRequest request)
        {
            throw new NotImplementedException();
        }

        public ObjectResponse<Schedule> GetSchedule(IdRequest<string> request)
        {
            throw new NotImplementedException();
        }

        public QueryResponse<Schedule> QuerySchedule(QueryScheduleRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
