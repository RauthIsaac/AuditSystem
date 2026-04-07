using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Features.Enrollments.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.Enrollments.Handlers
{
    public class GetEnrollmentByIdQueryHandler : IRequestHandler<GetEnrollmentByIdQuery, RequestResponse<EnrollmentDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public GetEnrollmentByIdQueryHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<EnrollmentDto>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id);
            if (enrollment == null)
            {
                return RequestResponse<EnrollmentDto>.Fail("Enrollment not found.");
            }
            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollment);
            return RequestResponse<EnrollmentDto>.Success(enrollmentDto);
        }
        /*------------------------------------------------------------------*/
    }
}
