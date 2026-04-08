using Audit_System.Domain.Events;
using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.Enrollments.Commands;
using AuditSystem.Application.Features.Enrollments.DTOs;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Handlers
{
    public class UpdateEnrollmentPaymentStatusCommandHandler : IRequestHandler<UpdateEnrollmentPaymentStatusCommand, RequestResponse<EnrollmentDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditQueue _auditQueue;
        public UpdateEnrollmentPaymentStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditQueue = auditQueue;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<EnrollmentDto>> Handle(UpdateEnrollmentPaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var enrollmentEntity = await _unitOfWork.Enrollments.GetByIdAsync(request.Id);

            if (enrollmentEntity == null)
            {
                return RequestResponse<EnrollmentDto>.Fail("Enrollment not found");
            }

            enrollmentEntity.IsPaid = request.IsPaid;

            await _unitOfWork.Enrollments.UpdateAsync(enrollmentEntity);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (result >= 0)
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = request.UserId,
                    Action = "UpdateEnrollmentStatus",
                    EntityName = "Enrollment",
                    EntityId = enrollmentEntity.Id.ToString(),
                    Metadata = $"Payment status updated to: {request.IsPaid}"
                });

                var responseData = _mapper.Map<EnrollmentDto>(enrollmentEntity);
                return RequestResponse<EnrollmentDto>.Success(responseData, "Enrollment Status updated successfully");
            }

            return RequestResponse<EnrollmentDto>.Fail("Failed to update enrollment status");
        }
        /*------------------------------------------------------------------*/
    }
}
