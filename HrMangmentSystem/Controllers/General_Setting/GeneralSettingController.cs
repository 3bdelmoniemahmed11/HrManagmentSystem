using HrManagment.BLL.Services.GeneralSettingServices;
using HrManagment.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HrManagmentSystem.Controllers.General_Setting
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralSettingController : ControllerBase
    {

        private readonly IGeneralSettingsService generalSetting;
        public GeneralSettingController(IGeneralSettingsService _generalSetting)
        {
            generalSetting = _generalSetting;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(generalSetting.GetLastSetting());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GeneralSetting settingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          await   generalSetting.Insert(settingModel);
            return Ok(settingModel);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] GeneralSetting choice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           await generalSetting.Update(choice);
            return Ok(choice);

        }


    }
}
