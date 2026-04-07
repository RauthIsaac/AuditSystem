using Audit_System.Domain.Entities;
using Audit_System.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = await _context.Courses.ToListAsync();

            if (courses == null || !courses.Any())
            {
                throw new Exception("No courses found.");
            }

            return courses;
        }
        /*------------------------------------------------------------------*/
        public async Task<Course?> GetByIdAsync(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id==id);
            if (course == null)
            {
                throw new Exception($"Course with ID {id} not found.");
            }
            return course;
        }
        /*------------------------------------------------------------------*/
        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
        /*------------------------------------------------------------------*/
        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
        }
        /*------------------------------------------------------------------*/
        public async Task DeleteAsync(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course != null) 
            {
                _context.Courses.Remove(course);
            }
        }
        /*------------------------------------------------------------------*/
    }
}
