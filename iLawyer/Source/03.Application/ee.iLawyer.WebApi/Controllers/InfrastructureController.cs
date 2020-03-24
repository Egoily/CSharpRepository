using ee.Core.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/infr")]
    public class InfrastructureController : BaseController
    {
        /// <summary>
        /// Gets the list of area infos
        /// </summary>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        [Route("Areas"), HttpGet]
        public QueryResponse<Area> GetAreas(int pageIndex, int pageSize)
        {

            var areas = Service.GetAreas(new GetAreasRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            });

            return areas;
        }

     
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("Pickers"), HttpGet]
        public QueryResponse<Picker> GetPickers()
        {
            return Service.GetPickers(new GetPickersRequest());
        }
    }
}