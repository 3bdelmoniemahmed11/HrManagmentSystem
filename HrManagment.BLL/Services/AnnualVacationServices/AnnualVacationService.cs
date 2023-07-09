using HrManagment.BLL.Services.VacationServices;
using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.AnnualVacationServices
{
    public class AnnualVacationService: IAnnualVacationService
    {
        private readonly IGenericRepository<AnnualVacation> _annualVacationRepository;
        public AnnualVacationService(IGenericRepository<AnnualVacation> annualVacationRepository)
        {
            _annualVacationRepository = annualVacationRepository;
        }
        public async Task<List<AnnualVacation>> GetAnnualVacationsByPeriod(DateTime startDate, DateTime endDate)
        {
            var annualVacations = await _annualVacationRepository
                .GetFilteredAsync(vac => vac.VacationDate.Date >= startDate.Date && vac.VacationDate.Date <= endDate.Date && vac.EndDate == null);
            return  annualVacations.ToList();
        }


    }
}
