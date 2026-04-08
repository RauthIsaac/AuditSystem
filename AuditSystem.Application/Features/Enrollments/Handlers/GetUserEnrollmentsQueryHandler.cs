using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Features.Enrollments.Queries;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Wrappers;
using AutoMapper;
using MediatR;

namespace AuditSystem.Application.Features.Enrollments.Handlers
{
    public class GetUserEnrollmentsQueryHandler
        : IRequestHandler<GetUserEnrollmentsQuery, RequestResponse<IEnumerable<EnrollmentDto>>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUserEnrollmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<IEnumerable<EnrollmentDto>>> Handle(
            GetUserEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var enrollments = await _unitOfWork.Enrollments
                .GetUserEnrollmentsAsync(request.UserId);

            if (!enrollments.Any())
                return RequestResponse<IEnumerable<EnrollmentDto>>.Fail("No enrollments found");

            var responseData = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
            return RequestResponse<IEnumerable<EnrollmentDto>>.Success(responseData);
        }
        /*------------------------------------------------------------------*/
    }
}