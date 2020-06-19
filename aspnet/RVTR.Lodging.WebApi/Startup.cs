using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
      });

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

      // Implements a global exception handler
      applicationBuilder.UseExceptionHandler(a => a.Run(async context =>
      {
        // creates an empty object for the exception (so as not to leak server data)
        var result = new ObjectResult("");
        // sets the status code for the exception response to server error
        result.StatusCode = 500;

        var JsonResult = JsonConvert.SerializeObject(result);
        context.Response.ContentType = "application/json";

        // writes the object into a readable response
        await context.Response.WriteAsync(JsonResult);
      }));

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
