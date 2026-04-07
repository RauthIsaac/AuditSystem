using Audit_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(Guid id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(Guid id);
    }
}
