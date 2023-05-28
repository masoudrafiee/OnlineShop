using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Api.Dto;
using OnlineShop.Application.Commands;
using OnlineShop.Application.Queries;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketController> _logger;
        private readonly IMapper _mapper;
        public BasketController(ILogger<BasketController> logger, IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Basket>> CreateBasket([FromBody] BasketDto basket)
        {
            //var command= _mapper.Map<CreateBasketCommand>(basket);
           var result= await _mediator.Send(new CreateBasketCommand(basket.BasketId,basket.BuyerId,basket.Quantity,basket.ProductId)); 
            return Ok(result);
        }

        [HttpGet("{buyerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Basket>> GetBasket(int buyerId)
        {
            var result = await _mediator.Send(new GetBasketQuery(buyerId));
            return Ok(result);
        }

        [HttpDelete]
        [Route("{basketId}")]
        public async Task<ActionResult<Basket>> DeleteBasket( int basketId)
        {
            await _mediator.Send(new DeleteBasketCommand(basketId));
            return Ok();
        }
    }
}
