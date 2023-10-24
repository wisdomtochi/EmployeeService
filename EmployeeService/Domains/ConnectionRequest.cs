using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domains
{
    public class ConnectionRequest
    {
        [Key]
        public int ReceiverId { get; set; }
        public int SenderId { get; set; }
        public string? RequestNotification { get; set; }
    }
}
