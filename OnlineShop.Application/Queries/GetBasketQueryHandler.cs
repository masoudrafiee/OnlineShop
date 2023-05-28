using MediatR;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Queries
{
    class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, Basket>
    {
        private readonly IBasketRepository _basketRepository;
        public GetBasketQueryHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Basket> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            if (request.BuyerId < 1)
                throw new ArgumentException("BuyerId is missing");

            return await _basketRepository.GetByBuyerIdAsync(request.BuyerId);
        }
    }
}
