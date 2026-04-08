using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.Auth.Commands;
using AuditSystem.Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Auth.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto?>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || user.PasswordHash != request.Password)
                return null;

            var token = _jwtTokenService.GenerateToken(user.Id, user.Email, user.Role);

            return new LoginResponseDto
            {
                Token = token,
                Email = user.Email
            };
        }
    }
}
