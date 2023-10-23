namespace EmployeeService.Domains
{
    public class Connection
    {
        public int Id { get; set; }
        public List<Employee>? Employees { get; set; } = new();
        public string? ConnectedNotification { get; set; }
    }
}
