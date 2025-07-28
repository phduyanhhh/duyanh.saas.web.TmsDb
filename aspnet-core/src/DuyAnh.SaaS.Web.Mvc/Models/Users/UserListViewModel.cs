using DuyAnh.SaaS.Roles.Dto;
using System.Collections.Generic;

namespace DuyAnh.SaaS.Web.Models.Users;

public class UserListViewModel
{
    public IReadOnlyList<RoleDto> Roles { get; set; }
}
