using Abp.MultiTenancy;
using DuyAnh.SaaS.Authorization.Users;

namespace DuyAnh.SaaS.MultiTenancy;

public class Tenant : AbpTenant<User>
{
    public Tenant()
    {
    }

    public Tenant(string tenancyName, string name)
        : base(tenancyName, name)
    {
    }
}
