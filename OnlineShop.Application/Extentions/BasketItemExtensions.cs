using OnlineShop.Application.Commands;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Application.Extentions
{
    public static class BasketItemExtensions
    {
        public static IEnumerable<OrderItemDTO> ToOrderItemsDTO(this IEnumerable<BasketItem> basketItems)
        {
            foreach (var item in basketItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }

        public static OrderItemDTO ToOrderItemDTO(this BasketItem item)
        {
            return new OrderItemDTO()
            {
                ProductId = item.ProductId, 
                Units = item.Quantity
            };
        }
    }
}
