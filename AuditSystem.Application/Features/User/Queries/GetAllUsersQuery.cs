using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using AuditSystem.Application.Features.User.DTOs;
using MediatR;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.User.Queries
{
    public record GetAllUsersQuery : IRequest<RequestResponse<IEnumerable<UserDto>>>;
}
