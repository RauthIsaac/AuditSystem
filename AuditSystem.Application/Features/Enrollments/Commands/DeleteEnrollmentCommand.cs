using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Commands
{
    public class DeleteEnrollmentCommand : IRequest<RequestResponse<bool>>
    {
        public Guid Id;
        public DeleteEnrollmentCommand(Guid id)
        {
            Id = id;
        }
    }
}
