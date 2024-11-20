using StudentService.DAL.Contracts;
using StudentService.DAL.Contracts.Repositories;
using StudentService.DAL.EF.Context;
using StudentService.DAL.EF.Module;
using StudentService.DAL.Model;

namespace StudentService.DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _context;

        public EFUnitOfWork(StudentDbContext context)
        {
            _context = context;
        }

        StudentRepository? studentRepository;

        SubjectRepository? subjectRepository;

        GradeRepository? gradeRepository;

        TestFillDbRepository? testFillDbRepository;

        public IGenericRepository<Student> StudentRepository =>
            studentRepository ??= new StudentRepository(_context);

        public IGenericRepository<Subject> SubjectRepository =>
            subjectRepository ??= new SubjectRepository(_context);

        public IGenericRepository<Grade> GradeRepository =>
            gradeRepository ??= new GradeRepository(_context);

        public ITestRepository TestFillDbRepository =>
            testFillDbRepository ??= new TestFillDbRepository(_context);

    }
}
