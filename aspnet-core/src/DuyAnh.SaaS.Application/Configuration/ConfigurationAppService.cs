using Abp.Authorization;
using Abp.Runtime.Session;
using DuyAnh.SaaS.Configuration.Dto;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Configuration;

[AbpAuthorize]
public class ConfigurationAppService : SaaSAppServiceBase, IConfigurationAppService
{
    public async Task ChangeUiTheme(ChangeUiThemeInput input)
    {
        await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
    }
}
