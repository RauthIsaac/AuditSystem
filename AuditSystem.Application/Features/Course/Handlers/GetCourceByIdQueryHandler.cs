using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.Course.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.Course.Handlers
{
    public class GetCourceByIdQueryHandler : IRequestHandler<GetCourceByIdQuery, RequestResponse<CourseDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public GetCourceByIdQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }

        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<CourseDto>> Handle(GetCourceByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null)
            {
                return RequestResponse<CourseDto>.Fail("Course not found");
            }
            var courseDto = _mapper.Map<CourseDto>(course);
            return RequestResponse<CourseDto>.Success(courseDto);
        }
        /*------------------------------------------------------------------*/
    }
}
