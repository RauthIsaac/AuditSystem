using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Entities
{
    public class User 
    { 
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }

        /*------------------------------------------------------------------*/
        /*----------------------Navigation Properties-----------------------*/
        public ICollection<Enrollment>? Enrollments { get; set; }
        /*------------------------------------------------------------------*/
    }
}
