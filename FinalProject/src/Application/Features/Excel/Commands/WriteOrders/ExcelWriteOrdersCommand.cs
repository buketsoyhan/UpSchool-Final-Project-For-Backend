using MediatR;

namespace Application.Features.Excel.Commands.WriteOrders
{
    public class ExcelWriteOrdersCommand:IRequest<object>
    {
        public string ExcelBase64File { get; set; }
    }
}
