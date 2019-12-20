﻿using ee.Core.Framework.Schema;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using System.Reflection;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/lawyer")]
    public class LawyerController : BaseController
    {
        #region * Infr
        [Route("infr/areas"), HttpPost]
        public BaseDataResponse GetAreas([FromBody] GetAreasRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetAreasRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                 .Input(request, true)
                 .Process(req => { return Service.GetAreas(req); })
                 .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                 .Build();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("infr/pickers"), HttpPost]
        public BaseDataResponse GetPickers([FromBody] GetPickersRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetPickersRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetPickers(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        #endregion

        #region * System User Management
        [Route("sys/register"), HttpPost]
        public BaseDataResponse Register(RegisterRequest request)
        {
            return ServiceProcessor.CreateProcessor<RegisterRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Register(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                .Build();
        }
        [Route("sys/login"), HttpPost]
        public BaseDataResponse Login(LoginRequest request)
        {
            return ServiceProcessor.CreateProcessor<LoginRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Login(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                .Build();
        }
        [Route("sys/logout"), HttpPost]
        public BaseDataResponse Logout(LogoutRequest request)
        {
            return ServiceProcessor.CreateProcessor<LogoutRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Logout(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                .Build();
        }
        [Route("sys/grant"), HttpPost]
        public BaseDataResponse Grant(GrantRequest request)
        {
            return ServiceProcessor.CreateProcessor<GrantRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Grant(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                .Build();
        }
        [Route("sys/updateuser"), HttpPost]
        public BaseDataResponse UpdateUser(UpdateUserRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateUserRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.UpdateUser(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                .Build();
        }
        [Route("sys/changepassword"), HttpPost]
        public BaseDataResponse ChangePassword(ChangePasswordRequest request)
        {
            return ServiceProcessor.CreateProcessor<ChangePasswordRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.ChangePassword(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                .Build();
        }
        #endregion

        #region * Court
        [Route("court/get"), HttpPost]
        public BaseDataResponse GetCourt(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("court/create"), HttpPost]
        public BaseDataResponse CreateCourt([FromBody]CreateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("court/update"), HttpPost]
        public BaseDataResponse UpdateCourt([FromBody]UpdateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("court/remove"), HttpPost]
        public BaseDataResponse DeleteCourt(RemoveCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("court/query"), HttpPost]
        public BaseDataResponse QueryCourt(QueryCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

        #region * Judge
        [Route("judge/get"), HttpPost]
        public BaseDataResponse GetJudge(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("judge/create"), HttpPost]
        public BaseDataResponse CreateJudge([FromBody]CreateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("judge/update"), HttpPost]
        public BaseDataResponse UpdateJudge([FromBody]UpdateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("judge/remove"), HttpPost]
        public BaseDataResponse DeleteJudge(RemoveJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("judge/query"), HttpPost]
        public BaseDataResponse QueryJudge(QueryJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

        #region * Client
        [Route("client/get"), HttpPost]
        public BaseDataResponse GetClient(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("client/create"), HttpPost]
        public BaseDataResponse CreateClient([FromBody]CreateClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("client/update"), HttpPost]
        public BaseDataResponse UpdateClient([FromBody]UpdateClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("client/remove"), HttpPost]
        public BaseDataResponse DeleteClient(RemoveClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("client/query"), HttpPost]
        public BaseDataResponse QueryClient(QueryClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

        #region * Project
        [Route("project/get"), HttpPost]
        public BaseDataResponse GetProject(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("project/create"), HttpPost]
        public BaseDataResponse CreateProject([FromBody]CreateProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("project/update"), HttpPost]
        public BaseDataResponse UpdateProject([FromBody]UpdateProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("project/remove"), HttpPost]
        public BaseDataResponse DeleteProject(RemoveProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("project/query"), HttpPost]
        public BaseDataResponse QueryProject(QueryProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

    }
}