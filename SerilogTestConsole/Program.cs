using SerilogLibrary;
using System;
using System.Reflection.Metadata;

namespace SerilogTestConsole
{
    class Program
    {
        static PerfTracker _perfTracker;
        static void Main(string[] args)
        {
            var detail = GetLogDetail("Starting Performance ", null);
            _perfTracker = new PerfTracker("SLoggerConsole_Execution", null, detail.UserName,
                detail.Location, detail.Product, detail.Layer);

            // create perf logger
            SLogger.WritePerformance(detail);

            SLogger.WriteDiagnostics(detail);

            SLogger.WriteUsage(detail);

            try
            {
                var ex = new Exception("Something bad happened.");
                ex.Data.Add("input param", "nothing to see here");
                throw ex;
            } catch (Exception ex)
            {
                SLogger.WriteError(GetLogDetail("", ex));
            }

            _perfTracker.Stop();
        }
        static SLogDetail GetLogDetail(string msg, Exception ex)
        {
            return new SLogDetail
            {
                Product = "My Serilog",
                Location = "TestConsole",
                Layer = "TestProgram",
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = msg,
                Exception = ex
            };
        }
    }
}
