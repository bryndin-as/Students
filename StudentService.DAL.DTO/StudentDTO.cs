namespace StudentService.DAL.DTO
{
    [Serializable]
    public record StudentBaseDTO
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }
    }

    [Serializable]
    public record StudentCreateDTO
    {
        public string Name { get; init; }

        public string Surname { get; init; }
    }
}