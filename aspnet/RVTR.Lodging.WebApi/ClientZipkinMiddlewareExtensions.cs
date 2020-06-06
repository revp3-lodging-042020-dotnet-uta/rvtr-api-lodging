using Microsoft.AspNetCore.Builder;

namespace RVTR.Lodging.WebApi
{
  internal static class ZipkinClientMiddlewareExtensions
  {
    public static IApplicationBuilder UseZipkin(this IApplicationBuilder applicationBuilder)
    {
      return applicationBuilder.UseMiddleware<ClientZipkinMiddleware>();
    }
  }
}
