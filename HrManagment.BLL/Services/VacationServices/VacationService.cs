using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.VacationServices
{
    public class VacationService : IVacationService
    {
        private readonly IGenericRepository<Vacation> _vacationRepository;
        public VacationService(IGenericRepository<Vacation> vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }
        public async Task<List<Vacation>> GetAnnualVacationsByPeriod(DateTime startDate, DateTime endDate)
        {
            var vacations = await _vacationRepository.GetAllAsync();
            var annualVacations=vacations.Where(vac=>vac.Type=="Annual"&&vac.Date.Date>=startDate.Date&&vac.Date.Date<=endDate.Date);
            return await annualVacations.ToListAsync();
        }

        public async Task<int> GetNumberOfWeeklyVacationsByPeriod(DateTime startDate, DateTime endDate)
        {

            int NumberOfWeeklyDays = 0;
            //retriving the vactions and filter the weekly vacations
            var vacations = await _vacationRepository.GetAllAsync();
            var WeeklyVacationsNames =  await vacations.Where(vac => vac.Type == "Weekly").Select(vac=>vac.Name).ToListAsync();
            //to calculate the number of weekly days 
            while (startDate.Date <= endDate.Date)
            {
                string currentDayName = startDate.ToString("dddd").ToLower().Trim();

                if (WeeklyVacationsNames.Contains(currentDayName))
                {
                    NumberOfWeeklyDays++;
                }

                startDate = startDate.Date.AddDays(1);
            }
            return NumberOfWeeklyDays;
        }
    }
}
