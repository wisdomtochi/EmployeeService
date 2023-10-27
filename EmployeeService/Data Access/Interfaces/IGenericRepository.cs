namespace EmployeeService.Data_Access.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> ReadSingle(int id);
        Task<IEnumerable<T>> ReadAll();
        Task<T> Create(T entity);
        Task Delete(int id);
        void Update(T entity);
        Task<bool> SaveChanges();
    }
}
