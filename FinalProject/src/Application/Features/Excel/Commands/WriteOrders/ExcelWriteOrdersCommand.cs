using Domain.Common;
using MediatR;

namespace Application.Features.Excel.Commands.WriteOrders
{
    public class ExcelWriteOrdersCommand:IRequest<Response<int>>
    {
        public string ExcelBase64File { get; set; }
    }
}
