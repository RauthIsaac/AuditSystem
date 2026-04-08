using AuditSystem.Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Auth.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<LoginResponseDto?>;
}
