using System.ComponentModel.DataAnnotations;

namespace StudentService.DAL.DTO
{
    [Serializable]
    public record GradeBaseDTO
    {
        [Range(1, 5, ErrorMessage = "Value must be between 1 and 5.")]
        public int Value { get; init; }  

        public int StudentId { get; init; } 

        public int SubjectId { get; init; }
    }

    [Serializable]
    public record GradeCreateDTO : GradeBaseDTO
    { }

}