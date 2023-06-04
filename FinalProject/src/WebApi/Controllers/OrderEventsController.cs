using Application.Features.OrderEvents.Commands.Add;
using Application.Features.OrderEvents.Queries.GetAll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderEventController : ControllerBase
    {
        private readonly OrderEventAddCommandHandler _orderEventAddCommandHandler;
        private readonly OrderEventGetAllQueryHandler _orderEventGetAllQueryHandler;

        public OrderEventController(OrderEventAddCommandHandler orderEventAddCommandHandler, OrderEventGetAllQueryHandler orderEventGetAllQueryHandler)
        {
            _orderEventAddCommandHandler = orderEventAddCommandHandler;
            _orderEventGetAllQueryHandler = orderEventGetAllQueryHandler;
        }

        [HttpPost]
        public IActionResult AddOrderEvent(OrderEventAddCommand command)
        {
            _orderEventAddCommandHandler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllOrderEvents()
        {
            var query = new OrderEventGetAllQuery();
            var dtos = _orderEventGetAllQueryHandler.Handle(query);
            return Ok(dtos);
        }
    }
}
