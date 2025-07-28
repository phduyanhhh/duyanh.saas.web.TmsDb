using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DuyAnh.SaaS.EntityFrameworkCore;
using DuyAnh.SaaS.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DuyAnh.SaaS.Web.Tests;

[DependsOn(
    typeof(SaaSWebMvcModule),
    typeof(AbpAspNetCoreTestBaseModule)
)]
public class SaaSWebTestModule : AbpModule
{
    public SaaSWebTestModule(SaaSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
    }

    public override void PreInitialize()
    {
        Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SaaSWebTestModule).GetAssembly());
    }

    public override void PostInitialize()
    {
        IocManager.Resolve<ApplicationPartManager>()
            .AddApplicationPartsIfNotAddedBefore(typeof(SaaSWebMvcModule).Assembly);
    }
}