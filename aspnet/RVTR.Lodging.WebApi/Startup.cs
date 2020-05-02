using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        options.UseNpgsql(Configuration.GetConnectionString("pgsl"));
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
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="provider"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseSwagger();
      app.UseSwaggerUI(options =>
      {
        foreach (var description in provider.ApiVersionDescriptions)
        {
          options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
        }
      });
      app.UseCors();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
