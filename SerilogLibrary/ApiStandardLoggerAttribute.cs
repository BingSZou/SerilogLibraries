using SerilogLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WeatherNetFramework.Services
{
    public class ApiStandardLoggerAttribute : ActionFilterAttribute
    {
        PerfTracker _perfTracker = null;
        private readonly string _productName = null;
        public ApiStandardLoggerAttribute(string productName)
        {
            _productName = productName;
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var perfTracker = new PerfTracker("WeatherNetFramework_app",
                null,
                actionContext.Request.RequestUri.UserInfo ?? "Anonymous",
                actionContext.Request.RequestUri.AbsoluteUri,
                _productName,
                "API");
            actionContext.Request.Properties["PerfTracker"] = perfTracker;
            base.OnActionExecuting(actionContext);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Properties.TryGetValue("PerfTracker", out object perfTrackerObject))
            {
                var perfTracker = perfTrackerObject as PerfTracker;
                if (perfTracker != null)
                    perfTracker.Stop();
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}