using MediatR;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Commands
{
    public class CreateBuyerCommand : IRequest<Buyer>
    {
        public CreateBuyerCommand(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }
}
