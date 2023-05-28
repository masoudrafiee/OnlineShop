using MediatR;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Queries
{
    public class GetBasketQuery : IRequest<Basket>
    {
        public GetBasketQuery(int buyerId)
        {
            BuyerId = buyerId;
        }
        public int BuyerId { get; private set; }
    }
}
