using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQureyHandler : IRequestHandler<GetProductImagesQureyRequset, GetProductImagesQureyResponce>
    {
        public async Task<GetProductImagesQureyResponce> Handle(GetProductImagesQureyRequset request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
