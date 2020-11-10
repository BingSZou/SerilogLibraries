using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading;

namespace SerilogLibrary
{
    public class SLogDetail
    {
        public SLogDetail()
        {
            Timestamp = DateTime.Now;
        }
        public DateTime Timestamp { get; private set; }
        public string Message { get; set; }
        //WHERE
        public string Product { get; set; }
        public string Layer { get; set; }
        public string Location { get; set; }
        public string Hostname { get; set; }
        //WHO
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId    { get; set; }
        // EVERYTHING ELSE
        public long? ElapsedMilliseconds { get; set; }
        public Exception Exception { get; set; }
        public String CorrelationId { get; set; }
        public Dictionary<String, object> AdditionalInfo { get; set; }
    }
}