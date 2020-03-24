using ee.Core.Framework.Schema;
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
        public DataResponse GetAreas([FromBody] GetAreasRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetAreasRequest, DataResponse>(MethodBase.GetCurrentMethod())
                 .Input(request, true)
                 .Process(req => { return Service.GetAreas(req); })
                 .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                 .Build();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("infr/pickers"), HttpPost]
        public DataResponse GetPickers([FromBody] GetPickersRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetPickersRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetPickers(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }

        #endregion

        #region * System User Management
        [Route("sys/register"), HttpPost]
        public DataResponse Register(RegisterRequest request)
        {
            return ServiceProcessor.CreateProcessor<RegisterRequest, DataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Register(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                .Build();
        }
        [Route("sys/login"), HttpPost]
        public DataResponse Login(LoginRequest request)
        {
            return ServiceProcessor.CreateProcessor<LoginRequest, DataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Login(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                .Build();
        }
        [Route("sys/logout"), HttpPost]
        public DataResponse Logout(LogoutRequest request)
        {
            return ServiceProcessor.CreateProcessor<LogoutRequest, DataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Logout(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                .Build();
        }
        [Route("sys/grant"), HttpPost]
        public DataResponse Grant(GrantRequest request)
        {
            return ServiceProcessor.CreateProcessor<GrantRequest, DataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.Grant(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                .Build();
        }
        [Route("sys/updateuser"), HttpPost]
        public DataResponse UpdateUser(UpdateUserRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateUserRequest, DataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.UpdateUser(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                .Build();
        }
        [Route("sys/changepassword"), HttpPost]
        public DataResponse ChangePassword(ChangePasswordRequest request)
        {
            return ServiceProcessor.CreateProcessor<ChangePasswordRequest, DataResponse>(MethodBase.GetCurrentMethod())
                .Input(request, true)
                .Process(req => { return Service.ChangePassword(req); })
                .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                .Build();
        }
        #endregion

        #region * Court
        [Route("court/get"), HttpPost]
        public DataResponse GetCourt(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        [Route("court/create"), HttpPost]
        public DataResponse CreateCourt([FromBody]CreateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateCourtRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("court/update"), HttpPost]
        public DataResponse UpdateCourt([FromBody]UpdateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateCourtRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }

        [Route("court/remove"), HttpPost]
        public DataResponse DeleteCourt(RemoveCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveCourtRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("court/query"), HttpPost]
        public DataResponse QueryCourt(QueryCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryCourtRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryCourt(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        #endregion

        #region * Judge
        [Route("judge/get"), HttpPost]
        public DataResponse GetJudge(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        [Route("judge/create"), HttpPost]
        public DataResponse CreateJudge([FromBody]CreateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateJudgeRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("judge/update"), HttpPost]
        public DataResponse UpdateJudge([FromBody]UpdateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateJudgeRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }

        [Route("judge/remove"), HttpPost]
        public DataResponse DeleteJudge(RemoveJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveJudgeRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("judge/query"), HttpPost]
        public DataResponse QueryJudge(QueryJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryJudgeRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryJudge(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        #endregion

        #region * Client
        [Route("client/get"), HttpPost]
        public DataResponse GetClient(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        [Route("client/create"), HttpPost]
        public DataResponse CreateClient([FromBody]CreateClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateClientRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("client/update"), HttpPost]
        public DataResponse UpdateClient([FromBody]UpdateClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateClientRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }

        [Route("client/remove"), HttpPost]
        public DataResponse DeleteClient(RemoveClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveClientRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("client/query"), HttpPost]
        public DataResponse QueryClient(QueryClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryClientRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryClient(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        #endregion

        #region * Project
        [Route("project/get"), HttpPost]
        public DataResponse GetProject(IdRequest request)
        {
            return ServiceProcessor.CreateProcessor<IdRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        [Route("project/create"), HttpPost]
        public DataResponse CreateProject([FromBody]CreateProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateProjectRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("project/update"), HttpPost]
        public DataResponse UpdateProject([FromBody]UpdateProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateProjectRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }

        [Route("project/remove"), HttpPost]
        public DataResponse DeleteProject(RemoveProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveProjectRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();
        }
        [Route("project/query"), HttpPost]
        public DataResponse QueryProject(QueryProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryProjectRequest, DataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryProject(req); })
                  .UsingResponseConverter(new Core.Framework.Processor.ResponseConverter<DataResponse>(Converters.ToDataResponse))
                  .Build();

        }
        #endregion

    }
}