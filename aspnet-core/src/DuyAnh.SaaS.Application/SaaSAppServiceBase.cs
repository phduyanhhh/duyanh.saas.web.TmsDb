using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using DuyAnh.SaaS.Authorization.Users;
using DuyAnh.SaaS.MultiTenancy;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace DuyAnh.SaaS;

/// <summary>
/// Derive your application services from this class.
/// </summary>
public abstract class SaaSAppServiceBase : ApplicationService
{
    public TenantManager TenantManager { get; set; }

    public UserManager UserManager { get; set; }

    protected SaaSAppServiceBase()
    {
        LocalizationSourceName = SaaSConsts.LocalizationSourceName;
    }

    protected virtual async Task<User> GetCurrentUserAsync()
    {
        var user = await UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
        if (user == null)
        {
            throw new Exception("There is no current user!");
        }

        return user;
    }

    protected virtual Task<Tenant> GetCurrentTenantAsync()
    {
        return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
    }

    protected virtual void CheckErrors(IdentityResult identityResult)
    {
        identityResult.CheckErrors(LocalizationManager);
    }
}
