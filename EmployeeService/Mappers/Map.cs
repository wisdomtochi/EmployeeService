using EmployeeService.Domains;
using EmployeeService.DTO.Read;

namespace EmployeeService.Mappers
{
    public static class Map
    {
        public static List<EmployeeDTO> Employees(IEnumerable<Employee> source)
        {
            List<EmployeeDTO> employeeDTOs = source.Select(x => new EmployeeDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                Salary = x.Salary
            }).ToList();

            return employeeDTOs;
        }
    }
}
