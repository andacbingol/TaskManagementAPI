using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.Features.Exceptions.AppInternalExceptions;
using TaskManagementAPI.Application.Features.Rules.User;

namespace TaskManagementAPI.Application.Features.Commands.User.UpdatePassword;

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, Unit>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IUserClaimService _userClaimService;
    private readonly UpdatePasswordRules _updatePasswordRules;

    public UpdatePasswordCommandHandler(IUserService userService, IMapper mapper, UpdatePasswordRules updatePasswordRules, IUserClaimService userClaimService)
    {
        _userService = userService;
        _mapper = mapper;
        _updatePasswordRules = updatePasswordRules;
        _userClaimService = userClaimService;
    }

    public async Task<Unit> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
    {
        Guid claimUserId = _userClaimService.GetClaimUserId();

        UpdatePasswordDTO updatePasswordDTO = _mapper.Map<UpdatePasswordDTO>(request);
        updatePasswordDTO.Id = claimUserId;

        await _updatePasswordRules.PasswordIncorrectAsync(claimUserId, request.Password);
        _updatePasswordRules.PasswordConfirmIncorrect(request.PasswordNew, request.PasswordNewConfirm);

        await _userService.UpdatePasswordAsync(updatePasswordDTO);
        return Unit.Value;
    }
}
