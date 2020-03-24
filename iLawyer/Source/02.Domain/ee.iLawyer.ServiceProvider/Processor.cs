using ee.Core.Framework.Schema;
using ee.Core.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ee.iLawyer.ServiceProvider
{
    public static class Processor
    {
        public static string EndPoint = Configuration.Data.ServerUri;

        public static async Task<DataResponse> ProcessAsync(string resource, RequestBase request, int timeout = 60 * 1000)
        {
            var uri = EndPoint + resource;

            var response = await HttpInvoker.PostToStringAsync(uri, null, JsonConvert.SerializeObject(request), timeout).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<DataResponse>(response);


        }
        public static async Task<DataResponse> PostProcessAsync(string resource, RequestBase request, int timeout = 60 * 1000)
        {

            using (var client = new FluentClient(EndPoint))
            {
                client.BaseClient.Timeout = System.TimeSpan.FromMilliseconds(timeout);
                return await client.PostAsync(resource, request).As<DataResponse>();
            }

        }
    }
}
