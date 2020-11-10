using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace SerilogLibrary
{
    public static class SLogger
    {
        private static readonly ILogger _diagnosticLogger;
        private static readonly ILogger _performanceLogger;
        private static readonly ILogger _errorLogger;
        private static readonly ILogger _usageLogger;
            
        static SLogger()
        {
            _performanceLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\temp\PerfText.log")
                .CreateLogger();

            _diagnosticLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\temp\DiagFile.log")
                .CreateLogger();
            _usageLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\temp\UsageFile.log")
                .CreateLogger();
            _errorLogger = new LoggerConfiguration()
                .WriteTo.File(path: @"c:\temp\ErrorFile.log")
                .CreateLogger();
        }

        public static void WriteDiagnostics(SLogDetail info)
        {
            var showDiagnostics = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("EnableDiagnostics"));
            if (!showDiagnostics)
                return;

            _diagnosticLogger.Write(LogEventLevel.Information, "{@FlogDetail}", info);
        }

        public static void WriteError(SLogDetail info)
        {
            _errorLogger.Write(LogEventLevel.Information, "{@FlogDetail}", info);
        }

        public static void WritePerformance(SLogDetail info)
        {
            _performanceLogger.Write(LogEventLevel.Information, "{@FlogDetail}", info);
        }

        public static void WriteUsage(SLogDetail info)
        {
            _usageLogger.Write(LogEventLevel.Information, "{@FlogDetail}", info);
        }

    }
}
