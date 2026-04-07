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
    public class GetAuditLogByIdQueryHandler : IRequestHandler<GetAuditLogByIdQuery, RequestResponse<AuditLogDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IMapper _mapper;
        public GetAuditLogByIdQueryHandler(IAuditLogRepository auditLogRepository, IMapper mapper   )
        {
            _auditLogRepository = auditLogRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<AuditLogDto>> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken)
        {
            var auditLog = await _auditLogRepository.GetByIdAsync(request.Id);
            if (auditLog == null)
            {
                return RequestResponse<AuditLogDto>.Fail("Audit log not found.");
            }
            var auditLogDto = _mapper.Map<AuditLogDto>(auditLog);
            return RequestResponse<AuditLogDto>.Success(auditLogDto, "Audit log retrieved successfully.");
        }        
        /*------------------------------------------------------------------*/
    }
}
