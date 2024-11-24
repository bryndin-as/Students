using StudentService.DAL.Model;

namespace StudentService.DAL.Contracts.Repositories
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllWithGradesAsync();
    }
}
