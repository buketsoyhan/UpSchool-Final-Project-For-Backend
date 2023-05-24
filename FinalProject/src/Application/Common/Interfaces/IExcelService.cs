using Application.Common.Models.Excel;

namespace Application.Common.Interfaces
{
    public interface IExcelService
    {
        //Excel save

        Task<List<ExcelSaveDto>> SaveExcelAsync(ExcelSaveDto saveDto, CancellationToken cancellationToken);
    }
}
