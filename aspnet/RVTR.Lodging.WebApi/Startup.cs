using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO;
using zipkin4net.Middleware;

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
    private readonly IConfiguration _configuration;

    /// <summary>
    ///
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
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
        options.UseNpgsql(_configuration.GetConnectionString("pgsql"), options =>
        {
          options.EnableRetryOnFailure(3);
        });
      });

      services.AddScoped<ClientZipkinMiddleware>();
      services.AddScoped<UnitOfWork>();
      services.AddSwaggerGen();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ClientSwaggerOptions>();
      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
      });
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="hostEnvironment"></param>
    /// <param name="descriptionProvider"></param>
    /// <param name="context"></param>
    public void Configure(IApiVersionDescriptionProvider descriptionProvider,
                          IApplicationBuilder applicationBuilder,
                          IWebHostEnvironment hostEnvironment,
                          LodgingContext context)
    {
      if (hostEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();

        Seed.SeedDatabase(context);
      }

      applicationBuilder.UseZipkin();
      applicationBuilder.UseTracing("lodgingapi.rest");

      applicationBuilder.UseExceptionHandler(a => a.Run(async context =>
      {
        context.Response.StatusCode = 500;
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;

        var result = JsonConvert.SerializeObject(new { error = exception.Message });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
      }));

      //applicationBuilder.UseExceptionHandler(errorApp =>
      //{
      //  errorApp.Run(async context =>
      //  {
      //    context.Response.StatusCode = 500;
      //    context.Response.ContentType = "text/html";

      //    await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
      //    await context.Response.WriteAsync("ERROR!<br><br>\r\n");

      //    var exceptionHandlerPathFeature =
      //        context.Features.Get<IExceptionHandlerPathFeature>();



      //    // Use exceptionHandlerPathFeature to process the exception (for example, 
      //    // logging), but do NOT expose sensitive error information directly to 
      //    // the client.

      //    if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
      //    {
      //      await context.Response.WriteAsync("File error thrown!<br><br>\r\n");
      //    }

      //    await context.Response.WriteAsync("<a href=\"/\">Home</a><br>\r\n");
      //    await context.Response.WriteAsync("</body></html>\r\n");
      //    await context.Response.WriteAsync(new string(' ', 512)); // IE padding
      //  });
      //});
      //applicationBuilder.UseHsts();

      applicationBuilder.UseHttpsRedirection();
      applicationBuilder.UseRouting();
      applicationBuilder.UseSwagger();
      applicationBuilder.UseSwaggerUI(options =>
      {
        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
          options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
      });

      applicationBuilder.UseCors();
      applicationBuilder.UseAuthorization();
      applicationBuilder.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
