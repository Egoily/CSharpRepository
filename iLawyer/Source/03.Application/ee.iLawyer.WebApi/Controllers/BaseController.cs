using ee.iLawyer.Ops;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{

    public class BaseController : ApiController
    {
        protected static ILawyerService Service = new ILawyerService();
    }
}