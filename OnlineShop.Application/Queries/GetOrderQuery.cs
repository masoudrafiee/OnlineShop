using MediatR;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Queries
{
    public class GetOrderQuery : IRequest<Order>
    {
        public GetOrderQuery(int orderId)
        {
            OrderId = orderId;
        }
        public int OrderId { get; private set; }
    }
}
