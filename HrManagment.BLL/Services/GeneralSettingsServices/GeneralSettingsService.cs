using HrManagment.DAL.Models;
using HrManagment.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.GeneralSettingServices
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        public readonly IGenericRepository<GeneralSetting> _generialSettingsRepository;
        public GeneralSettingsService(IGenericRepository<GeneralSetting> generialSettingsRepository)
        {
            _generialSettingsRepository = generialSettingsRepository;
        }
        public async Task<IEnumerable<GeneralSetting>> GetAll()
        {
            return await _generialSettingsRepository.GetAllAsync();
        }

        public async Task<GeneralSetting> GetLastSetting()
        {
            var services = await _generialSettingsRepository.GetAllAsync();
            return (GeneralSetting)services.FirstOrDefault(g => g.EndDate == null);
        }

        public async Task Insert(GeneralSetting choices)
        {
            _generialSettingsRepository.InsertAsync(choices);
            await _generialSettingsRepository.SaveAsync();
        }

        public async Task Update(GeneralSetting choices)
        {
            _generialSettingsRepository.Update(choices);
            await _generialSettingsRepository.SaveAsync();
        }
        public async Task<GeneralSetting> GetDeducation_Addation()
        {
            var generalSettings = await _generialSettingsRepository.GetFilteredAsync(gs => gs.EndDate == null);
            return generalSettings.FirstOrDefault();
        }
    }
}
