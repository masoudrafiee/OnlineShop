using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.ShippingAggregate
{
    public class ShippingType: Enumeration
    {
        public static ShippingType Express = new ShippingType(1, "Express");
        public static ShippingType Post = new ShippingType(2, "Post");
        public ShippingType(int id, string name)
            : base(id, name)
        {

        }
    }
}
