using HrManagment.BLL.Services.AttendanceService;
using HrManagment.BLL.Services.DepartmentServices;
using HrManagment.BLL.Services.EmployeeContrActDataServices;
using HrManagment.BLL.Services.EmployeeServices;
using HrManagment.DAL.Models;
using HrManagmentSystem.Controllers.EmployeesPersonalData.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.Employeee
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        
        
        private readonly IEmployeeService _employeeService;
        private readonly IEmployeeContrActDataService _contractDataService;
        private readonly IDepartmentService _departmentService;
        public EmployeeController(
            IEmployeeService employeeService
            , IEmployeeContrActDataService contractDataService,
            IDepartmentService departmentService
            )
        {
            _employeeService = employeeService;
            _contractDataService= contractDataService;
            _departmentService = departmentService;
        }



        //[HttpGet()]
        //public async Task<IActionResult> getAllAttendances()
        //{
        //    var res = await _employeeService.GetAllAsync();
        //    return Ok(res);
        //}
        ////alla
        [HttpGet]
        public async Task<IActionResult> getAllEmployees()
        {
            var Employees = await _employeeService.GetAllEmpsIncludingDeptAsync();

            List<EmployeesDataDTO> EmployeesData = new List<EmployeesDataDTO>();

            foreach (var employee in Employees)
            {
                EmployeesDataDTO employeesDataDTO = new EmployeesDataDTO();
                employeesDataDTO.Id = employee.Id;
                employeesDataDTO.Name = employee.Name;
                employeesDataDTO.Nationality = employee.Nationality;
                employeesDataDTO.Gender = employee.Gender;
                employeesDataDTO.Address = employee.Address;
                employeesDataDTO.Phone = employee.Phone;
                employeesDataDTO.NetSalary = employee.NetSalary;
                employeesDataDTO.BirthDate = employee.BirthDate.ToString();
                employeesDataDTO.SSN = employee.SSN;
                employeesDataDTO.AttendanceTime = employee.AttendanceTime.ToString();
                employeesDataDTO.DepartureTime = employee.DepartureTime.ToString();
                employeesDataDTO.DeptId = employee.DeptId;
                employeesDataDTO.Department = employee.Department.Name;
                employeesDataDTO.IsDeleted = employee.IsDeleted;
                var hirdaterecord = await _contractDataService.GetByIdAsync(employee.Id);
                employeesDataDTO.HirDate = hirdaterecord.HireDate.ToString();

                EmployeesData.Add(employeesDataDTO);




            }
            return Ok(EmployeesData);
        }
        [HttpGet("{empId}")]
        public async Task<IActionResult> GetEmpbyidAsync(int empId)
        {
            var employee = await _employeeService.GetByIdAsync(empId);

            var hirdate = await _contractDataService.GetByIdAsync(empId);

            EmployeesDataDTO employeesDataDTO = new EmployeesDataDTO();
            employeesDataDTO.Id = employee.Id;
            employeesDataDTO.Name = employee.Name;
            employeesDataDTO.Nationality = employee.Nationality;
            employeesDataDTO.Gender = employee.Gender;
            employeesDataDTO.Address = employee.Address;
            employeesDataDTO.Phone = employee.Phone;
            employeesDataDTO.NetSalary = employee.NetSalary;
            employeesDataDTO.BirthDate = employee.BirthDate.ToString();
            employeesDataDTO.SSN = employee.SSN;
            employeesDataDTO.AttendanceTime = employee.AttendanceTime.ToString();
            employeesDataDTO.DepartureTime = employee.DepartureTime.ToString();
            employeesDataDTO.DeptId = employee.DeptId;
            employeesDataDTO.IsDeleted = employee.IsDeleted;
            //edit it 
            var hirdaterecord = await _contractDataService.GetByIdAsync(employee.Id);
            employeesDataDTO.HirDate = hirdate.HireDate.ToString();
            return Ok(employeesDataDTO);
        }
        [HttpPost]
        public async Task<IActionResult> PostNewEmployee([FromBody] EmployeesDataDTO employee)
        {
            HrManagment.DAL.Models.Employee sendemployee = new HrManagment.DAL.Models.Employee();
            EmployeeContractDate hirdate = new EmployeeContractDate();
            sendemployee.Name = employee.Name;
            sendemployee.NetSalary = employee.NetSalary;
            sendemployee.Address = employee.Address;
            sendemployee.Gender = employee.Gender;
            sendemployee.Phone = employee.Phone;
            sendemployee.BirthDate = DateTime.Parse(employee.BirthDate);
            sendemployee.AttendanceTime = TimeSpan.Parse(employee.AttendanceTime);
            sendemployee.DepartureTime = TimeSpan.Parse(employee.DepartureTime);
            sendemployee.SSN = employee.SSN;
            sendemployee.Nationality = employee.Nationality;
            sendemployee.IsDeleted = false;

            sendemployee.DeptId = int.Parse(employee.Department);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeService.InsertAsync(sendemployee);


            hirdate.HireDate = DateTime.Parse(employee.HirDate);
            hirdate.EmpId = await _employeeService.GetEmployeeBySSN(employee.SSN);

            await _contractDataService.InsertAsync(hirdate);
            return Ok(employee);


        }
        [HttpPut]
        public async Task<IActionResult> EditEmployee([FromBody] EmployeesDataDTO employee)
        {
            HrManagment.DAL.Models.Employee sendemployee = new HrManagment.DAL.Models.Employee();
            EmployeeContractDate hirdate = new EmployeeContractDate();


            sendemployee.Id = employee.Id;
            sendemployee.Name = employee.Name;
            sendemployee.NetSalary = employee.NetSalary;
            sendemployee.Address = employee.Address;
            sendemployee.Gender = employee.Gender;
            sendemployee.Phone = employee.Phone;
            sendemployee.BirthDate = DateTime.Parse(employee.BirthDate);
            sendemployee.AttendanceTime = TimeSpan.Parse(employee.AttendanceTime);
            sendemployee.DepartureTime = TimeSpan.Parse(employee.DepartureTime);
            sendemployee.SSN = employee.SSN;
            sendemployee.IsDeleted = false;
            sendemployee.DeptId = int.Parse(employee.Department);
            hirdate.HireDate = DateTime.Parse(employee.HirDate);


            sendemployee.Nationality = employee.Nationality;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeService.UpdateAsync(sendemployee);
            return Ok(sendemployee);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _employeeService.DeleteAsync(id);
            return Ok(id);
        }

        [HttpGet("{departmentId}/getByDepartment")]
        public async Task<IActionResult> GetEmployeeByDepartment(int departmentId)
        {
            var emps = await _employeeService.GetEmployeeByDepartment(departmentId);
            if(emps.Count() > 0)
            {
                return Ok(new { Message = "Yes" });
            }
            return Ok(new { Message = "No" });
        }

        [HttpPut("{olddeptId:int}/{newdeptId:int}")]
        public async Task<IActionResult> UpdateEmployeeByDepartment(int oldDeptId, int newDeptID)
        {
            await _employeeService.updateEmployeeByDepartment(oldDeptId, newDeptID);
            return Ok(new { Message = "Done" });
        }

    }
}
