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
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, RequestResponse<CourseDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditQueue _auditQueue;
        public UpdateCourseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditQueue = auditQueue;
        }

        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<CourseDto>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseEntity = await _unitOfWork.Courses.GetByIdAsync(request.Id);

            if (courseEntity == null)
            {
                return RequestResponse<CourseDto>.Fail("Course not found");
            }

            _mapper.Map(request, courseEntity);

            await _unitOfWork.Courses.UpdateAsync(courseEntity);

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = Guid.Empty,
                    Action = "CreateCourse",
                    EntityName = "Course",
                    EntityId = courseEntity.Id.ToString(),
                    Metadata = $"Admin has updated a course : {courseEntity.Title}"
                });

                var responseData = _mapper.Map<CourseDto>(courseEntity);
                return RequestResponse<CourseDto>.Success(responseData, "Course updated successfully");
            }

            return RequestResponse<CourseDto>.Fail("Failed to update course");

        }
        /*------------------------------------------------------------------*/
    }
}
