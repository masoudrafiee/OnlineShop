using Moq;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Builders
{
    public class BasketBuilder
    {
        private Basket _basket;
        public int BasketBuyerId => 1;

        public int BasketId => 1;

        public BasketBuilder()
        {
            _basket = WithNoItems();
        }

        public Basket Build()
        {
            return _basket;
        }

        public Basket WithNoItems()
        {
            var basketMock = new Mock<Basket>(BasketBuyerId);
            basketMock.SetupGet(s => s.Id).Returns(BasketId);

            _basket = basketMock.Object;
            return _basket;
        }

        public Basket WithOneBasketItem()
        {
            var basketMock = new Mock<Basket>(BasketBuyerId);
            _basket = basketMock.Object;
            _basket.AddItem(1, 1, "bag",ProductType.Normal,123,1,DiscountType.Amount,1,1);
            return _basket;
        }
    }
}
