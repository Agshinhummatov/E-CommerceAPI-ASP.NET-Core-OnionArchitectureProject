using E_CommerceAPI.Application.DTOs.User;
using E_CommerceAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser model);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accsessTokenDate, int addOnAccsessTokenDate);

        Task UpdatePasswordAsync(string userId,string resetToken,string newPassword);


        Task<List<ListUser>> GetAllUsersAsync( int page,int size);

        int TotalUsersCount {  get; }

        Task AssignRoleToUserAsnyc(string userId, string[] roles);

        Task<string[]> GetRolesToUserAsync(string userId);

    }
}
