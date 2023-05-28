using MediatR;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Queries
{
    class GetBuyerQueryHandler : IRequestHandler<GetBuyerQuery, IEnumerable<Buyer>>
    {
        private readonly IBuyerRepository _buyerRepository;

        public GetBuyerQueryHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }

        public async Task<IEnumerable<Buyer>> Handle(GetBuyerQuery request, CancellationToken cancellationToken)
        {
            return await _buyerRepository.GetAllAsync();
        }
    }
}
