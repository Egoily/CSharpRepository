﻿using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects.SystemManagement;
using ee.iLawyer.Ops.Contact.Interfaces;

namespace ee.iLawyer.ServiceProvider
{
    public class ILawyerServiceProvider : IILawyerService, IFoundation, ISystemUserManagement
    {

        private BaseDataResponse Process(string resource, BaseRequest request, int? timeout = 60 * 1000)
        {

            var task =  Processor.ProcessAsync(resource, request,timeout);

            return task.Result;
            //return Processor.Process(resource, request);
        }

        public BaseResponse CreateClient(CreateClientRequest request)
        {
            var resource = @"/api/lawyer/client/create";
            return Process(resource, request);
        }

        public BaseResponse CreateCourt(CreateCourtRequest request)
        {
            var resource = @"/api/lawyer/court/create";
            return Process(resource, request);
        }

        public BaseResponse CreateJudge(CreateJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/create";
            return Process(resource, request);
        }

        public BaseResponse CreateProject(CreateProjectRequest request)
        {
            var resource = @"/api/lawyer/project/create";
            return Process(resource, request);
        }

        public BaseQueryResponse<Area> GetAreas(GetAreasRequest request)
        {
            var resource = @"/api/lawyer/infr/areas";
            return Process(resource, request, 120 * 1000).ToBaseQueryResponse<Area>();
        }

        public BaseObjectResponse<Client> GetClient(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/client/get";
            return Process(resource, request).ToBaseObjectResponse<Client>();
        }

        public BaseObjectResponse<Court> GetCourt(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/court/get";
            return Process(resource, request).ToBaseObjectResponse<Court>();
        }

        public BaseObjectResponse<Judge> GetJudge(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/judge/get";
            return Process(resource, request).ToBaseObjectResponse<Judge>();
        }

        public BaseObjectResponse<Project> GetProject(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/project/get";
            return Process(resource, request).ToBaseObjectResponse<Project>();
        }



        public BaseQueryResponse<Picker> GetPickers(GetPickersRequest request)
        {
            var resource = @"/api/lawyer/infr/pickers";
            return Process(resource, request).ToBaseQueryResponse<Picker>();
        }

        public BaseQueryResponse<Client> QueryClient(QueryClientRequest request)
        {
            var resource = @"/api/lawyer/client/query";
            return Process(resource, request).ToBaseQueryResponse<Client>();
        }

        public BaseQueryResponse<Court> QueryCourt(QueryCourtRequest request)
        {
            var resource = @"/api/lawyer/court/query";
            return Process(resource, request).ToBaseQueryResponse<Court>();
        }

        public BaseQueryResponse<Judge> QueryJudge(QueryJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/query";
            return Process(resource, request).ToBaseQueryResponse<Judge>();
        }

        public BaseQueryResponse<Project> QueryProject(QueryProjectRequest request)
        {
            var resource = @"/api/lawyer/project/query";
            return Process(resource, request).ToBaseQueryResponse<Project>();
        }

        public BaseResponse RemoveClient(RemoveClientRequest request)
        {
            var resource = @"/api/lawyer/client/remove";
            return Process(resource, request);
        }

        public BaseResponse RemoveCourt(RemoveCourtRequest request)
        {
            var resource = @"/api/lawyer/court/remove";
            return Process(resource, request);
        }

        public BaseResponse RemoveJudge(RemoveJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/remove";
            return Process(resource, request);
        }

        public BaseResponse RemoveProject(RemoveProjectRequest request)
        {
            var resource = @"/api/lawyer/project/remove";
            return Process(resource, request);
        }

        public BaseResponse SaveOrUpdateProjectProgress(SaveOrUpdateProjectProgressRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse SaveOrUpdateProjectTodoList(SaveOrUpdateProjectTodoListRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse UpdateClient(UpdateClientRequest request)
        {
            var resource = @"/api/lawyer/client/update";
            return Process(resource, request);
        }

        public BaseResponse UpdateCourt(UpdateCourtRequest request)
        {
            var resource = @"/api/lawyer/court/update";
            return Process(resource, request);
        }

        public BaseResponse UpdateJudge(UpdateJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/update";
            return Process(resource, request);
        }

        public BaseResponse UpdateProject(UpdateProjectRequest request)
        {
            var resource = @"/api/lawyer/project/update";
            return Process(resource, request);
        }


        public BaseQueryResponse<PermissionModule> GetPermissionModules(BaseRequest request)
        {
            var resource = @"/api/lawyer/sys/permissionmodules";
            return Process(resource, request).ToBaseQueryResponse<PermissionModule>();
        }

        public BaseQueryResponse<Role> GetRoles(GetRolesRequest request)
        {
            var resource = @"/api/lawyer/sys/roles";
            return Process(resource, request).ToBaseQueryResponse<Role>();
        }

        public BaseQueryResponse<User> QueryUser(QueryUserRequest request)
        {
            var resource = @"/api/lawyer/sys/users";
            return Process(resource, request).ToBaseQueryResponse<User>();
        }

        public BaseResponse Register(RegisterRequest request)
        {
            var resource = @"/api/lawyer/sys/register";
            return Process(resource, request);
        }

        public BaseObjectResponse<User> Login(LoginRequest request)
        {
            var resource = @"/api/lawyer/sys/login";
            return Process(resource, request).ToBaseObjectResponse<User>();
        }

        public BaseResponse Logout(LogoutRequest request)
        {
            var resource = @"/api/lawyer/sys/logout";
            return Process(resource, request);
        }

        public BaseResponse Grant(GrantRequest request)
        {
            var resource = @"/api/lawyer/sys/grant";
            return Process(resource, request);
        }

        public BaseResponse UpdateUser(UpdateUserRequest request)
        {
            var resource = @"/api/lawyer/sys/updateuser";
            return Process(resource, request);
        }

        public BaseResponse ChangePassword(ChangePasswordRequest request)
        {
            var resource = @"/api/lawyer/sys/changepassword";
            return Process(resource, request);
        }



        public BaseResponse Create<T>(T request) where T : BaseRequest, new()
        {
            var resource = @"/api/lawyer/court/create";
            return Process(resource, request);
        }

        public BaseResponse CreateSchedule(CreateScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse UpdateSchedule(UpdateScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse RemoveSchedule(RemoveScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseObjectResponse<Schedule> GetSchedule(BaseIdRequest<string> request)
        {
            throw new System.NotImplementedException();
        }

        public BaseQueryResponse<Schedule> QuerySchedule(QueryScheduleRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
