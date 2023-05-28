using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.Exceptions;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.BasketAggregate
{
    public class BasketItem : BaseEntity
    {
        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public ProductType ProductType { get; private set; }
        public int BasketId { get; private set; }
        public decimal Price { get; private set; }
        public decimal Profit { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public decimal DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public BasketItem()
        {

        }
        public BasketItem(int basketId, int productId, string productName, ProductType productType, decimal price, decimal profit, DiscountType discountType, decimal discountValue, int quantity)
        {
            if (basketId < 1)
                throw new ArgumentException("buyerId is missing");
            if (productId < 1)
                throw new ArgumentException("productId is missing");
            if (string.IsNullOrEmpty(productName))
                throw new ArgumentException("productName is missing");
            if (price < 1)
                throw new ArgumentException("price is missing");
            if (profit < 0)
                throw new ArgumentException("profit is missing");
            if (discountValue < 0)
                throw new ArgumentException("discountValue is missing");
            if (quantity < 1)
                throw new ArgumentException("quantity is missing");

            ProductId = productId;
            ProductName = productName;
            ProductType = productType;
            BasketId = basketId;
            Price = price;
            Profit = profit;
            DiscountType = discountType;
            DiscountValue = discountValue;
            SetQuantity(quantity);
        }

        public void AddQuantity(int quantity)
        {
            if (quantity < 1)
                AddError("Invalid number of quantity");

            if (Errors.Any())
                throw new DomainException(Errors.Select(e => e.Error));

            Quantity += quantity;
        }

        public void SetQuantity(int quantity)
        {
            if (quantity < 1)
                AddError("Invalid number of quantity");

            if (Errors.Any())
                throw new DomainException(Errors.Select(e => e.Error));
            Quantity = quantity;
        }
    }
}
