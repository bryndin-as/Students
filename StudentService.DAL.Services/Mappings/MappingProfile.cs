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
        }
    }
}