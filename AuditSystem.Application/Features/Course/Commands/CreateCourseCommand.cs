using AuditSystem.Application.Features.Course.DTOs;
using MediatR;
using AuditSystem.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;


namespace AuditSystem.Application.Features.Course.Commands
{
    public record CreateCourseCommand : IRequest<RequestResponse<CourseDto>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }
}
