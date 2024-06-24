using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.DTOs.Order;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.Order.ComplatedOrder
{
    public class CompleteOrderCommandHanlder : IRequestHandler<CompleteOrderCommandRequest, CompleteOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IMailService _mailService;

        public CompleteOrderCommandHanlder(IOrderService orderService, IMailService mailService)
        {
            _orderService = orderService;
            _mailService = mailService;
        }

        public async Task<CompleteOrderCommandResponse> Handle(CompleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
          (bool succesed,CompletedOrderDTO dto)  =  await _orderService.CompleteOrderAsync(request.Id);
            if (succesed)
             await   _mailService.SendCompletedOrderMailAsync(dto.EMail,dto.OrderCode,dto.OrderDate,dto.userName);
            
            return new();
        }
    }
}
