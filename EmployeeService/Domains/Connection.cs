using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domains
{
    public class Connection
    {
        [Key]
        public Guid EmployeeId { get; set; }
        [Key]
        public Guid FriendId { get; set; }
    }
}
