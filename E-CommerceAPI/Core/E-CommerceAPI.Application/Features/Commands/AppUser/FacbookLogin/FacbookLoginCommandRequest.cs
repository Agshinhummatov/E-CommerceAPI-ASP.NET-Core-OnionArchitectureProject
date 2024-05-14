using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.FacbookLogin
{
    public class FacbookLoginCommandRequest : IRequest<FacbookLoginCommandResponse>
    {
        public string AuthToken { get; set; }
    }
}
