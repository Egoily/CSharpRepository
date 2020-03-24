using ee.Core.Framework.Schema;
using ee.Core.Net;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO.ViewObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class HttpTests
    {

        [TestInitialize()]
        public void Initialize()
        {

        }

        [TestMethod()]
        public void Test()
        {
            var url = "http://localhost:2155/api/lawyer/sys/login";
            var request = new LoginRequest()
            {
                LoginName = "admin",
                Password = "admin",
            };
            var response = HttpInvoker.PostToString(url, null, JsonConvert.SerializeObject(request), 100 * 1000);

        }
        [TestMethod()]
        public async Task FluentClientTestPost()
        {
            var url = "http://localhost:2155/";

            var resource = "api/test/Post";

            using (var client = new FluentClient(url))
            {
                client.BaseClient.Timeout = TimeSpan.FromMilliseconds(100 * 1000);
                var response = await client.PostAsync(resource).WithArguments(new { para = "test" }).As<string>();

            }

        }
        [TestMethod()]
        public async Task FluentClientTestPost2()
        {
            var url = "http://localhost:2155/";

            var resource = "api/test/Post2";
            var para = new KeyValuePair<int, string>(1, "test");

            using (var client = new FluentClient(url))
            {
                client.BaseClient.Timeout = TimeSpan.FromMilliseconds(100 * 1000);
                var response = await client.PostAsync(resource).WithBody(para).As<string>();
                var response2 = await client.PostAsync(resource,para).As<string>();
            }

        }
        [TestMethod()]
        public async Task FluentClientTestGet()
        {
            var url = "http://localhost:2155/";

            var resource = "api/test";


            using (var client = new FluentClient(url))
            {
                client.BaseClient.Timeout = TimeSpan.FromMilliseconds(100 * 1000);
                var response = await client.GetAsync(resource).WithArguments(new { para = "test" }).As<string>();

            }

        }
    }
}
