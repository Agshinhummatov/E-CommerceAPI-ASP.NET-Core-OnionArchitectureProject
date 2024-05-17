using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs;
using MediatR;


namespace E_CommerceAPI.Application.Features.Commands.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
          Token token  = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

            return new()
            {
                Token = token
            };
        }
    }
}
