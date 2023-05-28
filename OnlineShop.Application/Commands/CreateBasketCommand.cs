using MediatR;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Commands
{
    public class CreateBasketCommand : IRequest<Basket>
    {
        public int BasketId { get; private set; }
        public int BuyerId { get; private set; }
        public int Quantity { get; private set; }
        public int ProductId { get; private set; }

        public CreateBasketCommand(int basketId, int buyerId, int quantity, int productItemId)
        {
            BasketId = basketId;
            BuyerId = buyerId;
            Quantity = quantity;
            ProductId = productItemId;
        }
    }
}
