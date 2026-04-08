using AuditSystem.Application.Features.Auth.DTOs;
using AuditSystem.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuditSystem.Application.Features.Auth.Commands
{
    public record RegisterCommand(string Name, string Email, string Password): IRequest<RequestResponse<LoginResponseDto>>;
}
