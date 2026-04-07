using AuditSystem.Application.Features.Enrollments.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Course.DTOs
{
    public class EnrollmentCourseDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
    }
}
