using Audit_System.Domain.Entities;
using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Course.DTOs;
using AuditSystem.Application.Features.Course.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.Course.Handlers
{
    public class GetAllCourcesQueryHandler : IRequestHandler<GetAllCoursesQuery, RequestResponse<IEnumerable<CourseDto>>>
    {
        /*------------------------------------------------------------------*/
        readonly ICourseRepository _courseRepository;
        readonly IMapper _mapper;
        public GetAllCourcesQueryHandler(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<IEnumerable<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllAsync();
            if (courses == null || !courses.Any())
            {
                return RequestResponse<IEnumerable<CourseDto>>.Fail("No courses found.");
            }
            var courseDtos = _mapper.Map<IEnumerable<CourseDto>>(courses);
            return RequestResponse<IEnumerable<CourseDto>>.Success(courseDtos);
        }
        /*------------------------------------------------------------------*/
    }
}
