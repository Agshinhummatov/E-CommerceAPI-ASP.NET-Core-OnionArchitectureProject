using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Order;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace E_CommerceAPI.Persistence.Services
{
    public class OrderService : IOrderService
    {
        readonly IOrderWriteRepository _orderWriteRepository;
        readonly IOrderReadRepository _orderReadRepository;
        readonly ICompletedOrderWriteRepository _completedOrderWriteRepository;
        readonly ICompletedOrderReadRepository _completedOrderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository, ICompletedOrderWriteRepository completedOrderWriteRepository, ICompletedOrderReadRepository completedOrderReadRepository = null)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
            _completedOrderWriteRepository = completedOrderWriteRepository;
            _completedOrderReadRepository = completedOrderReadRepository;
        }



        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 100000000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".")+1, orderCode.Length - orderCode.IndexOf(".")-1);
            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
            });

            await _orderWriteRepository.SaveAsync();
        }



        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(o => o.Basket)
                      .ThenInclude(b => b.User)
                      .Include(o => o.Basket)
                         .ThenInclude(b => b.BasketItems)
                         .ThenInclude(bi => bi.Product);

           



            var data = query.Skip(page * size).Take(size);


           var _data = from order in data    // left join
                       join completedOrder in _completedOrderReadRepository.Table
                      on order.Id equals completedOrder.OrderId into co
                      from _co in co.DefaultIfEmpty()
                      select new 
                      {
                       Id =   order.Id,
                       CreatedDate =  order.CreatedDate,
                       OrderCode =  order.OrderCode,
                       Basket =   order.Basket,
                       Completed = _co != null ? true : false

                      };
           

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await _data.Select(o => new
                {    Id = o.Id,
                    CreatedDate = o.CreatedDate,
                    OrderCode = o.OrderCode,
                    TotalPrice = o.Basket.BasketItems.Sum(bi => bi.Product.Price * bi.Quantity),
                    UserName = o.Basket.User.UserName,
                    o.Completed
                }).ToListAsync()
            };
        }

        public async Task<SingleOrder> GetOrderByIdAsync(string id)
        {
            var data = _orderReadRepository.Table.Include(o => o.Basket)
                 .ThenInclude(b => b.BasketItems)
                 .ThenInclude(bi => bi.Product);

            // left Join
            var _data = await (from order in data  
                        join completedOrder in _completedOrderReadRepository.Table
                        on order.Id equals completedOrder.OrderId into co
                        from _co in co.DefaultIfEmpty()
                        select new
                        {
                            Id = order.Id,
                            CreatedDate = order.CreatedDate,
                            OrderCode = order.OrderCode,
                            Basket = order.Basket,
                            Completed = _co != null ? true : false,
                            Address = order.Address,
                            Description = order.Description

                        }).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));




            return new()
            {
                Id = _data.Id.ToString(),
                BasketItems = _data.Basket.BasketItems.Select(bi => new
                {
                    bi.Product.Name,
                    bi.Product.Price,
                    bi.Quantity,

                }),
             Address = _data.Address,
             Description = _data.Description,
             CreatedDate = _data.CreatedDate,
             OrderCode = _data.OrderCode,
             Completed  =_data.Completed

            };
        }

        public async Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id)
        {
            Order? order = await _orderReadRepository.Table.Include(o => o.Basket).ThenInclude(b => b.User).FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
            if (order != null)
            {

              await _completedOrderWriteRepository.AddAsync(new() { OrderId = Guid.Parse(id) });
              return  (await _completedOrderWriteRepository.SaveAsync() > 0, new()
              {
                  OrderCode = order.OrderCode,
                  OrderDate = order.CreatedDate,
                  userName = order.Basket.User.UserName,
                
                  EMail = order.Basket.User.Email
              });

            }
            return(false,null);
        }

    }
}
