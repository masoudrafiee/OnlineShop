using MediatR;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Queries
{
    public class GetBuyerQuery: IRequest<IEnumerable<Buyer>>
    {

    }
}
