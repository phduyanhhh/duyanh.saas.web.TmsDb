using Abp.AspNetCore.Mvc.Authorization;
using DuyAnh.SaaS.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace DuyAnh.SaaS.Web.Controllers;

[AbpMvcAuthorize]
public class HomeController : SaaSControllerBase
{
    public ActionResult Index()
    {
        return View();
    }
}
