using E_CommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Token
{
    public interface ITokenHandler 
    {
        DTOs.Token CreateAccessToken(int second,AppUser appUser);
        string CreateRefreshToken();
    }
}
