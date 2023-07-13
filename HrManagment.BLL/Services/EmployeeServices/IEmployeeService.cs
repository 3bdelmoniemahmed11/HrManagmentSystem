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
    }
}
