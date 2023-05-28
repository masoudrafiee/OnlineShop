using OnlineShop.Domain.AggregatesModel.ProductAggregate;

namespace OnlineShop.Api.Dto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountValue { get; set; }
        public decimal Profit { get; set; }
        public int ProductType { get; set; }
        public int DiscountType { get; set; }
        public string Description { get; set; }
    }
}
