using Audit_System.Domain.Events;
using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Auth.Commands;
using AuditSystem.Application.Features.Auth.DTOs;
using AuditSystem.Application.Interfaces;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, RequestResponse<LoginResponseDto?>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuditQueue _auditQueue;

        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService, IUnitOfWork unitOfWork, IAuditQueue auditQueue )
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _unitOfWork = unitOfWork;
            _auditQueue = auditQueue;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<LoginResponseDto?>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                _auditQueue.QueueEvent(new AuditEvent
                {
                    UserId = Guid.Empty,
                    Action = "LOGIN_FAILED",
                    EntityName = "User",
                    EntityId = request.Email,
                    Metadata = $"Failed login attempt for email: {request.Email}"
                });

                return RequestResponse<LoginResponseDto?>.Fail("Invalid email or password");
            }

            _auditQueue.QueueEvent(new AuditEvent
            {
                UserId = user.Id,
                Action = "LOGIN",
                EntityName = "User",
                EntityId = user.Id.ToString(),
                Metadata = $"User {user.Email} logged in successfully"
            });

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email!, user.Role);

            return RequestResponse<LoginResponseDto?>.Success(new LoginResponseDto
            {
                Token = token,
                Email = user.Email!
            });
        }
        /*------------------------------------------------------------------*/
    }
}
