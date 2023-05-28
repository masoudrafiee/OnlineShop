using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly OnlineShopContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public BasketRepository(OnlineShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
     
        public Basket Add(Basket basket)
        {
            return _context.Basket.Add(basket).Entity;
        }

        public async Task<Basket> GetAsync(int basketId)
        {
            var basket = await _context
                                .Basket
                                .Include(x => x.Items)
                                .FirstOrDefaultAsync(o => o.Id == basketId);
            return basket;
        }
        public async Task<Basket> GetByBuyerIdAsync(int buyerId)
        {
            var order = await _context
                                .Basket
                                .Include(x=>x.Items) 
                                .FirstOrDefaultAsync(o => o.BuyerId== buyerId);
            return order;
        }

        public void Update(Basket basket)
        {
            _context.Entry(basket).State = EntityState.Modified;
        }
        public void Delete(Basket basket)
        {
            _context.Entry(basket).State = EntityState.Deleted;
        }
    }
}
