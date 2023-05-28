using MediatR;
using OnlineShop.Application.Services;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.AggregatesModel.ShippingAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderTimeService _orderTimeService;
        public CreateOrderCommandHandler(IOrderRepository orderRepository,
                                         IBasketRepository basketRepository,
                                         IProductRepository productRepository,
                                         IOrderTimeService orderTimeService)
        {
            _orderRepository = orderRepository;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _orderTimeService = orderTimeService;
        }
        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Basket basket = await _basketRepository.GetByBuyerIdAsync(request.BuyerId);

            if (basket == null)
                throw new ArgumentNullException("basket is bull");

            //Address address = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);
            // List<ProductItem> productItems = await _productRepository.GetListAsync(basket.Items.Select(x => x.ProductId).ToList());

            SetOrder(ShippingType.Express,ProductType.Fragile, basket.Items, request);
            SetOrder(ShippingType.Post,ProductType.Normal, basket.Items, request); 

            return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }

        private void SetOrder(ShippingType shippingType, ProductType productType, IReadOnlyCollection<BasketItem> items, CreateOrderCommand request)
        {
            Address address = new Address(request.Street, request.City, request.State, request.Country, request.ZipCode);
            Order order = new Order(request.BuyerId, shippingType, address);
            var baskets = items.Where(x => x.ProductType == productType).ToList() ?? new List<BasketItem>();
            foreach (var bst in baskets)
            {
                order.AddOrderItem(bst.ProductId, bst.ProductName, bst.ProductType, bst.Price, bst.Profit, bst.DiscountType, bst.DiscountValue, bst.Quantity);
            }

            decimal total = order.GetTotal();
            order.Total = total;

            if (!_orderTimeService.IsOrderTime(order.OrderDate.TimeOfDay))
                throw new Exception("order time  between 8AM and 7PM ");

            if (total < 50000)
                throw new Exception("The minimum order amount is 50000 ");

            _orderRepository.Add(order);
        } 
    }
}
