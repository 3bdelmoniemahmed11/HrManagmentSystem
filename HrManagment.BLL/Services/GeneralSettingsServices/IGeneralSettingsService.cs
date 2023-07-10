using HrManagment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManagment.BLL.Services.GeneralSettingsServices
{
    public interface IGeneralSettingsService
    {
        Task<GeneralSetting> GetDeducation_Addation();
    }
}
