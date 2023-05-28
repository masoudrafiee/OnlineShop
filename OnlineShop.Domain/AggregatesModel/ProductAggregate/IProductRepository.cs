using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.AggregatesModel.ProductAggregate
{
    public interface IProductRepository : IRepository<ProductItem>
    {
        ProductItem Add(ProductItem productItem);
        void Update(ProductItem productItem);
        Task<ProductItem> GetAsync(int productId);
        Task<bool> IsExistAsync(string name,ProductType productType);
        Task<List<ProductItem>> GetListByIdsAsync(List<int> productIds);
        Task<List<ProductItem>> GetListAsync();
    }
}
