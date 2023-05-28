using MediatR;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Queries
{
    public class GetProductQuery : IRequest<IEnumerable<ProductItem>>
    {
    }
}
