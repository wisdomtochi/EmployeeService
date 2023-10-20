﻿namespace EmployeeService.Domains
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public int Salary { get; set; }
        public List<Connection>? Connections { get; set; } = new();
    }
}
