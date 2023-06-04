using Application.Features.Products.Commands.Add;
using Application.Features.Products.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductAddCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new ProductGetAllQuery();
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
    }
}
