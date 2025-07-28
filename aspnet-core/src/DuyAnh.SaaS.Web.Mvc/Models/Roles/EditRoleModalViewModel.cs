using Abp.AutoMapper;
using DuyAnh.SaaS.Roles.Dto;
using DuyAnh.SaaS.Web.Models.Common;

namespace DuyAnh.SaaS.Web.Models.Roles;

[AutoMapFrom(typeof(GetRoleForEditOutput))]
public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
{
    public bool HasPermission(FlatPermissionDto permission)
    {
        return GrantedPermissionNames.Contains(permission.Name);
    }
}
