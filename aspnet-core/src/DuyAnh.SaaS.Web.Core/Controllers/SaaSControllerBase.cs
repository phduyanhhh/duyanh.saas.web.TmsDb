using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace DuyAnh.SaaS.Controllers
{
    public abstract class SaaSControllerBase : AbpController
    {
        protected SaaSControllerBase()
        {
            LocalizationSourceName = SaaSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
