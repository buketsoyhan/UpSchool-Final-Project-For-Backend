using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Features.Products.Queries.GetAll
{
    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, List<ProductGetAllDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ProductGetAllQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ProductGetAllDto>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            var dbQuery=_applicationDbContext.Products.AsQueryable();

            dbQuery = dbQuery.Where(x=> Convert.ToInt32(x.OrderId)==request.OrderId);

            if(request.IsDeleted.HasValue) dbQuery = dbQuery.Where(x=>x.IsDeleted==request.IsDeleted.Value);

            //return await eksik
        }
    }
}
