using Abp.Configuration.Startup;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DuyAnh.SaaS.Sessions;
using DuyAnh.SaaS.Web.Views;
using DuyAnh.SaaS.Web.Areas.App.Views.Shared.Components.AppSideBarUserArea;

namespace DuyAnh.SaaS.Web.Areas.App.Views.Shared.Components.AppSideBarUserArea
{
	public class AppSideBarUserAreaViewComponent : SaaSViewComponent 
	{
		private readonly ISessionAppService _sessionAppService;
		private readonly IMultiTenancyConfig _multiTenancyConfig;

		public AppSideBarUserAreaViewComponent(
						ISessionAppService sessionAppService,
						IMultiTenancyConfig multiTenancyConfig)
		{
			_sessionAppService = sessionAppService;
			_multiTenancyConfig = multiTenancyConfig;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var model = new AppSideBarUserAreaViewModel
			{
				LoginInformations = await _sessionAppService.GetCurrentLoginInformations(),
				IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
			};
			//return View(model);
			return View("~/Areas/App/Views/Shared/Components/AppSideBarUserArea/Default.cshtml", model);
		}
	}
}
