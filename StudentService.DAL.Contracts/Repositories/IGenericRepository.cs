using StudentService.DAL.Model.Abstract;
using System.Linq.Expressions;

namespace StudentService.DAL.Contracts.Repositories
{
    public interface IGenericRepository<T> where T : ItemBase
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task CreateAsync(T item);

        Task UpdateAsync(T item);

        Task DeleteAsync(T item);
    }
}