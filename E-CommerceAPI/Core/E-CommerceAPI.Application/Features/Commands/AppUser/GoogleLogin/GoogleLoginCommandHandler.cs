using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "227724186272-cd6hkg2ee1s62h8p7hoka7hbu8ad2ibi.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);

          var info =   new UserLoginInfo(request.Provider, payload.Subject, request.Provider);

           await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider,
                info.ProviderKey); // varsa eger login ol 
             bool result = user != null; // user null deyilse ture ver
            if (user == null) // burda yoxlayiramki bu kullancii varmi burda null gelir yeni disarda olan ASPNETUsersLogins tabelinde bele bir user yoxdu ilk defe google login edir ve userimiz nulldir ve bunu kaydetmek gerekir
            {
                user = await _userManager.FindByEmailAsync(payload.Email); // burda ise men yoxlayiram belke diger tablemde menim gelen datanin icinde bele bir mail var yeni sohbet AspNetUsers tabilinden gedir 
                if (user == null) // eger bu mailde yoxsa eger menim veri tabanimda bele bir kulanici yoxdur
                {
                    user = new()
                    {
                       Id = Guid.NewGuid().ToString(),
                       Email = payload.Email,
                       UserName = payload.Email,
                       NameSurname = payload.Name
                    };

                    var identityResult = await _userManager.CreateAsync(user); // burda ise men artiq useri yaradirem gedib dusur menim esas Tabeleme 'AspNetUsers'
                   result = identityResult.Succeeded;
                }
            }

            if (result) // erger turedirse artiq burda deyiriki userimiz dis kaynakdan geldi ve kaydedildi b
                await _userManager.AddLoginAsync(user, info); // burda ise men gedirem ASPNETUsersLogins icine kayd edirem bunu yeni diskaynaga
            else
                throw new Exception("Invalid external authentication");

            Token token = _tokenHandler.CreateAccessToken(5); // 5 deqiqelik bir token olsudurmasini isdeyirem

            return new()
            {
               Token = token
            };

        }
    }
}

