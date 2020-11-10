using SerilogLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace WeatherNetFramework.Services
{
    public class ApiExceptionLogger : ExceptionLogger
    {
        private readonly string _productName = null;
        public ApiExceptionLogger(string productName)
        {
            this._productName = productName;
        }
        public override bool ShouldLog(ExceptionLoggerContext context)
        {
            return true;
        }
        public override void Log(ExceptionLoggerContext context)
        {
            var logentry = new SLogDetail()
            {
                CorrelationId = new Guid().ToString(),
                Product = _productName,
                Layer = "API",
                Location = context.Request.RequestUri.AbsoluteUri,
                Hostname = Environment.MachineName,
                Exception = context.Exception,
                UserName = "temp"
            };
            SLogger.WriteError(logentry);
            base.Log(context);
        }
    }
}