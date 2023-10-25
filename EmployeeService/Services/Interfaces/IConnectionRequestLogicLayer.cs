namespace EmployeeService.Services.Interfaces
{
    public interface IConnectionRequestLogicLayer
    {
        Task<IEnumerable<int>> GetConnectionRequestList(int Id);
        Task SendConnectionRequest(int employeeId, int requestId);
    }
}
