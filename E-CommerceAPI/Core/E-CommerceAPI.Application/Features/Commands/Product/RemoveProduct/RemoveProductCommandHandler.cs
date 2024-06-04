using E_CommerceAPI.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Product.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommandRequset, RemoveProductCommandResponse>
    {
        readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequset request, CancellationToken cancellationToken)
        {

            await _productWriteRepository.RemoveAsync(request.Id);
            await _productWriteRepository.SaveAsync();
            return new();
        }


    }
}
