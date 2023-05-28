using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly OnlineShopContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public OrderRepository(OnlineShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Order Add(Order order)
        {
            return _context.Order.Add(order).Entity;

        }

        public async Task<Order> GetAsync(int orderId)
        {
            var order = await _context
                                .Order
                                .Include(x => x.Address)
                                .FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
            {
                order = _context
                            .Order
                            .Local
                            .FirstOrDefault(o => o.Id == orderId);
            }
            if (order != null)
            {
                await _context.Entry(order)
                    .Collection(i => i.OrderItems).LoadAsync();
          
            }

            return order;
        }

        public void Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
        }

        public async Task<Order> GetByOrderIdAsync(int orderId)
        {
            var order = await _context
                                .Order
                                .Include(x => x.Address)
                                .FirstOrDefaultAsync(o => o.Id == orderId);
            return order;
        }
    }
}
