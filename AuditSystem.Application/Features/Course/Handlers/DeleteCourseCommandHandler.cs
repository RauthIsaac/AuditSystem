using Audit_System.Domain.Events;
using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Course.Commands;
using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Course.Handlers
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, RequestResponse<bool>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditQueue _auditQueue;
        public DeleteCourseCommandHandler(IUnitOfWork unitOfWork, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _auditQueue = auditQueue;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<bool>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(request.Id);
            if (course == null)
            {
                return RequestResponse<bool>.Fail($"Course with ID: {request.Id} not found.");
            }

            await _unitOfWork.Courses.DeleteAsync(course.Id);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
            if (result > 0)
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = Guid.Empty,
                    Action = "DeleteCourse",
                    EntityName = "Course",
                    EntityId = course.Id.ToString(),
                    Metadata = $"Admin has deleted the course : {course.Title}"
                });

                return RequestResponse<bool>.Success(true, "Course deleted successfully");
            }

            return RequestResponse<bool>.Fail("Failed to delete course");

        }
        /*------------------------------------------------------------------*/
    }
}
