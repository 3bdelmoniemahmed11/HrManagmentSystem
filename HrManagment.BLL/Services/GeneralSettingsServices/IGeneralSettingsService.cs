using HrManagment.DAL.Models;

namespace HrManagment.BLL.Services.GeneralSettingServices
{
    public interface IGeneralSettingsService
    {
        Task<IEnumerable<GeneralSetting>> GetAll();
        Task<GeneralSetting> GetLastSetting();
        Task Insert(GeneralSetting choices);
        Task Update(GeneralSetting choices);
        Task<GeneralSetting> GetDeducation_Addation();

    }
}