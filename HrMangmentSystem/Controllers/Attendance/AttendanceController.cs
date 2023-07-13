using AutoMapper;
using HrManagment.BLL.Services.AttendanceService;
using HrManagment.DAL.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HrManagment.DAL.Models;

using HrManagment.BLL.Services.EmployeeServices;


namespace HrManagmentSystem.Controllers.Attendance
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMapper _imapper;
        private readonly IEmployeeService _employeeService;
        public AttendanceController(IAttendanceService attendanceService, IMapper imapper, IEmployeeService employeeService)
        {
            _attendanceService = attendanceService;
            _imapper = imapper;
            _employeeService = employeeService;

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
        [HttpPost("attendances")]
        public async Task<IActionResult> AddEmpAtt([FromBody] List<EmpAttendanceDTO> attendancesDTO)
        {
            var employess = await _employeeService.GetAllAsync();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var attendanceDTO in attendancesDTO)
            {
                var employee = employess.FirstOrDefault(e => e.SSN.Trim() == attendanceDTO.SSN.Trim());
                if (employee != null)
                {
                    attendanceDTO.EmpId = employee.Id;
                    
                }
            }


            List<HrManagment.DAL.Models.Attendance> attendance = new List<HrManagment.DAL.Models.Attendance> ();
            attendance = _imapper.Map<List<HrManagment.DAL.Models.Attendance>>(attendancesDTO);
            await _attendanceService.InsertListAsync(attendance);
            return Ok("done");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getById(int id)
        {
            var attendance = await _attendanceService.GetByIdAsync(id);

            return Ok(attendance);
            
        }

        [HttpPut]
        public async Task<IActionResult> update([FromBody]EmpAttendanceDTO attendance)
        {
            var Exist = await _attendanceService.GetByIdAsynAsNoTracking(attendance.Id);
            if (Exist == null)
            {
                return NotFound("Attendance Not found");

            }           
            var mappedAtt = _imapper.Map<HrManagment.DAL.Models.Attendance>(attendance);
            await  _attendanceService.Update(mappedAtt);
            return Ok(attendance);

        }

    }
}
