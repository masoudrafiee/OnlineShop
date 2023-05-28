using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.AggregatesModel.ShippingAggregate;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public Order()
        {

        }

        public DateTime OrderDate { get; private set; }
        public Address Address { get; private set; }
        public int GetBuyerId => _buyerId;
        private int _buyerId;
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public Decimal Total { get; set; }
        public ShippingType ShippingType { get; set; }
        public Order(int buyerId, ShippingType shippingType, Address address) : this()
        {
            if ( buyerId<1)
                throw new ArgumentException("buyerId is missing");

            _buyerId = buyerId;
            Address = address;
            ShippingType = shippingType;
            OrderDate = DateTime.Now;
            _orderItems = new List<OrderItem>();
        }

        public void AddOrderItem(int productId, string productName, ProductType productType, decimal price, decimal profit, DiscountType discountType, decimal discountValue, int quantity)
        {
            var existingOrderForProduct = _orderItems.Where(o => o.ProductId == productId)
                .SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                if (discountValue > existingOrderForProduct.GetCurrentDiscount())
                {
                    existingOrderForProduct.SetNewDiscount(discountValue);
                }
                existingOrderForProduct.AddQuantity(quantity);
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, productType, price, profit, discountType, discountValue, quantity);
                _orderItems.Add(orderItem);
            }
        }

        public decimal GetTotal()
        {
            return _orderItems.Sum(o => (o.GetQuantity() * (GetProductPrice(o.DiscountType, o.DiscountValue, o.GetPrice()) + o.GetProfit())));
        }

        private decimal GetProductPrice(DiscountType discountType, decimal discountValue, decimal price)
        {
            switch (discountType)
            {
                case var value when value == DiscountType.Percentage: 
                    return price * discountValue;
                case var value when value ==DiscountType.Amount:
                    return price + discountValue;
                default:
                    return 0;
            }
        }

    }
}
