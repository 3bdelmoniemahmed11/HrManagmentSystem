using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.SalaryClickLogServices
{
    public class SalaryClickLogService : ISalaryClickLogService
    {
        private readonly IGenericRepository<SalaryClickLog> _salaryClickLogRepository;
        public SalaryClickLogService(IGenericRepository<SalaryClickLog> salaryClickLogRepository)
        {
            _salaryClickLogRepository = salaryClickLogRepository;
        }
        public async Task<DateTime> GetStartDate()
        {
            //var allDates = await _salaryClickLogRepository.GetAllAsync();
            //var startDate = allDates.LastOrDefault();
            //if (startDate != null)
            //{
            //    return startDate.Date;
            //}
            ////the first day from the actual month  and year 
            //var defaultDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //return defaultDate;

            var allStartDates = await _salaryClickLogRepository.GetAllAsync();
            var startDate = allStartDates.OrderBy(d => d.Id).LastOrDefault();

            if (startDate != null)
            {
                return startDate.Date;
            }
            //the first day from the actual month  and year 
            var defaultDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            return defaultDate;


        }



    }
}
