using E_CommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Application.DTOs.Token CreateAccessToken(int second)
        {
            Application.DTOs.Token token = new();
            // Simmetrik Təhlükəsizlik Açarı  SecurityKey in simetricini aliram

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            // sifrelenmis kimliyi olsuturuyoruz

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            // olusturalcak token ayarlarini veriyoruz

            token.Expiration = DateTime.UtcNow.AddSeconds(second); // bura saniyelik ve ya nece deqielik  vaxt veririkki token bu qeder yasayacaq

            JwtSecurityToken securityToken = new(
                audience : _configuration["Token:Audience"],
                issuer : _configuration["Token:Issuer"],
                expires : token.Expiration,
                notBefore : DateTime.UtcNow, // ne zaman tokeni olsudursun ve devereye girsin? ele hemen deqiqe method bu name spacdedi E_CommerceAPI.Infrastructure.Services.Token
                signingCredentials : signingCredentials
                );

            // token olsdurucu sinifindan bir ornek alalim

            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken =  tokenHandler.WriteToken(securityToken);

            //string refershToken = CreateRefreshToken();
            token.RefreshToken = CreateRefreshToken();
            return token;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
