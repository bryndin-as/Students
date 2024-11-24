namespace StudentService.DAL.DTO
{
    [Serializable]
    public record SubjectBaseDTO
    {
        public int Id { get; init; }

        public string Name { get; init; }
    }

    [Serializable]
    public record SubjectCreateDTO
    {
        public string Name { get; init; }
    }
}