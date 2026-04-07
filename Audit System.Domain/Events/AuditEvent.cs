using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Events
{
    public class AuditEvent
    {
        public Guid UserId { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }
        public string? Metadata { get; set; }
    }
}
