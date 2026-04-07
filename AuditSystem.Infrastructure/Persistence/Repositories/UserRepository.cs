using Audit_System.Domain.Entities;
using Audit_System.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            if(users == null || !users.Any())
            {
                throw new Exception("No users found.");
            }
            return users;
        }
        /*------------------------------------------------------------------*/
        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new Exception($"User with id {id} not found.");
            }
            return user;
        }
        /*------------------------------------------------------------------*/
        public Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------*/
        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        /*------------------------------------------------------------------*/
        public async Task DeleteAsync(Guid id)
        {
        }
        /*------------------------------------------------------------------*/
        
    }
}
