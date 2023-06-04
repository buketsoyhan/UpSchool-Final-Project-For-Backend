using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetAllDto>>
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductGetAllQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ProductGetAllDto>> Handle(ProductGetAllQuery query, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products.ToListAsync(cancellationToken);

            var dtos = products.Select(p => new ProductGetAllDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Name = p.Name,
                Price = p.Price,
                Picture = p.Picture,
                IsOnSale = p.IsOnSale,
                SalePrice = p.SalePrice
            }).ToList();

            return dtos;
        }
    }
}
