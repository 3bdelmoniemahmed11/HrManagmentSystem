using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        IGenericRepository<Department> _Departmentrepository;
        public DepartmentService(IGenericRepository<Department> Departmentrepository)
        {
            _Departmentrepository = Departmentrepository;
        }
        public async Task<IEnumerable<Department>> GetAsync()
        {
            var departments = await _Departmentrepository.GetAllAsync();
            return departments.Where(d => d.IsDeleted == false);
        }

        public async Task insertAsync(Department department)
        {
            await _Departmentrepository.InsertAsync(department);
            await _Departmentrepository.SaveAsync();
        }

        public async Task updateAsync(Department department)
        {
            await _Departmentrepository.Update(department);
            await _Departmentrepository.SaveAsync();
        }
        public async Task<string> GetDeptName(int departmentId)
        {
         
           var department= await _Departmentrepository.GetByIdAsynAsNoTracking(departmentId);
            return department.Name; 
        }

    
        public async Task<Department> GetByIdAsync(int departmentId)
        {
            return await _Departmentrepository.GetByIdAsync(departmentId);
        }
    }
}
