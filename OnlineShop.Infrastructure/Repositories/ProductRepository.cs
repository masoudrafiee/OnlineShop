using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ProductRepository(OnlineShopContext context)
        {
            _context = context;
        }

        public ProductItem Add(ProductItem productItem)
        {
            return _context.ProductItem.Add(productItem).Entity;
        }

        public async Task<ProductItem> GetAsync(int productId)
        {
            var productItem = await _context
                            .ProductItem 
                            .FirstOrDefaultAsync(o => o.Id == productId);

            return productItem;
        }

        public async Task<bool> IsExistAsync(string name, ProductType productType)
        {
            var productItem = await _context
                         .ProductItem
                         .Where(o=> o.Name.Equals(name) && o.ProductType==productType).AnyAsync();

            return productItem;
        }

        public async Task<List<ProductItem>> GetListByIdsAsync(List<int> productIds)
        {
            var productItems = await _context
                            .ProductItem
                            .Include(x => x.ProductType)
                            .Where(o => productIds.Any(y => y == o.Id)).ToListAsync();


            return productItems;
        }

        public async Task<List<ProductItem>> GetListAsync()
        {
            var productItems = await _context
                            .ProductItem
                            .ToListAsync();


            return productItems;
        }
        public void Update(ProductItem productItem)
        {
            _context.Entry(productItem).State = EntityState.Modified;
        }
    }
}
