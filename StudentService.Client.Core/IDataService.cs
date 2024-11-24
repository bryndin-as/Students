using StudentService.DAL.DTO;

namespace StudentService.Client.Core
{
    public interface IDataService
    {
        Task<IEnumerable<StudentBaseDTO>> GetStudentsAsync();

        Task<IEnumerable<SubjectBaseDTO>> GetSubjectsAsync();

        Task<IEnumerable<StudentWithGradesDTO>> GetStudentWithGradesAsync();

        Task<int> CreateStudentAsync(StudentCreateDTO item);

        Task<int> CreateSubjectAsync(SubjectCreateDTO item);

        Task AddSeedTest(int count);
    }
}
