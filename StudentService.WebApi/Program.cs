using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StudentService.DAL.Contracts;
using StudentService.DAL.Core;
using StudentService.DAL.EF;
using StudentService.DAL.EF.Context;
using StudentService.DAL.Services;
using StudentService.DAL.Services.Mappings;
using StudentService.WebApi.Extensions;
using StudentService.WebApi.Options;

namespace StudentService.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddContext(builder.Configuration);
            builder.Services.ConfigureRouteApi(builder.Configuration);

            builder.Services.AddAutoMapper(x => x.AddProfile<MappingProfile>());
            builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            builder.Services.AddScoped<IDataService, DataService>();

            var app = builder.Build();
            var options = app.Services.GetRequiredService<IOptions<ApiRouteOptions>>();

            //Update db
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StudentDbContext>();
                if (context.Database.GetPendingMigrations().Any())
                    context.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseStudentApi(options);
            app.UseSubjectApi(options);
            app.UseGradeApi(options);
            app.UseSeedDataApi(options);

            app.Run();

        }
    }
}
