using MediatR;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IEnumerable<ProductItem>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<ProductItem>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetListAsync();
        }
    }
}
