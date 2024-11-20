using StudentService.DAL.Model.Abstract;

namespace StudentService.DAL.Contracts.Repositories
{
    public interface ITestRepository 
    {
        Task SeedTestDataAsync(int count); 
    }
}
