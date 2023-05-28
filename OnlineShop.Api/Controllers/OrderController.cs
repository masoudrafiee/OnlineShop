using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Api.Dto;
using OnlineShop.Application.Commands;
using OnlineShop.Application.Queries;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using OnlineShop.Domain.AggregatesModel.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(
            IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Order>> CreateOrder([FromBody]OrderDto order)
        {
            var result = await _mediator.Send(new CreateOrderCommand(order.BuyerId, order.City, order.Street, order.State, order.Country, order.ZipCode));
            return Ok(result);
        }

        [HttpGet("{orderId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Order>> GetOrder(int orderId)
        {
            var result = await _mediator.Send(new GetOrderQuery(orderId));
            return Ok(result);
        }
    }
}
