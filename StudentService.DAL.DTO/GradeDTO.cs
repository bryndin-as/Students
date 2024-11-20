using System.ComponentModel.DataAnnotations;

namespace StudentService.DAL.DTO
{
    [Serializable]
    public class GradeBaseDTO 
    {
        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5.")]
        public int Value { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }
    }

    [Serializable]
    public class GradeCreateDTO : GradeBaseDTO
    { }

    [Serializable]
    public class GradeUpdateDTO : GradeBaseDTO
    {
        public int Id { get; set; }
    }
}
