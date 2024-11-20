using Microsoft.EntityFrameworkCore;
using StudentService.DAL.Contracts;
using StudentService.DAL.Core;
using StudentService.DAL.DTO;
using StudentService.DAL.EF;
using StudentService.DAL.EF.Context;
using StudentService.DAL.Services;
using StudentService.DAL.Services.Mappings;
using StudentService.WebApi.Extensions;

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
            builder.Services.AddAutoMapper(x => x.AddProfile<MappingProfile>());
            builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            builder.Services.AddScoped<IDataService, DataService>();

            var app = builder.Build();

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


            app.MapGet("/seed-data", async (IDataService data, int count) =>
                {
                    await data.AddSeedTest(count);
                    return Results.Ok();
                });

            app.MapGet("api/students", async (IDataService data) =>
            {
                var result = await data.GetStudentsAsync();
                return Results.Ok(result);
            });

            app.MapGet("api/subjects", async (IDataService data) =>
            {
                var result = await data.GetSubjectsAsync();
                return Results.Ok(result);
            });

            app.MapGet("api/grades", async (IDataService data) =>
            {
                var result = await data.GetGradesAsync();
                return Results.Ok(result);
            });

            app.MapPost("api/students", async (IDataService data, StudentCreateDTO studentCreateDTO) =>
            {
                var id = await data.CreateStudentAsync(studentCreateDTO);
                return Results.Ok(id);  
            });

            app.MapPost("api/subjects", async (IDataService data, SubjectCreateDTO subjectCreateDTO) =>
            {
                var id = await data.CreateSubjectAsync(subjectCreateDTO);
                return Results.Ok(id);
            });

            app.MapPost("api/grades", async (IDataService data, GradeCreateDTO gradeCreateDTO) =>
            {
                var id = await data.CreateGradeAsync(gradeCreateDTO);
                return Results.Ok(id);
            });

            app.Run();
        }
    }
}
