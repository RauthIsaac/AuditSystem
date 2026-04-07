using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }

        /*------------------------------------------------------------------*/
        /*----------------------Navigation Properties-----------------------*/
        public ICollection<Enrollment>? Enrollments { get; set; }
        /*------------------------------------------------------------------*/
    }
}
