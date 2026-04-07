using AuditSystem.Application.Features.Enrollments.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.Enrollments.Queries
{
    public record GetEnrollmentByIdQuery : IRequest<RequestResponse<EnrollmentDto>>
    {
        public Guid Id;
        public GetEnrollmentByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
