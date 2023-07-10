using HrManagment.BLL.Services.EmployeeSalaryReport;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.EmployeeSalaryReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryReportController : ControllerBase
    {
        private readonly IEmployeeSalaryReportService _employeeSalaryReportService;
        public EmployeeSalaryReportController(IEmployeeSalaryReportService employeeSalaryReportService)
        {
            _employeeSalaryReportService = employeeSalaryReportService;
        }

        [HttpGet]
        public async Task<IActionResult> getOffDays(int id ) 
        {
            var res = await _employeeSalaryReportService.CalcOffDays(id);
            return Ok(res);
        }

      
    }
}
