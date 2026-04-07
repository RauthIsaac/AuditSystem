using Audit_System.Domain.Interfaces;
using AuditSystem.Application.Features.User.DTOs;
using AuditSystem.Application.Features.User.Queries;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using AuditSystem.Application.Wrappers;

namespace AuditSystem.Application.Features.User.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, RequestResponse<IEnumerable<UserDto>>>
    {
        /*------------------------------------------------------------------*/
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            if (users == null || !users.Any())
            {
                return RequestResponse<IEnumerable<UserDto>>.Fail("No users found.");
            }

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return RequestResponse<IEnumerable<UserDto>>.Success(usersDto, "Users retrieved successfully.");
        }
        /*------------------------------------------------------------------*/
    }
}
