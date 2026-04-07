using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Interfaces;
using AuditSystem.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        private ICourseRepository _courses;
        private IUserRepository _users;
        private IEnrollmentRepository _enrollments;
        private IAuditLogRepository _auditLogs;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public ICourseRepository Courses => _courses ??= new CourseRepository(_context);
        public IUserRepository Users => _users ??= new UserRepository(_context);
        public IEnrollmentRepository Enrollments => _enrollments ??= new EnrollmentRepository(_context);
        public IAuditLogRepository AuditLogs => _auditLogs ??= new AuditLogRepository(_context);
        /*------------------------------------------------------------------*/
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        /*------------------------------------------------------------------*/
        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }
        /*------------------------------------------------------------------*/
        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }
        /*------------------------------------------------------------------*/
        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        /*------------------------------------------------------------------*/
        public void Dispose()
        {
            _context.Dispose();
        }
        /*------------------------------------------------------------------*/
    }
}
