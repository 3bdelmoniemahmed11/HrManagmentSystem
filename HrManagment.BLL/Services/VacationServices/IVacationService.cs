using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.VacationServices
{
    public interface IVacationService
    {
        Task<List<Vacation>> GetAnnualVacationsByPeriod(DateTime startDate ,DateTime endDate);
        Task<int> GetNumberOfWeeklyVacationsByPeriod(DateTime startDate, DateTime endDate);
    }
}
