using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Api.Dto;
using OnlineShop.Application.Commands;
using OnlineShop.Application.Queries;
using OnlineShop.Domain.AggregatesModel.BuyerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuyerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BuyerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Buyer>> CreateBuyer([FromBody] BuyerDto buyer)
        {
            var result = await _mediator.Send(new CreateBuyerCommand(buyer.Name));
            return Ok(result);
        }

        [HttpGet("Buyers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyers()
        {
            var result = await _mediator.Send(new GetBuyerQuery());
            return Ok(result);
        }
    }
}
