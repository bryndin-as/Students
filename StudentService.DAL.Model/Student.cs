using StudentService.DAL.Model.Abstract;

namespace StudentService.DAL.Model
{
    public class Student : ItemBase
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;

        public ICollection<Grade> Grades { get; set; } = new List<Grade>();
    }
}
