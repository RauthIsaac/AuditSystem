using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.User.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
    }
}
