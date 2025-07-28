using Abp.Modules;
using Abp.Reflection.Extensions;
using DuyAnh.SaaS.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DuyAnh.SaaS.Web.Host.Startup
{
    [DependsOn(
       typeof(SaaSWebCoreModule))]
    public class SaaSWebHostModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SaaSWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SaaSWebHostModule).GetAssembly());
        }
    }
}
