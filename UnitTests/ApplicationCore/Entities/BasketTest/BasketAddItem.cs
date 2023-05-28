using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.BasketTest
{
    public class BasketAddItem
    {
        private readonly int _testProductId = 1; 
        private readonly int _testQuantity = 1;
        private readonly int _buyerId = 1;
        private readonly int _testBasketId = 1;
        private readonly string _testProductName = "bag";
        private readonly ProductType _testProductType = ProductType.Normal;
        private readonly decimal _testPrice = 123;
        private readonly decimal _testProfit = 1;
        private readonly DiscountType _testDiscountType = DiscountType.Amount;
        private readonly decimal _testDiscountValue = 1;

        [Fact]
        public void AddsBasketItemIfNotPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(_testProductId, firstItem.ProductId);
            Assert.Equal(_testPrice, firstItem.Price);
            Assert.Equal(_testQuantity, firstItem.Quantity);
        }

        [Fact]
        public void IncrementsQuantityOfItemIfPresent()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);
            var firstItem = basket.Items.Single();
            Assert.Equal(_testQuantity * 2, firstItem.Quantity);
        }

        [Fact]
        public void KeepsOriginalUnitPriceIfMoreItemsAdded()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice*2, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);
              
            var firstItem = basket.Items.Single();
            Assert.Equal(_testPrice, firstItem.Price);
        }

        [Fact]
        public void DefaultsToQuantityOfOne()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice * 2, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);

            var firstItem = basket.Items.Single();
            Assert.Equal(1, firstItem.Quantity);
        }

        [Fact]
        public void CantAddItemWithNegativeQuantity()
        {
            var basket = new Basket(_buyerId);

            Assert.Throws<ArgumentOutOfRangeException>(() => basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice-1, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity));
        }

        [Fact]
        public void CantModifyQuantityToNegativeNumber()
        {
            var basket = new Basket(_buyerId);
            basket.AddItem(_testBasketId, _testProductId, _testProductName, _testProductType, _testPrice, _testProfit, _testDiscountType, _testDiscountValue, _testQuantity);

            Assert.Throws<ArgumentOutOfRangeException>(() => 
            basket.AddItem(_testBasketId, 
            _testProductId, 
            _testProductName, 
            _testProductType,
            _testPrice - 2, 
            _testProfit, 
            _testDiscountType, 
            _testDiscountValue, 
            _testQuantity)
              );
        }

    }
}
