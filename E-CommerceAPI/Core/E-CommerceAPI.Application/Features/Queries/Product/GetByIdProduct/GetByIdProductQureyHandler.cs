using E_CommerceAPI.Application.Repositories;
using MediatR;
using P = E_CommerceAPI.Domain.Entities;

namespace E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQureyHandler : IRequestHandler<GetByIdProductQureyRequset, GetByIdProductQureyResponse>
    {

        IProductReadRepository _productReadRepository;
public GetByIdProductQureyHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQureyResponse> Handle(GetByIdProductQureyRequset request, CancellationToken cancellationToken)
        {
           P.Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new() {
            Price = product.Price,
            Stock = product.Stock,
            Name = product.Name
            };
        }
    }
}
