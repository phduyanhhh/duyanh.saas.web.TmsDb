using Abp.Application.Services;
using DuyAnh.SaaS.MultiTenancy.Dto;

namespace DuyAnh.SaaS.MultiTenancy;

public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
{
}

