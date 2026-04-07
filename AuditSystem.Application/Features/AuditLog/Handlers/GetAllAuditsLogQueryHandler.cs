using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.AuditLog.DTOs;
using AuditSystem.Application.Features.AuditLog.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.AuditLog.Handlers
{
    public class GetAllAuditsLogQueryHandler : IRequestHandler<GetAllAuditsLogQuery, RequestResponse<IEnumerable<AuditLogDto>>>
    {
        /*------------------------------------------------------------------*/
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper _mapper;
        public GetAllAuditsLogQueryHandler(IAuditLogRepository auditLogRepository, IMapper mapper)
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<IEnumerable<AuditLogDto>>> Handle(GetAllAuditsLogQuery request, CancellationToken cancellationToken)
        {
            var auditLogs = await  _auditLogRepository.GetAllAsync();
            if(auditLogs == null || !auditLogs.Any())
            {
                return RequestResponse<IEnumerable<AuditLogDto>>.Fail("No audit logs found.");
            }
            var auditLogDtos = _mapper.Map<IEnumerable<AuditLogDto>>(auditLogs);
            return RequestResponse<IEnumerable<AuditLogDto>>.Success(auditLogDtos, "Audit logs retrieved successfully.");
        }
        /*------------------------------------------------------------------*/
    }
}
