using Microsoft.EntityFrameworkCore;
using StudentService.DAL.Contracts.Repositories;
using StudentService.DAL.EF.Context;
using StudentService.DAL.Model.Abstract;

namespace StudentService.DAL.EF.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ItemBase
    {
        private readonly StudentDbContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepository(StudentDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task CreateAsync(T item)
        {
            _dbSet.Add(item);
            await SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T item)
        {
            _dbSet.Entry(item).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T item)
        {
            _dbSet.Remove(item);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbex)
            {
                throw new Exception($"Ошибка обновления базы данных: {dbex.InnerException?.Message ?? dbex.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
