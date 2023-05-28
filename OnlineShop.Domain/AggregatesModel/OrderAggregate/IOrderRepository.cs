using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.AggregatesModel.OrderAggregate
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Add(Order order);
        void Update(Order order);
        Task<Order> GetAsync(int orderId);
        Task<Order> GetByOrderIdAsync(int orderId);

    }
}
