using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services.Authentications
{
    public interface IExternalAuthentications
    {
        Task<DTOs.Token> FacbookLoginAsync(string authToken,int accessTokenLifeTime);
        Task<DTOs.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);  
    }
}
