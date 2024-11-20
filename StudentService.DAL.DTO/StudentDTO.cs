namespace StudentService.DAL.DTO
{
    [Serializable]
    public class StudentBaseDTO
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }

    [Serializable]
    public class StudentCreateDTO : StudentBaseDTO
    { }

    [Serializable]
    public class StudentUpdateDTO : StudentBaseDTO
    {
        public int Id { get; set; }
    }
}
