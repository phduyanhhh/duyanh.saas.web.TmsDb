using DuyAnh.SaaS.Configuration.Dto;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
