using Volo.Abp.Settings;

namespace HDHDC.Speedwave.Settings
{
    public class SpeedwaveSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(SpeedwaveSettings.MySetting1));
        }
    }
}
