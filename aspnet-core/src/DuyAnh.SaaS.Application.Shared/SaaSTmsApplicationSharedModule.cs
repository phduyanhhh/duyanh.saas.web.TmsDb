using Abp.Modules;
using Abp.Reflection.Extensions;

namespace DuyAnh.SaaS.Application.Shared
{
	public class SaaSTmsApplicationSharedModule : AbpModule
	{
		public override void Initialize()
		{
			IocManager.RegisterAssemblyByConvention(typeof(SaaSTmsApplicationSharedModule).GetAssembly());
		}
	}
}
