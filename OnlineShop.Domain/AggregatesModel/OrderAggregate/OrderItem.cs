using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        {

        }
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public ProductType ProductType { get; private set; }
        public decimal Price { get; private set; }
        public decimal Profit { get; private set; }
        public decimal DiscountValue { get; private set; } 
        public DiscountType DiscountType { get; private set; }
        public int Quantity { get; private set; }

        public OrderItem(int productId, string productName, ProductType productType, decimal price, decimal profit, DiscountType discountType , decimal discountValue, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Invalid number of quantity");

            if ((price * quantity) < discountValue)
                throw new Exception("The total of order item is lower than applied discount");

            ProductId = productId;
            ProductName = productName;
            ProductType = productType;
            Price = price;
            Profit = profit;
            DiscountType = discountType;
            DiscountValue = discountValue;
            Quantity = quantity;
        }

        public void SetNewDiscount(decimal discount)
        {
            if (discount < 0)
                throw new Exception("Discount is not valid");

            DiscountValue = discount;
        }

        public ProductType GetProductTypeId()
        {
            return ProductType;
        }

        public decimal GetCurrentDiscount()
        {
            return DiscountValue;
        }

        public decimal GetProfit()
        {
            return Profit;
        }
        public int GetQuantity()
        {
            return Quantity;
        }

        public decimal GetPrice()
        {
            return Price;
        }
        public void AddQuantity(int quantity)
        {
            if (quantity < 0)
            {
                throw new Exception("Invalid Quantity");
            }

            Quantity += quantity;
        }

    }
}
