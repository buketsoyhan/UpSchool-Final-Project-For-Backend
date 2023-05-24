using Application.Common.Interfaces;
using Application.Common.Models.Excel;

namespace Infrastructure.Services
{
    public class ExcelManager : IExcelService
    {
        public Task<List<ExcelSaveDto>> SaveExcelAsync(ExcelSaveDto saveDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
