using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQureyRequset : IRequest<GetByIdProductQureyResponse>
    {

        public string Id { get; set; }
    }
}
