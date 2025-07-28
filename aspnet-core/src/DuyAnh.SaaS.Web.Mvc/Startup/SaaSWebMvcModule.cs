using Abp.Modules;
using Abp.Reflection.Extensions;
using DuyAnh.SaaS.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DuyAnh.SaaS.Web.Startup;

[DependsOn(typeof(SaaSWebCoreModule))]
public class SaaSWebMvcModule : AbpModule
{
    private readonly IWebHostEnvironment _env;
    private readonly IConfigurationRoot _appConfiguration;

    public SaaSWebMvcModule(IWebHostEnvironment env)
    {
        _env = env;
        _appConfiguration = env.GetAppConfiguration();
    }

    public override void PreInitialize()
    {
        Configuration.Navigation.Providers.Add<SaaSNavigationProvider>();
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SaaSWebMvcModule).GetAssembly());
    }
}
