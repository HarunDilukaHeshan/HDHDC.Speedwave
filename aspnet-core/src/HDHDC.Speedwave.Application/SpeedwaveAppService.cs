using System;
using System.Collections.Generic;
using System.Text;
using HDHDC.Speedwave.Localization;
using Volo.Abp.Application.Services;

namespace HDHDC.Speedwave
{
    /* Inherit your application services from this class.
     */
    public abstract class SpeedwaveAppService : ApplicationService
    {
        protected SpeedwaveAppService()
        {
            LocalizationResource = typeof(SpeedwaveResource);
        }
    }
}
