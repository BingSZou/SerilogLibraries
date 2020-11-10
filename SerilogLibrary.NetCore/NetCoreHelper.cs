using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace SerilogLibrary.NetCore
{
    public static class NetCoreHelper
    {
        public static void LogNetCoreDiagnostics(string product, string layer, string activity, string message,
            HttpContext context, Dictionary<string, object> additionalInfo = null)
        {
            var logDetail = GetSLogDetail(product, layer, activity, context, additionalInfo);
            logDetail.Message = message;
            SLogger.WriteDiagnostics(logDetail);
        }

        public static void LogNetCoreError(string product, string layer, string activity, Exception exception,
            HttpContext context, Dictionary<string, object> additionalInfo = null)
        {
            var logDetail = GetSLogDetail(product, layer, activity, context, additionalInfo);
            logDetail.Exception = exception;
            SLogger.WriteDiagnostics(logDetail);
        }

        public static void LogNetCoreUsage(string product, string layer, string activity,
            HttpContext context, Dictionary<string, object> additionalInfo = null)
        {
            SLogger.WriteUsage(GetSLogDetail(product, layer, activity, context, additionalInfo));
        }
        public static void LogNetCorePerformance(string proudct, string layer, string activity,
            HttpContext context, Dictionary<string, object> additionalInfo = null)
        {

        }

        private static SLogDetail GetSLogDetail(string product, string layer, string activity,
            HttpContext context, Dictionary<string, object> additionalInfo)
        {
            var detail = new SLogDetail()
            {
                Product = product,
                Layer = layer,
                AdditionalInfo = additionalInfo ?? new Dictionary<string, object>(),
                CorrelationId = Activity.Current?.Id ?? context.TraceIdentifier,
                Hostname = Environment.MachineName,

            };

            SetUserInfo(detail);
            SetRequestData(detail, context);

            return detail;
        }
        private static void SetUserInfo(SLogDetail detail)
        {
            var user = ClaimsPrincipal.Current;
            if (user == null)
                return;

            int idx = 0;
            foreach(var claim in user.Claims)
            {
                if (claim.Type == ClaimTypes.Name)
                    detail.UserName = claim.Value;
                else if (claim.Type == ClaimTypes.NameIdentifier)
                    detail.UserId = claim.Value;
                else
                    detail.AdditionalInfo.Add($"Claim-{claim.Type}-{idx}", claim.Value);
                idx++;
            }
        }
        private static void SetRequestData(SLogDetail detail, HttpContext context)
        {
            var request = context.Request;
            if (request == null)
                return;
            detail.Location = request.GetDisplayUrl();
            if (request.Headers.TryGetValue("User-Agent", out StringValues useragent))
                detail.AdditionalInfo.Add("UserAgent", useragent);
            if (request.Headers.TryGetValue("Accepted-Language", out StringValues language))
                detail.AdditionalInfo.Add("Language", language);
            var qdict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(request.QueryString.ToString());
            foreach(var kvp in qdict)
            {
                detail.AdditionalInfo.Add($"QueryString-{kvp.Key}", kvp.Value);
            }
        }
    }
}
