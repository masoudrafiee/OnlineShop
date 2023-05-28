using MediatR;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Queries
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Order> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            return await _orderRepository.GetByOrderIdAsync(request.OrderId);
        }
    }
}
