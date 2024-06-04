using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandRequset : IRequest<RemoveProductCommandResponse>
    {
        public string Id { get; set; }

    }
}
