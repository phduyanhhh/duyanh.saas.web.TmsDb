using Abp.AutoMapper;
using DuyAnh.SaaS.Sessions.Dto;

namespace DuyAnh.SaaS.Web.Views.Shared.Components.TenantChange;

[AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
public class TenantChangeViewModel
{
    public TenantLoginInfoDto Tenant { get; set; }
}
