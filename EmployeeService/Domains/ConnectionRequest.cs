﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Domains
{
    public class ConnectionRequest
    {
        [Key]
        public Guid ReceiverId { get; set; }
        public Guid SenderId { get; set; }
        public string? RequestNotification { get; set; }
    }
}
