using StudentService.DAL.EF.Context;
using StudentService.DAL.EF.Repositories;
using StudentService.DAL.Model;

namespace StudentService.DAL.EF.Module
{
    public class StudentRepository(StudentDbContext context)
       : GenericRepository<Student>(context)
    { }

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
