using HrManagment.BLL.Services.EmployeeSalaryReport;
using HrManagment.BLL.Services.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.EmployeeSalaryReport
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryReportController : ControllerBase
    {
        private readonly IEmployeeSalaryReportService _employeeSalaryReportService;
        private readonly IEmployeeService _employeeService;

        public EmployeeSalaryReportController(IEmployeeSalaryReportService employeeSalaryReportService , IEmployeeService employeeService)
        {
            _employeeSalaryReportService = employeeSalaryReportService;
            _employeeService = employeeService;
        }

        //[HttpGet]
        //public async Task<IActionResult> getOffDays(int id)
        //{
        //    var res = await _employeeSalaryReportService.CalcOffDays(id);
        //    return Ok(res);
        //}


 

        [HttpPost]
        public async Task<IActionResult> CalacAllEmpSalaries()
        {
            await _employeeSalaryReportService.CalcSalariesForAllEmps();
            return Ok("added successfuly");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOldSalaries()
        {
            await _employeeSalaryReportService.CalcSalariesForAllEmps();
            var res = await _employeeSalaryReportService.GetAll();
            
            return Ok(res);
        }


    }
}
