using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Auth.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
