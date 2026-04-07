using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Commands
{
    public record CreateEnrollmentCommand : IRequest<RequestResponse<EnrollmentDto>>
    {
        public Guid UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}
