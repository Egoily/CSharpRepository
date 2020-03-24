using ee.Core.Framework.Schema;
using ee.Core.Net;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement;
using ee.iLawyer.Ops.Contact.Interfaces;
using System.Threading.Tasks;

namespace ee.iLawyer.ServiceProvider
{
    public class ILawyerServiceProvider : IILawyerService, IFoundation, ISystemUserManagement
    {

        private DataResponse Process(string resource, RequestBase request, int timeout = 60 * 1000)
        {
            DataResponse response = null;
            Task.Run(async () =>
            {
                response = await Processor.ProcessAsync(resource, request, timeout).ConfigureAwait(false);
            }).Wait();//此处启动线程是为了防止Async & Await模式造成死锁

            return response;
        }

        public ResponseBase CreateClient(CreateClientRequest request)
        {
            var resource = @"/api/lawyer/client/create";
            return Process(resource, request);
        }

        public ResponseBase CreateCourt(CreateCourtRequest request)
        {
            var resource = @"/api/lawyer/court/create";
            return Process(resource, request);
        }

        public ResponseBase CreateJudge(CreateJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/create";
            return Process(resource, request);
        }

        public ResponseBase CreateProject(CreateProjectRequest request)
        {
            var resource = @"/api/lawyer/project/create";
            return Process(resource, request);
        }

        public QueryResponse<Area> GetAreas(GetAreasRequest request)
        {
            var resource = @"/api/lawyer/infr/areas";
            return Process(resource, request, 120 * 1000).ToQueryResponse<Area>();
        }

        public ObjectResponse<Client> GetClient(IdRequest request)
        {
            var resource = @"/api/lawyer/client/get";
            return Process(resource, request).ToObjectResponse<Client>();
        }

        public ObjectResponse<Court> GetCourt(IdRequest request)
        {
            var resource = @"/api/lawyer/court/get";
            return Process(resource, request).ToObjectResponse<Court>();
        }

        public ObjectResponse<Judge> GetJudge(IdRequest request)
        {
            var resource = @"/api/lawyer/judge/get";
            return Process(resource, request).ToObjectResponse<Judge>();
        }

        public ObjectResponse<Project> GetProject(IdRequest request)
        {
            var resource = @"/api/lawyer/project/get";
            return Process(resource, request).ToObjectResponse<Project>();
        }



        public QueryResponse<Picker> GetPickers(GetPickersRequest request)
        {
            var resource = @"/api/lawyer/infr/pickers";
            return Process(resource, request).ToQueryResponse<Picker>();
        }

        public QueryResponse<Client> QueryClient(QueryClientRequest request)
        {
            var resource = @"/api/lawyer/client/query";
            return Process(resource, request).ToQueryResponse<Client>();
        }

        public QueryResponse<Court> QueryCourt(QueryCourtRequest request)
        {
            var resource = @"/api/lawyer/court/query";
            return Process(resource, request).ToQueryResponse<Court>();
        }

        public QueryResponse<Judge> QueryJudge(QueryJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/query";
            return Process(resource, request).ToQueryResponse<Judge>();
        }

        public QueryResponse<Project> QueryProject(QueryProjectRequest request)
        {
            var resource = @"/api/lawyer/project/query";
            return Process(resource, request).ToQueryResponse<Project>();
        }

        public ResponseBase RemoveClient(RemoveClientRequest request)
        {
            var resource = @"/api/lawyer/client/remove";
            return Process(resource, request);
        }

        public ResponseBase RemoveCourt(RemoveCourtRequest request)
        {
            var resource = @"/api/lawyer/court/remove";
            return Process(resource, request);
        }

        public ResponseBase RemoveJudge(RemoveJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/remove";
            return Process(resource, request);
        }

        public ResponseBase RemoveProject(RemoveProjectRequest request)
        {
            var resource = @"/api/lawyer/project/remove";
            return Process(resource, request);
        }

        public ResponseBase SaveOrUpdateProjectProgress(SaveOrUpdateProjectProgressRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ResponseBase SaveOrUpdateProjectTodoList(SaveOrUpdateProjectTodoListRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ResponseBase UpdateClient(UpdateClientRequest request)
        {
            var resource = @"/api/lawyer/client/update";
            return Process(resource, request);
        }

        public ResponseBase UpdateCourt(UpdateCourtRequest request)
        {
            var resource = @"/api/lawyer/court/update";
            return Process(resource, request);
        }

        public ResponseBase UpdateJudge(UpdateJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/update";
            return Process(resource, request);
        }

        public ResponseBase UpdateProject(UpdateProjectRequest request)
        {
            var resource = @"/api/lawyer/project/update";
            return Process(resource, request);
        }


        public QueryResponse<PermissionModule> GetPermissionModules(RequestBase request)
        {
            var resource = @"/api/lawyer/sys/permissionmodules";
            return Process(resource, request).ToQueryResponse<PermissionModule>();
        }

        public QueryResponse<Role> GetRoles(GetRolesRequest request)
        {
            var resource = @"/api/lawyer/sys/roles";
            return Process(resource, request).ToQueryResponse<Role>();
        }

        public QueryResponse<User> QueryUser(QueryUserRequest request)
        {
            var resource = @"/api/lawyer/sys/users";
            return Process(resource, request).ToQueryResponse<User>();
        }

        public ResponseBase Register(RegisterRequest request)
        {
            var resource = @"/api/lawyer/sys/register";
            return Process(resource, request);
        }

        public ObjectResponse<User> Login(LoginRequest request)
        {
            var resource = @"/api/lawyer/sys/login";
            return Process(resource, request).ToObjectResponse<User>();
        }

        public ResponseBase Logout(LogoutRequest request)
        {
            var resource = @"/api/lawyer/sys/logout";
            return Process(resource, request);
        }

        public ResponseBase Grant(GrantRequest request)
        {
            var resource = @"/api/lawyer/sys/grant";
            return Process(resource, request);
        }

        public ResponseBase UpdateUser(UpdateUserRequest request)
        {
            var resource = @"/api/lawyer/sys/updateuser";
            return Process(resource, request);
        }

        public ResponseBase ChangePassword(ChangePasswordRequest request)
        {
            var resource = @"/api/lawyer/sys/changepassword";
            return Process(resource, request);
        }



        public ResponseBase Create<T>(T request) where T : RequestBase, new()
        {
            var resource = @"/api/lawyer/court/create";
            return Process(resource, request);
        }

        public ResponseBase CreateSchedule(CreateScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ResponseBase UpdateSchedule(UpdateScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ResponseBase RemoveSchedule(RemoveScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ObjectResponse<Schedule> GetSchedule(IdRequest<string> request)
        {
            throw new System.NotImplementedException();
        }

        public QueryResponse<Schedule> QuerySchedule(QueryScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
