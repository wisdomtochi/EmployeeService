using System.Text.Json.Serialization;

namespace EmployeeService.DTO.Read
{
    public class EmployeeDTO
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
    }
}
