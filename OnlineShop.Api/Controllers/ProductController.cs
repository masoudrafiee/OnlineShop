using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineShop.Api.Dto;
using OnlineShop.Application.Commands;
using OnlineShop.Application.Queries;
using OnlineShop.Domain.AggregatesModel.BasketAggregate;
using OnlineShop.Domain.AggregatesModel.ProductAggregate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductItem>> CreateProduct([FromBody] ProductDto product)
        {
            var result = await _mediator.Send(new CreateProductCommand(product.Name, product.Price, DiscountType.FromValue<DiscountType>(product.DiscountType), product.DiscountValue, product.Profit, ProductType.FromValue<ProductType>(product.ProductType), product.Description));
            return Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts()
        {
            var result = await _mediator.Send(new GetProductQuery());
            return Ok(result);
        }

    }
}
