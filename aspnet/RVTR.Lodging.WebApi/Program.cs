using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RVTR.Lodging.DataContext;

namespace RVTR.Lodging.WebApi
{
  public class Program
  {
    public static async void Main()
    {
      var host = CreateHostBuilder().Build();

      await CreateDbContextAsync(host);
      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder() =>
      Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
      {
        webBuilder.UseStartup<Startup>();
      });

    public static async Task CreateDbContextAsync(IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        var provider = scope.ServiceProvider;
        var context = provider.GetRequiredService<LodgingContext>();

        await context.Database.EnsureCreatedAsync();
      }
    }
  }
}
