using HrManagment.BLL.Services.GeneralSettingServices;
using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.Weekly_Vacation
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeeklyVacationServicesController : ControllerBase
    {
        IWeeklyVacationService weeklyVacationService;
        public WeeklyVacationServicesController(IWeeklyVacationService _weeklyVacationService)
        {
            weeklyVacationService = _weeklyVacationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await weeklyVacationService.GetWeekDays();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] IEnumerable<WeeklyVacation> weeklyVacations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await weeklyVacationService.InsertWeekDays(weeklyVacations);
            return Ok(weeklyVacations);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] IEnumerable<WeeklyVacation> weeklyVacations)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await weeklyVacationService.UpdateWeekDays(weeklyVacations);
            return Ok(weeklyVacations);

        }


    }
}