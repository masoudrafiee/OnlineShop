using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.AggregatesModel.BasketAggregate
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Basket Add(Basket basket);
        void Update(Basket basket);
        void Delete(Basket basket);
        Task<Basket> GetAsync(int basketId);
        Task<Basket> GetByBuyerIdAsync(int buyerId);
    }
}
