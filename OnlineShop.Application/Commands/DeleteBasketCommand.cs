using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Commands
{
    public class DeleteBasketCommand:IRequest<bool>
    {
        public int BasketId { get; set; }
        public DeleteBasketCommand(int basketId)
        {
            BasketId = basketId;
        }
    }
}
