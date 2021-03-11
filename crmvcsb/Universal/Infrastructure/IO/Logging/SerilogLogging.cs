﻿
namespace crmvcsb.Infrastructure.IO.Logging
{
    using crmvcsb.Universal.Infrastructure;
    using Serilog;

    public class SerilogLogging
    {
        public Serilog.Core.Logger log;
        public SerilogLogging()
        {
            this.log = new LoggerConfiguration()
            .WriteTo.Debug()
            .CreateLogger();
        }

    }

    public class LoggerCustom : ILoggerCustom
    {
        SerilogLogging logger = new SerilogLogging();
        public void Information(string input)
        {
            this.logger.log.Information(input);
        }
    }
}