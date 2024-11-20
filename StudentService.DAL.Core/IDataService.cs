using StudentService.DAL.DTO;

namespace StudentService.DAL.Core
{
    public interface IDataService
    {
        Task AddSeedTest(int count); 

        Task<IEnumerable<StudentBaseDTO>> GetStudentsAsync(); 

        Task<IEnumerable<SubjectBaseDTO>> GetSubjectsAsync();  

        Task<IEnumerable<GradeBaseDTO>> GetGradesAsync();   

        Task<int> CreateStudentAsync(StudentCreateDTO item); 

        Task<int> CreateSubjectAsync(SubjectCreateDTO item); 

        Task<int> CreateGradeAsync(GradeCreateDTO item);  

    }
}
