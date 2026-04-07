using Audit_System.Domain.Events;
using AuditSystem.Application.Features.Course.Commands;
using AuditSystem.Application.Features.Enrollments.Commands;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Enrollments.Handlers
{
    public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, RequestResponse<bool>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditQueue _auditQueue;
        public DeleteEnrollmentCommandHandler(IUnitOfWork unitOfWork, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _auditQueue = auditQueue;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<bool>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(request.Id);

            if (enrollment == null)
            {
                return RequestResponse<bool>.Fail($"Enrollment with ID: {request.Id} not found.");
            }
            await _unitOfWork.Enrollments.DeleteAsync(enrollment.Id);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = enrollment.UserId, 
                    Action = "DeleteEnrollment",
                    EntityName = "Enrollment",
                    EntityId = enrollment.Id.ToString(),
                    Metadata = $"Enrollment for Course ID: {enrollment.CourseId} has been deleted."
                });

                return RequestResponse<bool>.Success(true, "Enrollment deleted successfully");
            }

            return RequestResponse<bool>.Fail("Failed to delete enrollment");
        }
        /*------------------------------------------------------------------*/
    }
}
