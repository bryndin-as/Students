using StudentService.WebApi.Options;

namespace StudentService.WebApi.Extensions
{
    public static class ApiRouteExtension
    {
        public static void ConfigureRouteApi(this IServiceCollection services, IConfiguration configuration) 
        {
            services.Configure<ApiRouteOptions>(configuration.GetSection("ApiRouteOptions"));
        }
    }
}
