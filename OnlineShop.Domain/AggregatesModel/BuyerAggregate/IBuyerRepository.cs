using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.AggregatesModel.BuyerAggregate
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        void Update(Buyer buyer);
        Task<Buyer> GetAsync(int buyerId);
        Task<IEnumerable<Buyer>> GetAllAsync();
        Task<bool> IsExistAsync(string name);
    }
}
