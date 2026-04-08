using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.DTOs
{
    public class CreateEnrollmentRequest
    {
        public Guid CourseId { get; set; }
    }
}
