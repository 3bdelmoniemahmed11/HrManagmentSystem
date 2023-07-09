using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.AnnualVacationServices
{
    public interface IAnnualVacationService
    {
        Task<List<AnnualVacation>> GetAnnualVacationsByPeriod(DateTime startDate, DateTime endDate);

    }
}
