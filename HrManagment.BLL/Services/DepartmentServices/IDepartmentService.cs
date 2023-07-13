using HrManagment.DAL.Models;

namespace HrManagment.BLL.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> Get();
        Task insert(Department department);
        Task update(Department department);
        public  string GetDeptName(int departmentId);

    }
}