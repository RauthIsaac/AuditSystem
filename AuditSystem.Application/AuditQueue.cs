using Audit_System.Domain.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application
{
    public interface IAuditQueue
    {
        void QueueEvent(AuditEvent auditEvent);
        Task<AuditEvent> DequeueAsync(CancellationToken cancellationToken);
    }

    public class AuditQueue : IAuditQueue
    {
        private readonly ConcurrentQueue<AuditEvent> _events = new();
        private readonly SemaphoreSlim _signal = new(0);

        public void QueueEvent(AuditEvent auditEvent)
        {
            _events.Enqueue(auditEvent);
            _signal.Release(); 
        }

        public async Task<AuditEvent> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _events.TryDequeue(out var auditEvent);
            return auditEvent!;
        }
    }
}
