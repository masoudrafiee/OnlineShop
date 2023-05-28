using MediatR;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Commands
{
    public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, bool>
    {
        private readonly IBasketRepository _basketRepository;
        public DeleteBasketCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            Basket basket = await _basketRepository.GetAsync(request.BasketId);
            if (basket == null)
                throw new NullReferenceException("Basket is null");
            _basketRepository.Delete(basket);
            return await _basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}
