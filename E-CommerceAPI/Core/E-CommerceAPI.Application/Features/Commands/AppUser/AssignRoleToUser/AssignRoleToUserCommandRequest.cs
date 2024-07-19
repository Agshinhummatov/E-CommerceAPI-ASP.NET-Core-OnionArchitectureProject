using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }

    }
}