using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTests.Builders;
using Xunit;

namespace UnitTests.ApplicationCore.Entities.OrderTest
{
    public class OrderTotal
    {
        private decimal _testUnitPrice = 373;

        [Fact]
        public void IsZeroForNewOrder()
        {
            var order = new OrderBuilder().WithNoItems();

            Assert.Equal(0, order.GetTotal());
        }

        [Fact]
        public void IsCorrectGiven1Item()
        {
            var builder = new OrderBuilder();
            var items = new List<OrderItem>
            {
                builder.WithOrderItem()
            };
            var order = new OrderBuilder().WithItems(items);
            Assert.Equal(_testUnitPrice, order.GetTotal());
        }

        [Fact]
        public void IsCorrectGiven4Items()
        {
            var builder = new OrderBuilder();
            var order = builder.WithDefaultValues();

            Assert.Equal(  builder.TestUnits* (builder.TestPrice+builder.TestDiscountValue + builder.TestProfit), order.GetTotal());
        }
    }
}

