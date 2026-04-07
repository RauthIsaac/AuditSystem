using Audit_System.Domain.Entities;
using AuditSystem.Application;
using AuditSystem.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.BackgroundServices
{
    public class AuditBackgroundWorker : BackgroundService
    {
        private readonly IAuditQueue _queue;
        private readonly IServiceScopeFactory _scopeFactory;

        public AuditBackgroundWorker(IAuditQueue queue, IServiceScopeFactory scopeFactory)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var auditEvent = await _queue.DequeueAsync(stoppingToken);

                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var log = new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = auditEvent.UserId,
                    Action = auditEvent.Action,
                    EntityName = auditEvent.EntityName,
                    EntityId = auditEvent.EntityId,
                    Timestamp = DateTime.UtcNow,
                    Metadata = auditEvent.Metadata
                };

                dbContext.AuditLogs.Add(log);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
        }
    }
}
