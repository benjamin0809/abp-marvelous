using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using TalentMatrix.Configuration.Dto;

namespace TalentMatrix.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : TalentMatrixAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
