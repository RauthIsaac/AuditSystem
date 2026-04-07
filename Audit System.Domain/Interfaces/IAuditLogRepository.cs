using Audit_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<AuditLog?> GetByIdAsync(Guid id);
        Task AddAsync(AuditLog auditLog);
        Task UpdateAsync(AuditLog auditLog);
        Task DeleteAsync(Guid id);
    }
}
