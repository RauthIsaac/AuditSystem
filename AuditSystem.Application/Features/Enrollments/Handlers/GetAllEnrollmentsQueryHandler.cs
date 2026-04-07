using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Features.Enrollments.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YourProject.Application.Wrappers;

namespace AuditSystem.Application.Features.Enrollments.Handlers
{
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, RequestResponse<IEnumerable<EnrollmentDto>>>
    {
        /*------------------------------------------------------------------*/
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;
        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<IEnumerable<EnrollmentDto>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _enrollmentRepository.GetAllAsync();

            if (enrollments == null || !enrollments.Any())
            {
                return RequestResponse<IEnumerable<EnrollmentDto>>.Fail("No enrollments found.");
            }

            var enrollmentDtos = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
            return RequestResponse<IEnumerable<EnrollmentDto>>.Success(enrollmentDtos);
        }
        /*------------------------------------------------------------------*/
    }
}
