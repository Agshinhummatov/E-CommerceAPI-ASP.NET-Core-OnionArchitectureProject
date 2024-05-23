using E_CommerceAPI.Application.Abstractions.Hubs;
using E_CommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.SignalR.HubSevices
{
    public class ProductHubService : IProductHubService
    {
        readonly IHubContext<PrdocutHub> _hubContext;

        public ProductHubService(IHubContext<PrdocutHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.ProductAddedMessage, message);

        }
    }
}
