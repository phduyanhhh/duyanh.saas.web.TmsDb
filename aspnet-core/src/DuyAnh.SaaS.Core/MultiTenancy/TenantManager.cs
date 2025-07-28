using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using DuyAnh.SaaS.Authorization.Users;
using DuyAnh.SaaS.Editions;

namespace DuyAnh.SaaS.MultiTenancy;

public class TenantManager : AbpTenantManager<Tenant, User>
{
    public TenantManager(
        IRepository<Tenant> tenantRepository,
        IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
        EditionManager editionManager,
        IAbpZeroFeatureValueStore featureValueStore)
        : base(
            tenantRepository,
            tenantFeatureRepository,
            editionManager,
            featureValueStore)
    {
    }
}
