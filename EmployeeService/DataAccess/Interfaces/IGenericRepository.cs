namespace EmployeeService.DataAccess.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> ReadSingle(Guid id);
        Task<IEnumerable<T>> ReadAll();
        Task<T> Create(T entity);
        Task Delete(Guid id);
        Task Delete(T entity);
        void Update(T entity);
    }
}
