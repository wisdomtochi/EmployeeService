using System.Text.Json.Serialization;

namespace EmployeeService.DTO.Read
{
    public class ConnectionDTO
    {
        [JsonPropertyName("employee_id")]
        public Guid EmployeeId { get; set; }
        [JsonPropertyName("friend_id")]
        public Guid FriendId { get; set; }
    }
}
