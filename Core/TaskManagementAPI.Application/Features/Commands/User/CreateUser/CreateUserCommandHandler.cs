using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.User;
using TaskManagementAPI.Application.Features.Rules.User;

namespace TaskManagementAPI.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly CreateUserRules _createUserRules;

    public CreateUserCommandHandler(IUserService userService, IMapper mapper, CreateUserRules createUserRules, IAuthService authService)
    {
        _userService = userService;
        _mapper = mapper;
        _createUserRules = createUserRules;
        _authService = authService;
    }

    public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
    {
        await _createUserRules.EmailAlreadyExist(request.Email);
        await _createUserRules.UserNameAlreadyExist(request.Username);
        CreateUserDTO createUserDTO = _mapper.Map<CreateUserDTO>(request);

        CreateUserResultDTO createUserResultDTO = await _userService.CreateUserAsync(createUserDTO);

        await _userService.AssignDefaultRoleAsync(createUserDTO);

        await _authService.GenerateEmailConfirmationTokenAsync(request.Email);
        
        return _mapper.Map<CreateUserCommandResponse>(createUserResultDTO);
    }
}
