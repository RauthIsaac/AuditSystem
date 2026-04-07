using AuditSystem.Application.Features.AuditLog.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;


namespace AuditSystem.Application.Features.AuditLog.Queries
{
    public record GetAllAuditsLogQuery : IRequest<RequestResponse<IEnumerable<AuditLogDto>>>;
}
