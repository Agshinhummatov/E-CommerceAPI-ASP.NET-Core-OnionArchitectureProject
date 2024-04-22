using F = E_CommerceAPI.Domain.Entities;

using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Persistence.Contexts;

namespace E_CommerceAPI.Persistence.Repositories // file sozunu silirik
{
    public class FileReadRepository : ReadRepository<F.File>, IFileReadRepository
    {
        public FileReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
