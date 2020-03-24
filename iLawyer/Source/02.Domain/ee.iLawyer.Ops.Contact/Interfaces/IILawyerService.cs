using ee.Core.Framework.Schema;
using ee.Core.Net;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface IILawyerService
    {

        ResponseBase CreateCourt(CreateCourtRequest request);
        ResponseBase UpdateCourt(UpdateCourtRequest request);
        ResponseBase RemoveCourt(RemoveCourtRequest request);
        ObjectResponse<Court> GetCourt(IdRequest request);
        QueryResponse<Court> QueryCourt(QueryCourtRequest request);

        ResponseBase CreateJudge(CreateJudgeRequest request);
        ResponseBase UpdateJudge(UpdateJudgeRequest request);
        ResponseBase RemoveJudge(RemoveJudgeRequest request);
        ObjectResponse<Judge> GetJudge(IdRequest request);
        QueryResponse<Judge> QueryJudge(QueryJudgeRequest request);

        ResponseBase CreateClient(CreateClientRequest request);
        ResponseBase UpdateClient(UpdateClientRequest request);
        ResponseBase RemoveClient(RemoveClientRequest request);
        ObjectResponse<Client> GetClient(IdRequest request);
        QueryResponse<Client> QueryClient(QueryClientRequest request);

        ResponseBase CreateProject(CreateProjectRequest request);
        ResponseBase UpdateProject(UpdateProjectRequest request);
        ResponseBase RemoveProject(RemoveProjectRequest request);
        ObjectResponse<Project> GetProject(IdRequest request);
        QueryResponse<Project> QueryProject(QueryProjectRequest request);

        ResponseBase SaveOrUpdateProjectTodoList(SaveOrUpdateProjectTodoListRequest request);
        ResponseBase SaveOrUpdateProjectProgress(SaveOrUpdateProjectProgressRequest request);


        ResponseBase CreateSchedule(CreateScheduleRequest request);
        ResponseBase UpdateSchedule(UpdateScheduleRequest request);
        ResponseBase RemoveSchedule(RemoveScheduleRequest request);
        ObjectResponse<Schedule> GetSchedule(IdRequest<string> request);
        QueryResponse<Schedule> QuerySchedule(QueryScheduleRequest request);



    }
}
