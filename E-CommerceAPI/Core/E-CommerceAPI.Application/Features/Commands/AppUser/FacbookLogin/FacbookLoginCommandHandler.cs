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
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly HttpClient _httpClient;
        public FacbookLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler,IHttpClientFactory httpClientFactory)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _httpClient = httpClientFactory.CreateClient();


        }

        public async Task<FacbookLoginCommandResponse> Handle(FacbookLoginCommandRequest request, CancellationToken cancellationToken)
        {

            string accessTokenResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id=1630018717830717&client_secret=3ba68964bf9421426f691d4c511061b2&grant_type=client_credentials");
            FacebookAccessTokenResponse facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={request.AuthToken}&access_token={facebookAccessTokenResponse.AccessToken}");

            FacebookUserAccessTokenValidation validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);
            if (validation.Data.IsValid)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={request.AuthToken}");

                FacebookUserInfoResponse userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                bool result = user != null;
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfo.Email);
                    if (user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfo.Email,
                            UserName = userInfo.Email,
                            NameSurname = userInfo.Name
                        };
                        var identityResult = await _userManager.CreateAsync(user);
                        result = identityResult.Succeeded;
                    }
                }

                if (result)
                {
                    await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                    Token token = _tokenHandler.CreateAccessToken(5);
                    return new()
                    {
                        Token = token
                    };
                }

            }
            throw new Exception("Invalid external authentication.");
        }
    }
}
