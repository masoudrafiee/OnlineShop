using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Repositories
{
    public class BuyerRepository : IBuyerRepository
    {
        private readonly OnlineShopContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public BuyerRepository(OnlineShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public Buyer Add(Buyer buyer)
        {
            return _context.Buyer.Add(buyer).Entity;
        }

        public async Task<Buyer> GetAsync(int buyerId)
        {
            var buyer = await _context
                                 .Buyer
                                 .FirstOrDefaultAsync(o => o.Id == buyerId);
            return buyer;
        }

        public void Update(Buyer buyer)
        {
            _context.Entry(buyer).State = EntityState.Modified;
        }

        public async Task<bool> IsExistAsync(string name)
        {
            var isExist = await _context
                               .Buyer
                               .AnyAsync(o => o.Name.Equals(name));
            return isExist;
        }

        public async Task<IEnumerable<Buyer>> GetAllAsync()
        {
            var buyers = await _context
                                .Buyer
                                .ToListAsync();
            return buyers;
        }
    }
}

