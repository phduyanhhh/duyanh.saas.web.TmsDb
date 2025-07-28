using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using DuyAnh.SaaS.Authorization;
using DuyAnh.SaaS.Controllers;
using DuyAnh.SaaS.Roles;
using DuyAnh.SaaS.Web.Models.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Web.Controllers;

[AbpMvcAuthorize(PermissionNames.Pages_Roles)]
public class RolesController : SaaSControllerBase
{
	private readonly IRoleAppService _roleAppService;

	public RolesController(IRoleAppService roleAppService)
	{
		_roleAppService = roleAppService;
	}

	public async Task<IActionResult> Index()
	{
		var permissions = (await _roleAppService.GetAllPermissions()).Items;
		var model = new RoleListViewModel
		{
			Permissions = permissions
		};

		return View(model);
	}

	public async Task<IActionResult> Create()
	{
		var permissions = (await _roleAppService.GetAllPermissions()).Items;
		var model = new RoleListViewModel
		{
			Permissions = permissions
		};
		return PartialView("_CreateModal", model);
	}

	public async Task<IActionResult> Edit(int roleId)
	{
		var output = await _roleAppService.GetRoleForEdit(new EntityDto(roleId));
		var model = ObjectMapper.Map<EditRoleModalViewModel>(output);

		return PartialView("_EditModal", model);
	}
}
