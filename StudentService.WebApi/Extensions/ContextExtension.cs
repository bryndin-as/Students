using Microsoft.EntityFrameworkCore;
using StudentService.DAL.EF.Context;

namespace StudentService.WebApi.Extensions
{
    public static class ContextExtension
    {
        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var conSt = configuration.GetConnectionString("DockerConnection");

            services.AddDbContext<StudentDbContext>(options =>
                options.UseNpgsql(conSt));
        }
    }
}
