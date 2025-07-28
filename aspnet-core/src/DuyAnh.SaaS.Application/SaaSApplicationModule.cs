using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DuyAnh.SaaS.Authorization;

namespace DuyAnh.SaaS;

[DependsOn(
    typeof(SaaSCoreModule),
    typeof(AbpAutoMapperModule))]
public class SaaSApplicationModule : AbpModule
{
    public override void PreInitialize()
    {
        Configuration.Authorization.Providers.Add<SaaSAuthorizationProvider>();
    }

    public override void Initialize()
    {
        var thisAssembly = typeof(SaaSApplicationModule).GetAssembly();

        IocManager.RegisterAssemblyByConvention(thisAssembly);

        Configuration.Modules.AbpAutoMapper().Configurators.Add(
            // Scan the assembly for classes which inherit from AutoMapper.Profile
            cfg => cfg.AddMaps(thisAssembly)
        );
    }
}
