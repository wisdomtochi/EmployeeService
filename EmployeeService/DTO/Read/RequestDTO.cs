using System.Text.Json.Serialization;

namespace EmployeeService.DTO.Read
{
    public class RequestDTO
    {
        [JsonPropertyName("receiver_id")]
        public Guid ReceiverId { get; set; }
        [JsonPropertyName("sender_id")]
        public Guid SenderId { get; set; }
    }
}
