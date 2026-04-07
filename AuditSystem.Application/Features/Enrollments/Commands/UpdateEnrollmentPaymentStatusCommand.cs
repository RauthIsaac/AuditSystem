using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Commands
{
    public record UpdateEnrollmentPaymentStatusCommand : IRequest<RequestResponse<EnrollmentDto>>
    {
        public Guid Id;
        public bool IsPaid;
        public UpdateEnrollmentPaymentStatusCommand(Guid id, bool isPaid)
        {
            Id = id;
            IsPaid = isPaid;
        }
    }
}
