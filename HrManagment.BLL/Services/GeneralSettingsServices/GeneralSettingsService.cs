using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.GeneralSettingsServices
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly IGenericRepository<GeneralSetting> _genericRepository ;
        public GeneralSettingsService(IGenericRepository<GeneralSetting> genericRepository)
        {
            _genericRepository= genericRepository;
        }
        public async Task<GeneralSetting> GetDeducation_Addation()
        {
            var generalSettings= await  _genericRepository.GetFilteredAsync(gs => gs.EndDate == null);
            return   generalSettings.FirstOrDefault();
        }
    }
}
