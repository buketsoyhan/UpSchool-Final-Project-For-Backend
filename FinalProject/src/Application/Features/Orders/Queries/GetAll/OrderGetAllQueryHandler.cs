//using Application.Common.Interfaces;
//using Application.Features.OrderEvents.Queries.GetAll;
//using Application.Features.Orders.Commands.Add;
//using Domain.Common;
//using Domain.Entities;
//using MediatR;
//using Microsoft.EntityFrameworkCore;

//namespace Application.Features.Orders.Queries.GetAll
//{
//    public class OrderAddCommandHandler : IRequestHandler<OrderAddCommand, Response<Guid>>
//    {
//        private readonly IApplicationDbContext _applicationDbContext;

//        public OrderAddCommandHandler(IApplicationDbContext applicationDbContext)
//        {
//            _applicationDbContext = applicationDbContext;
//        }
//        public async Task<List<OrderEventGetAllDto>> Handle(OrderAddCommand request, CancellationToken cancellationToken)
//        {
//            var orderEvents = await _applicationDbContext.OrderEvents.ToListAsync();

//            var dtos = orderEvents.Select(e => new OrderEventGetAllDto
//            {
//                Id = e.Id,
//                OrderId = e.OrderId,
//                Status = e.Status
//            }).ToList();

//            return dtos;
//        }
//    }
//}
