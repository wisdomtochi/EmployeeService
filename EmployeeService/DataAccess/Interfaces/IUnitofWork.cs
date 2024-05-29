namespace EmployeeService.DataAccess.Interfaces
{
    public interface IUnitofWork<T> where T : class
    {
        IGenericRepository<T> Repository { get; }
        Task<bool> SaveChangesAsync();
    }
}
