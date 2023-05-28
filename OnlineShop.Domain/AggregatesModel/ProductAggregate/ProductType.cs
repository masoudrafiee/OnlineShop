using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.ProductAggregate
{
    public class ProductType : Enumeration
    {
        public static ProductType Fragile = new ProductType(1, nameof(Fragile));
        public static ProductType Normal = new ProductType(2, nameof(Normal)); 
        public ProductType(int id, string name)
            : base(id, name)
        {
        }
    }
}
