using System;
using System.Collections.Generic;
using System.Text;

namespace Audit_System.Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Action { get; set; }
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Metadata { get; set; }

    }
}
