using AutoMapper;
using MediatR;
using TaskManagementAPI.Application.Abstractions.Services;
using TaskManagementAPI.Application.DTOs.User;

namespace TaskManagementAPI.Application.Features.Queries.User.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQueryRequest, List<GetUsersQueryResponse>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<List<GetUsersQueryResponse>> Handle(GetUsersQueryRequest request, CancellationToken cancellationToken)
    {
        UserFilterDTO userFilterDTO = _mapper.Map<UserFilterDTO>(request);
        var datas = await _userService.GetUsersAsync(userFilterDTO);
        return datas.Select(u => _mapper.Map<GetUsersQueryResponse>(u))
            .ToList();
    }
}
