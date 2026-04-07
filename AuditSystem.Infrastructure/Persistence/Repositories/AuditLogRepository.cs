using Audit_System.Domain.Entities;
using Audit_System.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        public AuditLogRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            var auditLogs = await _context.AuditLogs.ToListAsync();

            if(auditLogs == null || !auditLogs.Any())
            {
                throw new Exception("No audit logs found.");
            }
            return auditLogs;   
        }
        /*------------------------------------------------------------------*/
        public async Task<AuditLog?> GetByIdAsync(Guid id)
        {
            var auditLog = await _context.AuditLogs.FirstOrDefaultAsync(a => a.Id == id);
            if (auditLog == null)
            {
                throw new Exception($"Audit log with ID {id} not found.");
            }
            return auditLog;
        }
        /*------------------------------------------------------------------*/
        public Task AddAsync(AuditLog auditLog)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------*/
        public Task UpdateAsync(AuditLog auditLog)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------*/
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------*/
    }
}
