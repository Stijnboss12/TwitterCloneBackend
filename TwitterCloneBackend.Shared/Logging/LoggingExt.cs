using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterCloneBackend.Shared.Logging
{
    public static class LoggingExt
    {
        public static void AddTwitterCloneLogging(this IHostBuilder host)
        {
            host.UseSerilog((ctx, logConfig) =>
            {
                logConfig.WriteTo.Console()
                    .ReadFrom.Configuration(ctx.Configuration)
                    .Enrich.WithEnvironmentName()
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Version", AppUtils.GetVersion());
            });
        }
    }
}
