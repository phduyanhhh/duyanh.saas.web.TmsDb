using Abp.Authorization;
using DuyAnh.SaaS.Authorization.Roles;
using DuyAnh.SaaS.Authorization.Users;

namespace DuyAnh.SaaS.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}
