using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace RVTR.Lodging.WebApi
{
  internal class ConfigureTracingClient
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="configuration"></param>
    /// <param name="loggerFactory"></param>
    public static void UseZipkin(IApplicationBuilder applicationBuilder, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
      var lifetime = applicationBuilder.ApplicationServices.GetService<IHostApplicationLifetime>();
      var statistics = new Statistics();

      lifetime.ApplicationStarted.Register(() =>
      {
        var logger = new TracingLogger(loggerFactory, "zipkin.aspnet");
        var sender = new HttpZipkinSender(configuration.GetConnectionString("zipkin"), "application/json");
        var tracer = new ZipkinTracer(sender, new JSONSpanSerializer(), statistics);

        TraceManager.SamplingRate = 1.0f;
        TraceManager.Trace128Bits = true;
        TraceManager.RegisterTracer(tracer);
        TraceManager.Start(logger);
      });

      lifetime.ApplicationStopped.Register(() =>
      {
        TraceManager.Stop();
      });
    }
  }
}
