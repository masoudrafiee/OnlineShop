using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.Dto
{
    public class OrderDto
    {
        public int BuyerId { get;  set; }
        public string City { get;  set; }
        public string Street { get;  set; }
        public string State { get;  set; }
        public string Country { get;  set; }
        public string ZipCode { get;  set; }
    }
}
