using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.User;
using E_CommerceAPI.Application.Exceptions;
using E_CommerceAPI.Application.Helpers;
using E_CommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;


namespace E_CommerceAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

      

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;
        }

  

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accsessTokenDate, int addOnAccsessTokenDate)
        {
    
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accsessTokenDate.AddSeconds(addOnAccsessTokenDate); // esas tokeninde uzerine vereceyimiz paramere uygun olaraqda refershtokenin vaxtini veririk
                await _userManager.UpdateAsync(user);
            }else
            throw new NotFoundUserExeption();
        }


        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
              AppUser user = await  _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user); // burda ise  SecurityStampAsync colomnda olan securtiyi ezirik ve yenileyirik
            }
            else
                throw new PasswordChangeFailedExeption();
        }

        public async Task<List<ListUser>> GetAllUsersAsync(int page,int size)
        {
          var users =  await _userManager.Users.Skip(page*size).Take(size).ToListAsync();

           return users.Select(users => new ListUser
           {
               Id = users.Id,
               Email = users.Email,
               NameSurname = users.NameSurname,
               UserName = users.UserName,
               TwoFactorEnabled =  users.TwoFactorEnabled
               
           }).ToList();
        }

       

        public int TotalUsersCount => _userManager.Users.Count();


        public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUserAsync(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] { };
        }
    }
}
