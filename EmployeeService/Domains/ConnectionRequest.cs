namespace EmployeeService.Domains
{
    public class ConnectionRequest
    {
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public string? RequestNotification { get; set; }
    }
}
