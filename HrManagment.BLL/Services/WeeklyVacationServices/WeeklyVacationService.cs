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
    public class WeeklyVacationService : IWeeklyVacationService
    {
        private readonly IGenericRepository<WeeklyVacation> _weeklyVacationRepository;
        public WeeklyVacationService(IGenericRepository<WeeklyVacation> weeklyVacationRepository)
        {
            _weeklyVacationRepository = weeklyVacationRepository;
        }

      
        public async Task<int> GetNumberOfWeeklyVacationsByPeriod(DateTime startDate, DateTime endDate)
        {

            int NumberOfWeeklyDays = 0;
            //retriving the vactions and filter the weekly vacations
            var vacations = await _weeklyVacationRepository.GetFilteredAsync( v => v.EndDate == null );
            var WeeklyVacationsNames = vacations.Select(vac=>vac.DayName.ToLower().Trim()).ToList();
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

        public async Task<IEnumerable<WeeklyVacation>> GetWeekDays()
        {
            var days = await _weeklyVacationRepository.GetAllAsync();
            return days.Where(d=>d.EndDate == null);
        }

        public async Task InsertWeekDays(IEnumerable<WeeklyVacation> weeklyVacations)
        {
            foreach (var item in weeklyVacations)
            {
                _weeklyVacationRepository.InsertAsync(item);
            }
       await   _weeklyVacationRepository.SaveAsync();
        }

        public async Task UpdateWeekDays(IEnumerable<WeeklyVacation> weeklyVacations)
        {
            foreach (var item in weeklyVacations)
            {
                _weeklyVacationRepository.Update(item);
            }
         await    _weeklyVacationRepository.SaveAsync();
        }
    }
}
