using Microsoft.EntityFrameworkCore;
using StudentService.DAL.Contracts.Repositories;
using StudentService.DAL.EF.Context;
using StudentService.DAL.EF.Repositories;
using StudentService.DAL.Model;

namespace StudentService.DAL.EF.Module
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllWithGradesAsync()
        {
            return await _context.Students
                .Include(student => student.Grades) 
                .ToListAsync();
        }
    }

    public class SubjectRepository(StudentDbContext context)
       : GenericRepository<Subject>(context)
    { }

    public class GradeRepository(StudentDbContext context)
       : GenericRepository<Grade>(context)
    { }

    public class TestFillDbRepository(StudentDbContext context)
        : TestRepository(context)
    { }
}
