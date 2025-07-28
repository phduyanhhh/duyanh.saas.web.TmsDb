using DuyAnh.SaaS.Application.Shared.Languages;
using System.Collections.Generic;
using DuyAnh.SaaS.Controllers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DuyAnh.SaaS.Web.Controllers
{
	public class LanguagesController : SaaSControllerBase
	{
		private readonly ILanguagesAppService _languagesAppService;
		public LanguagesController(
			ILanguagesAppService languagesAppService
			)
		{
			_languagesAppService = languagesAppService;
		}

		public ActionResult Index()
		{
			return View();
		}

		public IActionResult DownloadJson()
		{
			var data = new
			{
				languageName = "Ký hiện tên ngôn ngữ (vd: vi, en,...)",
				translations = new Dictionary<string, string>
					{
						{ "Welcome", "Chào mừng bạn" },
						{ "Logout", "Đăng xuất" },
						{ "Login", "Đăng nhập" }
					}
			};
			var jsonString = JsonConvert.SerializeObject(data, Formatting.Indented);
			var bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);

			return File(bytes, "application/json", "language.json");
		}

		public IActionResult UploadFile()
		{
			return PartialView("_UploadFile");
		}

		public IActionResult Create()
		{
			return PartialView("_Create");
		}

	}
}
