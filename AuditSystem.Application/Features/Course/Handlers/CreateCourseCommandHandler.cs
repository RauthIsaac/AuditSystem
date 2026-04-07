using Audit_System.Domain.Entities;
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
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, RequestResponse<CourseDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditQueue _auditQueue;
        public CreateCourseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditQueue = auditQueue;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<CourseDto>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var courseEntity = _mapper.Map<Audit_System.Domain.Entities.Course>(request);
            await _unitOfWork.Courses.AddAsync(courseEntity);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result > 0)
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = Guid.Empty,
                    Action = "CreateCourse",
                    EntityName = "Course",
                    EntityId = courseEntity.Id.ToString(),
                    Metadata = $"Admin has created a course : {courseEntity.Title}"
                });

                var responseData = _mapper.Map<CourseDto>(courseEntity);
                return RequestResponse<CourseDto>.Success(responseData);
            }
            return RequestResponse<CourseDto>.Fail("Failed to create course");
        }
        /*------------------------------------------------------------------*/
    }
}
