using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HrManagment.BLL.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int EmployeeId);
        Task<IEnumerable<Employee>> GetAllEmpsIncludingDeptAsync();

        Task InsertAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int empId);
        public Task<IEnumerable<Employee>> GetEmployee_Deparmtnet();
        public Task<int> GetEmployeeBySSN(string empssn);
        public Task<IEnumerable<Employee>> GetEmployeeByDepartment(int department_id);
        public Task updateEmployeeByDepartment(int departmentold, int departmentnew);
        
    }
}
