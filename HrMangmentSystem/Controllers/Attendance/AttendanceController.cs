using AutoMapper;
using HrManagment.BLL.Services.AttendanceService;
using HrManagment.DAL.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HrManagment.DAL.Models;

namespace HrManagmentSystem.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _imapper;
        public AttendanceController(IAttendanceService attendanceService, IMapper imapper)
        {
            _attendanceService = attendanceService;
            _imapper = imapper;
        }

        [HttpGet]
        public async Task<IActionResult> getAllAttendances()
        {
            var res = await _attendanceService.GetAllEmpsAttendances();
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmpAtt(int id)
        {
            await _attendanceService.Delete(id);
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmpAtt([FromBody] EmpAttendanceDTO att)
        {      
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mappedAtt = _imapper.Map<HrManagment.DAL.Models.Attendance>(att);
            await _attendanceService.AddAttAsync(mappedAtt);
            return Ok(mappedAtt);
        }

    }
}
