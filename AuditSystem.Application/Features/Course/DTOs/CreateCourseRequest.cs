using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Course.DTOs
{
    public class CreateCourseRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
