using Audit_System.Domain.Entities;
using Audit_System.Domain.Events;
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
    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, RequestResponse<EnrollmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditQueue _auditQueue;

        public CreateEnrollmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditQueue = auditQueue;
        }

        public async Task<RequestResponse<EnrollmentDto>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(request.CourseId);
            if (course == null) return RequestResponse<EnrollmentDto>.Fail("Course not found");

            var enrollment = new Enrollment
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId, 
                CourseId = request.CourseId,
                Timestamp = DateTime.UtcNow,
                IsPaid = false 
            };

            await _unitOfWork.Enrollments.AddAsync(enrollment);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                var savedEnrollment = await _unitOfWork.Enrollments.GetByIdAsync(enrollment.Id);

                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = enrollment.UserId,
                    Action = "EnrollCourse", 
                    EntityName = "Enrollment", 
                    EntityId = enrollment.Id.ToString(),  
                    Metadata = $"User enrolled in course: {course.Title}"  
                });

                var responseData = _mapper.Map<EnrollmentDto>(savedEnrollment);
                return RequestResponse<EnrollmentDto>.Success(responseData, "Enrolled successfully");
            }

            return RequestResponse<EnrollmentDto>.Fail("Enrollment failed");
        }
    }
}
