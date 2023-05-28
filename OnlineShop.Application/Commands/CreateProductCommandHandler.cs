using MediatR;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnlineShop.Application.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductItem>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductItem> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new ProductItem(request.Name, request.Price, request.DiscountType, request.DiscountValue, request.Profit, request.ProductType, request.Description);
            var isExist= await _productRepository.IsExistAsync(product.Name, product.ProductType);
            if (isExist)
            {
                throw new ArgumentException("Product is exist");
            }
            _productRepository.Add(product);
            await _productRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return product;
        }
    }
}
