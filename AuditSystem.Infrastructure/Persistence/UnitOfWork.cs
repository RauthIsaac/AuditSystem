using AuditSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
    
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public void Dispose()
        {
            _context.Dispose();
        }
        /*------------------------------------------------------------------*/
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
        /*------------------------------------------------------------------*/
    }
}
