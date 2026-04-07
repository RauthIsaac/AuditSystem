using AuditSystem.Application.Features.Enrollments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YourProject.Application.Wrappers;

namespace AuditSystem.Application.Features.Enrollments.Queries
{
    public record GetAllEnrollmentsQuery : IRequest<RequestResponse<IEnumerable<EnrollmentDto>>>;
}
