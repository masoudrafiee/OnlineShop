using MediatR;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Commands
{
    class CreateBuyerCommandHandler : IRequestHandler<CreateBuyerCommand, Buyer>
    {
        private readonly IBuyerRepository _buyerRepository;
        public CreateBuyerCommandHandler(IBuyerRepository buyerRepository)
        {
            _buyerRepository = buyerRepository;
        }
        public async Task<Buyer> Handle(CreateBuyerCommand request, CancellationToken cancellationToken)
        {
            var buyer = new Buyer(Guid.NewGuid().ToString(), request.Name);
            var isExist = await _buyerRepository.IsExistAsync(buyer.Name);
            if (isExist)
                throw new ArgumentException("Buyer is exist");

            _buyerRepository.Add(buyer);
            await _buyerRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return buyer;
        }
    }
}
