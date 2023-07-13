using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.EmployeeContrActDataServices
{
    public class EmployeeContrActDataService : IEmployeeContrActDataService
    {
        private readonly IGenericRepository<EmployeeContractDate> _DataService;
        public EmployeeContrActDataService(IGenericRepository<EmployeeContractDate> DataService)
        {
            _DataService = DataService;
        }
        public async Task DeleteAsync(int empId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeContractDate>> GetAllAsync()
        {
            string Employees = "Employee";
            var IncludedEmployees = await _DataService.GetIncluded(Employees);
            var ContractDate = await _DataService.GetAllAsync();

            return ContractDate;
        }

        public async Task<EmployeeContractDate> GetByIdAsync(int Id)
        {
            string Employees = "Employee";
            var IncludedEmployees = await _DataService.GetIncluded(Employees);
            var ContractDate = await _DataService.GetAllAsync();
            return ContractDate.FirstOrDefault(e => e.EmpId == Id);
        }

        public async Task InsertAsync(EmployeeContractDate data)
        {
            _DataService.InsertAsync(data);
            await _DataService.SaveAsync();
        }

        public async Task SaveChanges()
        {
            await _DataService.SaveAsync();
        }

        public async Task UpdateAsync(EmployeeContractDate data)
        {
            _DataService.Update(data);
            await _DataService.SaveAsync();
        }
    }
}
