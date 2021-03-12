using HDHDC.Speedwave.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HDHDC.Speedwave.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class SpeedwaveController : AbpController
    {
        protected SpeedwaveController()
        {
            LocalizationResource = typeof(SpeedwaveResource);
        }
    }
}