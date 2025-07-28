using DuyAnh.SaaS.Roles.Dto;
using System.Collections.Generic;

namespace DuyAnh.SaaS.Web.Models.Roles;

public class RoleListViewModel
{
    public IReadOnlyList<PermissionDto> Permissions { get; set; }
}
