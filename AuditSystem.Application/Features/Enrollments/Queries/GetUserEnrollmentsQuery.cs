using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Queries
{
    public record GetUserEnrollmentsQuery(Guid UserId) : IRequest<RequestResponse<IEnumerable<EnrollmentDto>>>;
}
