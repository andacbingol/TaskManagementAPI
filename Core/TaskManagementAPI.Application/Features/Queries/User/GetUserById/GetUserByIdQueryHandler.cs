using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
using TaskManagementAPI.Application.Features.Rules.User;
using TaskManagementAPI.Domain.Constants;

namespace TaskManagementAPI.Application.Features.Queries.User.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
{
    private readonly IUserService _userService;
    private readonly GetUserByIdRules _getUserByIdRules;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;

    public GetUserByIdQueryHandler(IUserService userService, IMapper mapper, GetUserByIdRules getUserByIdRules, IUserClaimService userClaimService)
    {
        _userService = userService;
        _mapper = mapper;
        _getUserByIdRules = getUserByIdRules;
        _userClaimService = userClaimService;
    }

    public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        await _getUserByIdRules.CheckAdminOrAccountOwner(claimUserId, request.Id);

        bool isAdmin = await _userService.IsInRoleAsync(claimUserId, Roles.Admin);

        UserDTO? userDTO = await _userService.GetUserByIdAsync(request.Id);
        
        if (userDTO is null)
            return null;

        if (isAdmin)
            return _mapper.Map<GetUserByIdQueryResponse>(userDTO);
        else
        {
            LimitedUserDTO limitedUserDTO = _mapper.Map<LimitedUserDTO>(userDTO);
            return _mapper.Map<GetUserByIdQueryResponse>(limitedUserDTO);
        }

    }
}
