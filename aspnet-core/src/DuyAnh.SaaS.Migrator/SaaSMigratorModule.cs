using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using DuyAnh.SaaS.Configuration;
using DuyAnh.SaaS.EntityFrameworkCore;
using DuyAnh.SaaS.Migrator.DependencyInjection;
using Castle.MicroKernel.Registration;
using Microsoft.Extensions.Configuration;

namespace DuyAnh.SaaS.Migrator;

[DependsOn(typeof(SaaSEntityFrameworkModule))]
public class SaaSMigratorModule : AbpModule
{
    private readonly IConfigurationRoot _appConfiguration;

    public SaaSMigratorModule(SaaSEntityFrameworkModule abpProjectNameEntityFrameworkModule)
    {
        abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

        _appConfiguration = AppConfigurations.Get(
            typeof(SaaSMigratorModule).GetAssembly().GetDirectoryPathOrNull()
        );
    }

    public override void PreInitialize()
    {
        Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
            SaaSConsts.ConnectionStringName
        );

        Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        Configuration.ReplaceService(
            typeof(IEventBus),
            () => IocManager.IocContainer.Register(
                Component.For<IEventBus>().Instance(NullEventBus.Instance)
            )
        );
    }

    public override void Initialize()
    {
        IocManager.RegisterAssemblyByConvention(typeof(SaaSMigratorModule).GetAssembly());
        ServiceCollectionRegistrar.Register(IocManager);
    }
}
