using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.User;
using E_CommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            CreateUserResponse response = await _userService.CreateAsync(new()
            {
             Email =request.Email,
             NameSurname = request.NameSurname,
             Password = request.Password,
             PasswordConfirm = request.PasswordConfirm,
             Username = request.Username,

            });


            return new()
            {
                   Message = response.Message,
                   Succeeded = response.Succeeded
            };
                


        }
    }
}
