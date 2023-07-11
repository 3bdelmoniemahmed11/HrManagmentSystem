using HrManagment.BLL.Services.AnnualVacationServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.Annual_Vacation
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnualVacationsController : ControllerBase
    {
        IAnnualVacationService annualVacationService;
        public AnnualVacationsController(IAnnualVacationService _annualVacationService)
        {
            annualVacationService = _annualVacationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await annualVacationService.GetAll();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AnnualVacation annualVacation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await annualVacationService.insert(annualVacation);
            return Ok(annualVacation);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AnnualVacation annualVacation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await annualVacationService.update(annualVacation);
            return Ok(annualVacation);

        }


    }
}
