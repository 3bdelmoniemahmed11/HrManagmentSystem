using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.VacationServices
{
    public interface IWeeklyVacationService
    {
        Task<int> GetNumberOfWeeklyVacationsByPeriod(DateTime startDate, DateTime endDate);
    }
}
