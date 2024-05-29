using EmployeeService.Enums;
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
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
    }
}
