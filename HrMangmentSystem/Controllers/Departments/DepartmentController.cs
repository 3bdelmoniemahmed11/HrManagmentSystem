using HrManagment.BLL.Services.DepartmentServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
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
        public async Task<IActionResult> Get()
        {
            var result = await departmentService.Get();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await departmentService.insert(department);
            return Ok(department);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await departmentService.update(department);
            return Ok(department);

        }

    }
}


