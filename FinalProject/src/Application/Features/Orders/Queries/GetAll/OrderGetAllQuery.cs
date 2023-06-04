using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetAll
{
    public class OrdersGetAllQuery : IRequest<List<OrderEventGetAllDto>>
    {
        public OrdersGetAllQuery()
        {
        }
        public OrdersGetAllQuery(bool? isDeleted)
        {
            IsDeleted = isDeleted;
        }
        public bool? IsDeleted { get; set; }
    }
}
