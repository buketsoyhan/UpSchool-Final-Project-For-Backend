using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Features.Excel.Commands.WriteOrders
{
    public class ExcelWriteOrdersCommandHandler : IRequestHandler<ExcelWriteOrdersCommand, Response<int>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IExcelService _excelService;

        public ExcelWriteOrdersCommandHandler(IApplicationDbContext applicationDbContext, IExcelService excelService)
        {
            _applicationDbContext = applicationDbContext;
            _excelService = excelService;
        }

        public async  Task<Response<int>> Handle(ExcelWriteOrdersCommand request, CancellationToken cancellationToken)
        {
            return new Response<int>($"Saved to the excel page");
        }
    }
}
