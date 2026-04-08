using Audit_System.Domain.Events;
using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Auth.Commands;
using AuditSystem.Application.Features.Auth.DTOs;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Wrappers;
using MediatR;

namespace AuditSystem.Application.Features.Auth.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RequestResponse<LoginResponseDto>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAuditQueue _auditQueue;
        public RegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService, IAuditQueue auditQueue)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
            _auditQueue = auditQueue;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<LoginResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = Guid.Empty,
                    Action = "REGISTER_FAILED",
                    EntityName = "User",
                    EntityId = request.Email,
                    Metadata = $"Registration failed - email already exists: {request.Email}"
                });
                
                return RequestResponse<LoginResponseDto>.Fail("Email already exists");
            }
               

            var user = new Audit_System.Domain.Entities.User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), 
                Role = "User"
            };

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _auditQueue.QueueEvent(new AuditEvent
            {
                UserId = user.Id,
                Action = "REGISTER",
                EntityName = "User",
                EntityId = user.Id.ToString(),
                Metadata = $"New user registered: {user.Email}"
            });

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email!, user.Role);

            return RequestResponse<LoginResponseDto>.Success(
                new LoginResponseDto
                {
                    Token = token,
                    Email = user.Email
                }
            , "Enrollment deleted successfully");
        }
        /*------------------------------------------------------------------*/
    }
}