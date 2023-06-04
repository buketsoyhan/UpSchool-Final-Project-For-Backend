using Application.Features.Orders.Commands.Add;
using Application.Features.Orders.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderAddCommand command)
        {
            var orderId = await _mediator.Send(command);
            return Ok(orderId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new OrdersGetAllQuery();
            var dtos = await _mediator.Send(query);
            return Ok(dtos);
        }
    }
}
