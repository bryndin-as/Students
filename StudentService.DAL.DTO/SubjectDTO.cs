namespace StudentService.DAL.DTO
{
    [Serializable]
    public class SubjectBaseDTO
    {
        public string Name { get; set; } 
    }

    [Serializable]
    public class SubjectCreateDTO : SubjectBaseDTO
    { }

    [Serializable]
    public class SubjectUpdateDTO : SubjectBaseDTO
    {
        public int Id { get; set; }
    }
}
