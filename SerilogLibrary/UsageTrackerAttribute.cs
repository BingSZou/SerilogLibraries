using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SerilogLibrary
{
    public class UsageTrackerAttribute : ActionFilterAttribute
    {
        private string _product, _layer, _activity;
        public UsageTrackerAttribute(string product, string layer, string activity)
        {
            _product = product;
            _layer = layer;
            _activity = activity;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var dict = new Dictionary<string, object>();
            foreach(var key in context.RouteData.Values?.Keys)
            {
                dict.Add($"RouteData-{key}", (string)context.RouteData.Values[key]);
            }
            SLogger.WriteUsage(new SLogDetail()
            {
                Product = _product,
                Layer = _layer,
                AdditionalInfo = dict,

            });
            base.OnActionExecuted(context);
        }
    }
}
