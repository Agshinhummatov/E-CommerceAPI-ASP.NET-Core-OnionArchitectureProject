using E_CommerceAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.AppUsers.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        readonly IUserService _userServicre;

        public GetRolesToUserQueryHandler(IUserService userServicre)
        {
            _userServicre = userServicre;
        }

        public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
         var userRoles = await _userServicre.GetRolesToUserAsync(request.UserId);

            return new()
            {
                UserRoles= userRoles
            };
        }
    }
}
