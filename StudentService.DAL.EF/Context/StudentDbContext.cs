using Microsoft.EntityFrameworkCore;
using StudentService.DAL.Model;

namespace StudentService.DAL.EF.Context
{
    public class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; } 

        public StudentDbContext(DbContextOptions<StudentDbContext> options)
            : base(options) { }

    }
}
