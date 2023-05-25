using Application.Features.Excel.Commands.WriteOrders;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ExcelsController : ApiControllerBase
    {
        [HttpPost("WriteExcel")]
        public async Task<IActionResult> WriteExcelAsync(ExcelWriteOrdersCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
