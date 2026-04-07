using AuditSystem.Application.Features.Course.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.Course.Queries
{
    public record GetAllCoursesQuery : IRequest<RequestResponse<IEnumerable<CourseDto>>>;
}
