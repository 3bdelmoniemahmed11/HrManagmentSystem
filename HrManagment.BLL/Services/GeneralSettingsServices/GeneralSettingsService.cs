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
        public readonly IGenericRepository<GeneralSetting> Services;
        public GeneralSettingsService(IGenericRepository<GeneralSetting> _Services)
        {
            Services=_Services;
        }
        public async Task<IEnumerable<GeneralSetting>> GetAll()
        {
            return await Services.GetAllAsync();
        }

        public async Task<GeneralSetting> GetLastSetting()
        {
            var services = await Services.GetAllAsync();
            return (GeneralSetting)services.FirstOrDefault(g => g.EndDate == null);
        }

        public async Task Insert(GeneralSetting choices)
        {
            Services.InsertAsync(choices);
            await Services.SaveAsync();
        }

        public async Task Update(GeneralSetting choices)
        {
            Services.Update(choices);
            await Services.SaveAsync();
        }
    }
}
