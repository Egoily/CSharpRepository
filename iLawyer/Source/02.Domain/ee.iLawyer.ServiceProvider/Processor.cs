using ee.Core.Framework.Schema;
using ee.Core.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ee.iLawyer.ServiceProvider
{
    public static class Processor
    {
        public static string EndPoint = Configuration.Data.ServerUri;

        public static BaseDataResponse Process(string resource, BaseRequest request, int? timeout = 60 * 1000)
        {
            var uri = EndPoint + resource;
            var response = HttpInvoker.PostToString(uri, null, JsonConvert.SerializeObject(request), timeout);
            return JsonConvert.DeserializeObject<BaseDataResponse>(response);
        }
        public static Task<BaseDataResponse> ProcessAsync(string resource, BaseRequest request, int? timeout = 60 * 1000)
        {
            var uri = EndPoint + resource;

            return Task.Run(() =>
            {
                var response = HttpInvoker.PostToString(uri, null, JsonConvert.SerializeObject(request), timeout);
                return JsonConvert.DeserializeObject<BaseDataResponse>(response);
            });



        }
    }
}
