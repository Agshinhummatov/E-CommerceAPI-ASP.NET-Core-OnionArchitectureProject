using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequset, UpdateProductCommandResponce>
    {
        public async Task<UpdateProductCommandResponce> Handle(UpdateProductCommandRequset request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
