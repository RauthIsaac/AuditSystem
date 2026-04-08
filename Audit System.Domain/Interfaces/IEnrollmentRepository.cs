using Audit_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment?> GetByIdAsync(Guid id);
        Task<IEnumerable<Enrollment>> GetUserEnrollmentsAsync(Guid userId);
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(Guid id);
    }
}
