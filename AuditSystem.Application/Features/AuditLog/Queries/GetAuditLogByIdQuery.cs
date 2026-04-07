using AuditSystem.Application.Features.AuditLog.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.AuditLog.Queries
{
    public record GetAuditLogByIdQuery : IRequest<RequestResponse<AuditLogDto>>
    {
        public GetAuditLogByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; init; }
    };
}
