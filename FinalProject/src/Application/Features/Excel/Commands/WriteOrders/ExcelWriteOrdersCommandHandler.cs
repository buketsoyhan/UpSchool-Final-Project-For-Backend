using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Excel.Commands.WriteOrders
{
    public class ExcelWriteOrdersCommandHandler : IRequestHandler<ExcelWriteOrdersCommand, object>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IExcelService _excelService;

        public ExcelWriteOrdersCommandHandler(IApplicationDbContext applicationDbContext, IExcelService excelService)
        {
            _applicationDbContext = applicationDbContext;
            _excelService = excelService;
        }

        public Task<object> Handle(ExcelWriteOrdersCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
