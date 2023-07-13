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
        public async Task<IEnumerable<Department>> Get()
        {
            var departments = await _Departmentrepository.GetAllAsync();
            return departments.Where(d => d.IsDeleted == false);
        }

        public async Task insert(Department department)
        {
            _Departmentrepository.InsertAsync(department);
            await _Departmentrepository.SaveAsync();
        }

        public async Task update(Department department)
        {
            _Departmentrepository.Update(department);
            await _Departmentrepository.SaveAsync();
        }
        public string GetDeptName(int departmentId)
        {
         
           var department= _Departmentrepository.GetByIdAsNoTracking(departmentId);
            return department.Name; 
        }
    }
}
