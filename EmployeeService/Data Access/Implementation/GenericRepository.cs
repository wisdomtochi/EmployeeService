using EmployeeService.Data;
using EmployeeService.Data_Access.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data_Access.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(EmployeeDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> ReadSingle(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ReadAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            var updateEntity = _dbSet.Attach(entity);
            updateEntity.State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync() >= 0;
        }
    }
}
