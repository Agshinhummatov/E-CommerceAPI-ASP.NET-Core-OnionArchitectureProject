using E_CommerceAPI.Application.Repositories;
using MediatR;
using P = E_CommerceAPI.Domain.Entities;

namespace E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequset, UpdateProductCommandResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequset request, CancellationToken cancellationToken)
        {
            P.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;


            await _productWriteRepository.SaveAsync();

            return new();
        }
    }
}
