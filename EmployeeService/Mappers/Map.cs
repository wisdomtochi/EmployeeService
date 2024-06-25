using EmployeeService.Domains;
using EmployeeService.DTO.Read;
using EmployeeService.Enums;

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
                Gender = EnumsImplementation.GetGender(x.Gender),
                Salary = x.Salary
            }).ToList();

            return employeeDTOs;
        }

        public static List<RequestDTO> Requests(IEnumerable<ConnectionRequest> source)
        {
            List<RequestDTO> requestDTOs = source.Select(x => new RequestDTO()
            {
                ReceiverId = x.ReceiverId,
                SenderId = x.SenderId
            }).ToList();

            return requestDTOs;
        }

        public static List<ConnectionDTO> Connections(IEnumerable<Connection> connections)
        {
            List<ConnectionDTO> connectionDTOs = connections.Select(x => new ConnectionDTO()
            {
                EmployeeId = x.EmployeeId,
                FriendId = x.FriendId
            }).ToList();

            return connectionDTOs;
        }
    }
}
