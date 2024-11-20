using Bogus;
using StudentService.DAL.Contracts.Repositories;
using StudentService.DAL.EF.Context;
using StudentService.DAL.Model;

namespace StudentService.DAL.EF.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly StudentDbContext _context;

        public TestRepository(StudentDbContext context)
        {
            _context = context;
            
        }

        public async Task SeedTestDataAsync(int count)
        {
            try
            {
                await AddStudent(count);
                await AddSubject(count);
                await AddGrade(count);

            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
        }

        public async Task AddStudent(int count)
        {
            var students = Enumerable.Range(1, count).Select(r =>
            {
                var faker = new Faker();
                var student = new Student
                {
                    Name = faker.Person.FirstName,
                    Surname = faker.Person.LastName,
                };
                return student;
            }).ToList();

            _context.Students.AddRange(students);
            await _context.SaveChangesAsync();
        }

        public async Task AddSubject(int count)
        {
            var subjects = Enumerable.Range(1, count).Select(r =>
            {
                var faker = new Faker(); 
                return new Subject
                {
                    Name = faker.Commerce.Department(), 
                };
            }).ToList();

            _context.Subjects.AddRange(subjects);
            await _context.SaveChangesAsync();
        }

        public async Task AddGrade(int count)
        {
            var random = new Random();

            var grades = Enumerable.Range(1, count).Select(r => new Grade
            {
                Value = random.Next(1, 5),
                StudentId = random.Next(1, _context.Students.Count() + 1),
                SubjectId = random.Next(1, _context.Subjects.Count() + 1)
            }).ToList();

            _context.Grades.AddRange(grades);
            await _context.SaveChangesAsync();
        }
    }
}
