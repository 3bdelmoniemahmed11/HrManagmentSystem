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

        Task<IEnumerable<AnnualVacation>> GetAll();
        Task insert(AnnualVacation vacation);
        Task update(AnnualVacation vacation);
        Task<List<AnnualVacation>> GetAnnualVacationsByPeriod(DateTime startDate, DateTime endDate);
  

    }
}
