using HrManagment.BLL.Services.DepartmentServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using HrManagmentSystem.Controllers.EmployeesPersonalData.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.Departments
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentService departmentService;
        public DepartmentController(IDepartmentService _departmentService)
        {
            departmentService = _departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await departmentService.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await departmentService.insertAsync(department);
            return Ok(department);
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await departmentService.updateAsync(department);
            return Ok(department);
        }
        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            var department = await departmentService.GetByIdAsync(departmentId);        
            return Ok(department);
        }


    }
}


