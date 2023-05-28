
namespace OnlineShop.Api.Dto
{
    public class BasketDto
    {
        public int BasketId { get; set; }
        public int BuyerId { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
