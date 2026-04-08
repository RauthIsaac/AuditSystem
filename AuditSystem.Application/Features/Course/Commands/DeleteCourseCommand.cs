using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Course.Commands
{
    public record DeleteCourseCommand : IRequest<RequestResponse<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DeleteCourseCommand(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
