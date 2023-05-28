using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.AggregatesModel.ShippingAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
    public class OrderBuilder
    {
        private Order _order;
        public int TestBuyerId => 1;
        public int TestProductId => 1;
        public ProductType TestProductType => ProductType.Normal;
        public DiscountType TestDiscountType => DiscountType.Amount;
        public ShippingType TestShippingType => ShippingType.Post;
        public string TestProductName => "bag";
        public decimal TestPrice = 123;
        public decimal TestProfit = 1;
        public decimal TestDiscountValue = 1;
        public int TestUnits = 3;

        public OrderBuilder()
        {
            _order = WithDefaultValues();
        }

        public Order Build()
        {
            return _order;
        }

        public Order WithDefaultValues()
        {
            var orderItem = new OrderItem(TestProductId, TestProductName,TestProductType, TestPrice, TestProfit,TestDiscountType, TestDiscountValue, TestUnits);
            var itemList = new List<OrderItem>() { orderItem };
            _order = new Order(TestBuyerId, TestShippingType, new AddressBuilder().WithDefaultValues());
            _order.AddOrderItem(TestProductId, TestProductName, TestProductType, TestPrice, TestProfit, TestDiscountType, TestDiscountValue, TestUnits);
            return _order;
        }

        public Order WithNoItems()
        {
            _order = new Order(TestBuyerId,  TestShippingType, new AddressBuilder().WithDefaultValues());
            return _order;
        }

        public Order WithItems(List<OrderItem> items)
        {
            _order = new Order(TestBuyerId, TestShippingType, new AddressBuilder().WithDefaultValues());
            _order.AddOrderItem(TestProductId, TestProductName, TestProductType, TestPrice, TestProfit, TestDiscountType, TestDiscountValue, TestUnits);
            return _order;
        }
        public OrderItem WithOrderItem()
        {
            var orderItem = new OrderItem(TestProductId, TestProductName, TestProductType, TestPrice, TestProfit, TestDiscountType, TestDiscountValue, TestUnits);
            return orderItem;
        }
    }
}
