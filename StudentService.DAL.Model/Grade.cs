using StudentService.DAL.Model.Abstract;

namespace StudentService.DAL.Model
{
    public class Grade : ItemBase
    {
        public int Value { get; set; } 

        public int StudentId { get; set; }
        public Student Student { get; set; } = null!; 

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!; 
    }
}
