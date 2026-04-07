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
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, RequestResponse<UserDto>>
    {
        /*------------------------------------------------------------------*/
        public readonly IUserRepository _userRepository;
        public readonly IMapper _mapper;
        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        /*------------------------------------------------------------------*/
        public async Task<RequestResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if(user == null)
            {
                return RequestResponse<UserDto>.Fail("User not found");
            }
            var userDto = _mapper.Map<UserDto>(user);
            return RequestResponse<UserDto>.Success(userDto, "User retrieved successfully");
        }
        /*------------------------------------------------------------------*/
    }
}
