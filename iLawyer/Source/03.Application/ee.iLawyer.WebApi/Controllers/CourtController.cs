using ee.Core.Framework.Schema;
using ee.Core.Net;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/court")]
    public class CourtController : BaseController
    {

        // GET api/Court/5
        public ObjectResponse<Court> Get(int id)
        {
            return Service.GetCourt(new IdRequest() { Id = id });
        }

        // POST api/Court
        public ResponseBase Post([FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.CreateCourtRequest>(para.ToString());
            return Service.CreateCourt(request);
        }

        // PUT api/Court/{id}
        public ResponseBase Put(int id, [FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.UpdateCourtRequest>(para.ToString());
            request.Id = id;
            return Service.UpdateCourt(request);
        }

        // DELETE api/Court/{id}
        public ResponseBase Delete(int id)
        {
            return Service.RemoveCourt(new Ops.Contact.Args.RemoveCourtRequest() { Ids = new List<int>() { id } });
        }
        [Route("query"), HttpGet]
        public QueryResponse<Court> Query(string province, string city, string county, string rank, string name, int pageIndex, int pageSize)
        {

            return Service.QueryCourt(
                new QueryCourtRequest()
                {
                    Province = province,
                    City = city,
                    County = county,
                    Name = name,
                    Rank = rank,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                });
        }
    }
}