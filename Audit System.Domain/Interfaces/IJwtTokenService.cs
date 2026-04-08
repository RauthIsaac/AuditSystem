using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
