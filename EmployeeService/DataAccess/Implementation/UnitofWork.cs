using EmployeeService.Data;
using EmployeeService.DataAccess.Interfaces;

namespace EmployeeService.DataAccess.Implementation
{
    public class UnitofWork<T> : IUnitofWork<T> where T : class
    {
        private readonly EmployeeDbContext _dbcontext;
        private IGenericRepository<T> _repository;

        public UnitofWork(EmployeeDbContext context)
        {
            _dbcontext = context;
        }

        public IGenericRepository<T> Repository => _repository ??= new GenericRepository<T>(_dbcontext);

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbcontext.SaveChangesAsync() >= 0;
        }
    }
}
