using MediatR;
using OnlineShop.Application.Extentions;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Application.Commands
{
    public class CreateOrderCommand:IRequest<bool>
    {
        //private readonly List<OrderItemDTO> _orderItems;
        //public int BasketId { get; set; }
        public int BuyerId { get; private set; }
        public string City { get; private set; }
        public string Street { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; } 

        public CreateOrderCommand()
        { 
        }
        public CreateOrderCommand(
           // int basketId, 
            int buyerId, 
            string city, 
            string street, 
            string state, 
            string country, 
            string zipcode) : this()
        { 
            //BasketId = basketId;
            BuyerId =buyerId; 
            City = city;
            Street = street;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
    }
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public int Units { get; set; }
        public string PictureUrl { get; set; }
    }
}
