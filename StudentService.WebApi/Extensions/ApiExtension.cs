using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StudentService.DAL.Core;
using StudentService.DAL.DTO;
using StudentService.WebApi.Options;
using StudentService.WebApi.Request;

namespace StudentService.WebApi.Extensions
{
    public static class ApiExtension
    {
        public static void UseStudentApi(this IEndpointRouteBuilder routeBuilder, IOptions<ApiRouteOptions> options)
        {
            routeBuilder.MapGet(options.Value.Student, async (IDataService data) => 
            {
                var result = await data.GetStudentsAsync();
                return Results.Ok(result);
            });

            routeBuilder.MapPost(options.Value.Student, async (IDataService data, StudentCreateDTO studentCreateDTO) =>
            {
                var id = await data.CreateStudentAsync(studentCreateDTO);
                return Results.Ok(id);
            });

            routeBuilder.MapGet(options.Value.StudentGrade, async (IDataService data) =>
            {
                var result = await data.GetStudentWithGradesAsync();
                return Results.Ok(result);
            });
        }

        public static void UseSubjectApi(this IEndpointRouteBuilder routeBuilder, IOptions<ApiRouteOptions> options)
        {
            routeBuilder.MapGet(options.Value.Subject, async (IDataService data) =>
            {
                var result = await data.GetSubjectsAsync();
                return Results.Ok(result);
            });

            routeBuilder.MapPost(options.Value.Subject, async (IDataService data, SubjectCreateDTO subjectCreateDTO) =>
            {
                var id = await data.CreateSubjectAsync(subjectCreateDTO);
                return Results.Ok(id);
            });
        }

        public static void UseGradeApi(this IEndpointRouteBuilder routeBuilder, IOptions<ApiRouteOptions> options)
        {
            routeBuilder.MapGet(options.Value.Grade, async (IDataService data) =>
            {
                var result = await data.GetGradesAsync();
                return Results.Ok(result);
            });

            routeBuilder.MapPost(options.Value.Grade, async (IDataService data, GradeCreateDTO gradeCreateDTO) =>
            {
                var id = await data.CreateGradeAsync(gradeCreateDTO);
                return Results.Ok(id);
            });
        }

        public static void UseSeedDataApi(this IEndpointRouteBuilder routeBuilder, IOptions<ApiRouteOptions> options)
        {
            routeBuilder.MapPost(options.Value.SeedData, async (IDataService data, [FromBody] CountRequest countRequest) =>
            {
                await data.AddSeedTest(countRequest.Count);
                return Results.Ok();
            });
        }
    }
}
 