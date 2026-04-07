using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.User.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.DTOs
{
    public class EnrollmentDto
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public UserDto User { get; set; }
        public EnrollmentCourseDto Course { get; set; }
        public bool IsPaid { get; set; }
    }
}
