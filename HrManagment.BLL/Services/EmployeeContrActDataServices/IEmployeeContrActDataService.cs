using HrManagment.DAL.Models;

namespace HrManagment.BLL.Services.EmployeeContrActDataServices
{
    public interface IEmployeeContrActDataService
    {
        Task<IEnumerable<EmployeeContractDate>> GetAllAsync();
        Task<EmployeeContractDate> GetByIdAsync(int EmployeeId);
        Task InsertAsync(EmployeeContractDate data);
        Task UpdateAsync(EmployeeContractDate data);
        Task DeleteAsync(int empId);
        Task SaveChanges();
    }
}