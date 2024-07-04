using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Abstractions.Services
{
    public interface IAuthorizationEndpointService
    {
        Task AssignRoleEndpointAsync(string[] roles,string menu,string code,Type type);

         Task<List<string>> GetRolesToEndpointAsync(string code, string menu);
    }
}
