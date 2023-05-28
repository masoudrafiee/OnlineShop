using MediatR;
using Microsoft.Extensions.Logging;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Commands
{
    public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, Basket>
    {
        private readonly IBuyerRepository _buyerRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<CreateBasketCommandHandler> _logger;
        public CreateBasketCommandHandler(IBasketRepository basketRepository,
                                          IProductRepository productRepository,
                                          ILogger<CreateBasketCommandHandler> logger,
                                          IBuyerRepository buyerRepository)
        {
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _buyerRepository = buyerRepository;
            _logger = logger;
        }
        public async Task<Basket> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.GetAsync(request.BuyerId);
            if (buyer == null)
            {
                throw new ArgumentNullException("Buyer not found");
            }
            var basket = await _basketRepository.GetByBuyerIdAsync(request.BuyerId);
            if (basket == null)
            {
                basket = new Basket(request.BuyerId);
                _basketRepository.Add(basket);
                await _basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            var product = await _productRepository.GetAsync(request.ProductId);
            if (product == null)
                throw new ArgumentNullException("Product not fount");

            basket.AddItem(request.BasketId,
                request.ProductId,
                product.Name,
                product.ProductType,
                product.Price,
                product.Profit,
                product.DiscountType,
                product.DiscountValue,
                request.Quantity);
            _basketRepository.Update(basket);
            await _basketRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return basket;
        }
    }
}
