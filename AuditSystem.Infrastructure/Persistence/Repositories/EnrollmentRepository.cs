using Audit_System.Domain.Entities;
using Audit_System.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            var enrollments = await _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .ToListAsync();

            if(enrollments == null || !enrollments.Any())
            {
                throw new Exception("No enrollments found.");
            }
            return enrollments;
        }
        /*------------------------------------------------------------------*/
        public async Task<Enrollment?> GetByIdAsync(Guid id)
        {
            var enrollment = await _context.Enrollments
                .Include(e => e.User)
                .Include(e => e.Course)
                .FirstOrDefaultAsync(e => e.Id == id);

            if(enrollment == null)
            {
                throw new Exception($"Enrollment with id {id} not found.");
            }
            return enrollment;
        }
        /*------------------------------------------------------------------*/
        public Task AddAsync(Enrollment enrollment)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------*/
        public Task UpdateAsync(Enrollment enrollment)
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
