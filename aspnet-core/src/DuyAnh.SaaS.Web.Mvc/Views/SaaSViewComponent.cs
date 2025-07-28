using Abp.AspNetCore.Mvc.ViewComponents;

namespace DuyAnh.SaaS.Web.Views;

public abstract class SaaSViewComponent : AbpViewComponent
{
    protected SaaSViewComponent()
    {
        LocalizationSourceName = SaaSConsts.LocalizationSourceName;
    }
}
