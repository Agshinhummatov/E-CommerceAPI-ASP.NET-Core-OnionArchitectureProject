﻿using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Repositories
{
    public class CompletedOrderWriteRepository : WriteRepository<CompletedOrder>, ICompletedOrderWriteRepository
    {
        public CompletedOrderWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
