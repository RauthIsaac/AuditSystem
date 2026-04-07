using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Course.Commands
{
    public class UpdateCourseCommand : IRequest<RequestResponse<CourseDto>>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<EnrollmentDto> Enrollments { get; set; }
    }
}
