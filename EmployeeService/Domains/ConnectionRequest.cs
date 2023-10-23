namespace EmployeeService.Domains
{
    public class ConnectionRequest
    {
        public int Id { get; set; }
        public List<Employee>? Employees { get; set; } = new();
        public string? RequestNotification { get; set; }
    }
}
