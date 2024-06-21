using E_CommerceAPI.Application.Abstractions.Services.Authentications;


namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IAuthService : IExternalAuthentication, IInternalAuthentication
    {
       
        Task PasswordResetAsync(string email);

        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);

    }
}
