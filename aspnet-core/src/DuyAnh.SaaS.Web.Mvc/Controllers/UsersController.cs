using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DuyAnh.SaaS.Authorization;
using DuyAnh.SaaS.Controllers;
using DuyAnh.SaaS.Users;
using DuyAnh.SaaS.Web.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Web.Controllers;

[AbpMvcAuthorize(PermissionNames.Pages_Users)]
public class UsersController : SaaSControllerBase
{
	private readonly IUserAppService _userAppService;

	public UsersController(IUserAppService userAppService)
	{
		_userAppService = userAppService;
	}

	public async Task<ActionResult> Index()
	{
		var roles = (await _userAppService.GetRoles()).Items;
		var model = new UserListViewModel
		{
			Roles = roles
		};
		return View(model);
	}

	public async Task<IActionResult> CreateModal()
	{
		var roles = (await _userAppService.GetRoles()).Items;
		var model = new UserListViewModel
		{
			Roles = roles
		};
		return PartialView("_CreateModal" ,model);
	}

	public async Task<ActionResult> EditModal(long userId)
	{
		var user = await _userAppService.GetAsync(new EntityDto<long>(userId));
		var roles = (await _userAppService.GetRoles()).Items;
		var model = new EditUserModalViewModel
		{
			User = user,
			Roles = roles
		};
		return PartialView("_EditModal", model);
	}

	public ActionResult ChangePassword()
	{
		return View();
	}
}
