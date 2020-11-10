using SerilogLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using WeatherNetFramework.Services;

namespace WeatherNetFramework
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Filters.Add(new UsageTrackerAttribute("WeatherNetFramework", "API", "RegisteringCore"));

            config.Services.Add(typeof(IExceptionLogger), new ApiExceptionLogger("WeatherNetFramework"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
