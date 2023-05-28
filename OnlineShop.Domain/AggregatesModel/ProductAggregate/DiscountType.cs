using OnlineShop.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Domain.AggregatesModel.ProductAggregate
{
    public class DiscountType : Enumeration
    {
        public static  DiscountType Percentage = new DiscountType(1, nameof(Percentage));
        public static DiscountType Amount = new DiscountType(2, nameof(Amount));
        public DiscountType(int id, string name)
            : base(id, name)
        {

        }
    }
}
