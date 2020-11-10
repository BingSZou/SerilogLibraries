using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace SerilogLibrary
{
    public class PerfTracker
    {
        private readonly SLogDetail _detail;
        private readonly Stopwatch _watch;

        public PerfTracker(string name, string userId, string userName, string location, string product, string layer)
        {
            _watch = new Stopwatch();
            _detail = new SLogDetail()
            {
                Message = name,
                UserId = userId,
                UserName = userName,
                Location = location,
                Product = product,
                Layer = layer,
                Hostname = Environment.MachineName
            };
            var beginTime = DateTime.Now;
            _detail.AdditionalInfo = new Dictionary<string, object>()
            {
                { "Started", beginTime.ToString(CultureInfo.InvariantCulture) }
            };
        }
        public PerfTracker(string name, string userId, string userName, string location, string product, string layer,
            IDictionary<string, object> additionalInfo)
            : this(name, userId, userName, location, product, layer)
        {
            foreach (var kvp in additionalInfo)
            {
                _detail.AdditionalInfo.Add("Input-" + kvp.Key, kvp.Value);
            }
        }
        public void Stop()
        {
            _watch.Stop();
            _detail.ElapsedMilliseconds = _watch.ElapsedMilliseconds;
            _detail.AdditionalInfo.Add("StopTime", _detail.ElapsedMilliseconds);
            SLogger.WritePerformance(_detail);
        }
    }
}
