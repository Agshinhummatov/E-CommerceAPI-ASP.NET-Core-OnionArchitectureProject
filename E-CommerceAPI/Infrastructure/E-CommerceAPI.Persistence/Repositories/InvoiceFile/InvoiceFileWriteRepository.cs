using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Persistence.Contexts;

namespace E_CommerceAPI.Persistence.Repositories
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
