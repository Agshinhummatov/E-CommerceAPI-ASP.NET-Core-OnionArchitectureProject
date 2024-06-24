using MediatR;

namespace E_CommerceAPI.Application.Features.Commands.Order.ComplatedOrder
{
    public class CompleteOrderCommandRequest : IRequest<CompleteOrderCommandResponse>
    {
        public string Id { get; set; }
    }
}