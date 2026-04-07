using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Entities
{
    public class Enrollment
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsPaid { get; set; }

        /*------------------------------------------------------------------*/
        /*----------------------Navigation Properties-----------------------*/
        public Guid UserId { get; set; }
        public User? User { get; set; }
        /*------------------------------------------------------------------*/
        public Guid CourseId { get; set; }
        public Course? Course { get; set; }
        /*------------------------------------------------------------------*/
    }
}
