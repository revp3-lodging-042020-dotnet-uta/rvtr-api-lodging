using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

namespace RVTR.Lodging.WebApi
{
  /// <summary>
  ///
  /// </summary>
  public class Startup
  {
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public IConfiguration Configuration { get; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApiVersioning(options =>
      {
        options.ReportApiVersions = true;
      });

      services.AddControllers();
      services.AddCors(options =>
      {
        options.AddPolicy("public", policy =>
        {
          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
      });

      services.AddDbContext<LodgingContext>(options =>
      {
        options.UseNpgsql(Configuration.GetConnectionString("pgsql"), options =>
        {
          options.EnableRetryOnFailure(3);
        });
      });

      services.AddScoped<UnitOfWork>();
      services.AddSwaggerGen();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
      });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="environment"></param>
    /// <param name="factory"></param>
    /// <param name="provider"></param>
    public void Configure(IApiVersionDescriptionProvider provider, IApplicationBuilder builder, ILoggerFactory factory, IWebHostEnvironment environment)
    {
      if (environment.IsDevelopment())
      {
        builder.UseDeveloperExceptionPage();
      }

      var lifetime = builder.ApplicationServices.GetService<IHostApplicationLifetime>();
      var statistics = new Statistics();

      lifetime.ApplicationStarted.Register(() =>
      {
        var logger = new TracingLogger(factory, "zipkin.aspnet");
        var sender = new HttpZipkinSender(Configuration.GetConnectionString("zipkin"), "application/json");
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

      builder.UseHttpsRedirection();
      builder.UseRouting();
      builder.UseSwagger();
      builder.UseSwaggerUI(options =>
      {
        foreach (var description in provider.ApiVersionDescriptions)
        {
          options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
      });

      builder.UseTracing("LodgingApi");
      builder.UseCors();
      builder.UseAuthorization();
      builder.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
