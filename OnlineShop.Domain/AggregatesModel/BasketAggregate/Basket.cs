using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.BasketAggregate
{
    public class Basket : BaseEntity, IAggregateRoot
    {
        public int BuyerId { get; set; }
        private readonly List<BasketItem> _items = new List<BasketItem>();
        public IReadOnlyCollection<BasketItem> Items => _items.AsReadOnly();
        public Basket(int buyerId)
        {
            if (buyerId < 1)
                throw new ArgumentException("buyerId is missing");

            BuyerId = buyerId;
        }
        public void AddItem(int basketId, int productId, string productName, ProductType productType, decimal price, decimal profit, DiscountType discountType, decimal discountValue, int quantity)
        {
            if (!Items.Any(i => i.ProductId == productId))
            {
                _items.Add(new BasketItem(basketId, productId, productName, productType, price,profit, discountType, discountValue, quantity));
                return;
            }
            var existingItem = Items.First(i => i.ProductId == productId);
            existingItem.AddQuantity(quantity);
        }
        public void EmptyItems()
        {
            _items.RemoveAll(i => i.Quantity == 0);
        }
    }
}
