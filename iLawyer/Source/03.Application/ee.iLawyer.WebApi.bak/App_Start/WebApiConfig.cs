using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ee.iLawyer.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            //设置默认的为Swagger
            config.Routes.MapHttpRoute(
               name: "swagger_root",
               routeTemplate: "",
               defaults: null,
               constraints: null,
               handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
