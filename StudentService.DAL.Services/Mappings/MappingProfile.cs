using AutoMapper;
using StudentService.DAL.DTO;
using StudentService.DAL.Model;

namespace StudentService.DAL.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student,StudentBaseDTO>();    
            CreateMap<Subject,SubjectBaseDTO>();    
            CreateMap<Grade,GradeBaseDTO>();    

            CreateMap<StudentCreateDTO, Student>();
            CreateMap<SubjectCreateDTO, Subject>();
            CreateMap<GradeCreateDTO, Grade>();

            CreateMap<Student, StudentWithGradesDTO>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StudentSurname, opt => opt.MapFrom(src => src.Surname))
            .ForMember(dest => dest.SubjectCount, opt => opt.MapFrom(src => src.Grades.Select(g => g.SubjectId).Distinct().Count()))
            .ForMember(dest => dest.AverageGrade, opt => opt.MapFrom(src =>
                src.Grades.Any() ? Math.Round(src.Grades.Average(g => g.Value), 2) : 0));
        }
    }
}