using AuditSystem.Application.Features.User.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using YourProject.Application.Wrappers;

namespace AuditSystem.Application.Features.User.Queries
{
    public record GetUserByIdQuery : IRequest<RequestResponse<UserDto>>
    {
        public Guid Id { get; set; }
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
