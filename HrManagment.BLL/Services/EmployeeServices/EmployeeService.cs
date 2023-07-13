using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _EmployeeRepository;
        public EmployeeService(IGenericRepository<Employee> EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _EmployeeRepository.GetFilteredAsync(e => e.IsDeleted == false);
        }
       
        public async Task<Employee> GetByIdAsync(int EmployeeId)
        {
           return await _EmployeeRepository.GetByIdAsync(EmployeeId);
        }

        public async Task<IEnumerable<Employee>> GetAllEmpsIncludingDeptAsync()
        {
            return await _EmployeeRepository.GetFilteredIncluded(e => e.IsDeleted == false, "Department");
        }

        public async Task UpdateAsync(Employee employee)
        {
            _EmployeeRepository.Update(employee);
            await _EmployeeRepository.SaveAsync();
        }
        public async Task InsertAsync(Employee employee)
        {
            _EmployeeRepository.InsertAsync(employee);
            await _EmployeeRepository.SaveAsync();


        }
        public async Task DeleteAsync(int empId)
        {
            var employee = await _EmployeeRepository.GetByIdAsync(empId);
            employee.IsDeleted = true;
            await _EmployeeRepository.SaveAsync();
        }
        public async Task<IEnumerable<Employee>> GetEmployee_Deparmtnet()
        {
            var res = await _EmployeeRepository.GetIncluded("Department");


            return res;
        }

        public async Task<int> GetEmployeeByPhone(string empPhone)
        {
            var res = await _EmployeeRepository.GetFilteredAsync(emp => emp.Phone == empPhone);


            return  res.FirstOrDefault().Id;
        }

    }
}
