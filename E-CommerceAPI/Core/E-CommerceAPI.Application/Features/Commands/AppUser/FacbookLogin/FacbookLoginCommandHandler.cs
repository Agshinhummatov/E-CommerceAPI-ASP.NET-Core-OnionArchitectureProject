using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Application.DTOs;
using E_CommerceAPI.Application.DTOs.Facbook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace E_CommerceAPI.Application.Features.Commands.AppUser.FacbookLogin
{
    public class FacbookLoginCommandHandler : IRequestHandler<FacbookLoginCommandRequest, FacbookLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public FacbookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacbookLoginCommandResponse> Handle(FacbookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var token  = await _authService.FacbookLoginAsync(request.AuthToken,15);
            return new()
            {
                Token = token
            };
          
        }
    }
}
