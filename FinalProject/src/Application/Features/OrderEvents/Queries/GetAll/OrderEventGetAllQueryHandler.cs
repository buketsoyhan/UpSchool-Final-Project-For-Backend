using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.OrderEvents.Queries.GetAll
{
    public class OrderEventGetAllQueryHandler : IRequestHandler<OrderEventGetAllQuery, List<OrderEventGetAllDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderEventGetAllQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderEventGetAllDto>> Handle(OrderEventGetAllQuery query, CancellationToken cancellationToken)
        {
            var orderEvents = await _dbContext.OrderEvents.ToListAsync(cancellationToken);

            var dtos = orderEvents.Select(e => new OrderEventGetAllDto
            {
                Id = e.Id,
                OrderId = e.OrderId,
                Status = e.Status
            }).ToList();

            return dtos;
        }

        public object Handle(OrderEventGetAllQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
