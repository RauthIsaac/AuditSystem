using Audit_System.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        IEnrollmentRepository Enrollments { get; }
        IAuditLogRepository AuditLogs { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
