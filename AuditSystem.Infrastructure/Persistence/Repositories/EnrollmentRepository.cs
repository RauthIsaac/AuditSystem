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
        public async Task<IEnumerable<Enrollment>> GetUserEnrollmentsAsync(Guid userId)
        {
            var enrollments =  await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ToListAsync();
            if( enrollments != null )
            {
                return enrollments;
            }
            return null;
        }
        /*------------------------------------------------------------------*/
        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
        }
        /*------------------------------------------------------------------*/
        public async Task UpdateAsync(Enrollment enrollment)
        {
            _context.Entry(enrollment).State = EntityState.Modified;
        }
        /*------------------------------------------------------------------*/
        public async Task DeleteAsync(Guid id)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(c => c.Id == id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
        }
        /*------------------------------------------------------------------*/
    }
}
