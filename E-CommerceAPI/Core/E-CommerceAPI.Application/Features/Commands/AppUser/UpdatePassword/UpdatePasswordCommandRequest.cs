using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
    {
        public string UserId { get; set; } 
        public string ResetToken { get; set; }  
        public string Password { get; set; }   
     

        public string PasswordConfirm { get; set; }
    }
}