using DuyAnh.SaaS.Roles.Dto;
using System.Collections.Generic;

namespace DuyAnh.SaaS.Web.Models.Common;

public interface IPermissionsEditViewModel
{
    List<FlatPermissionDto> Permissions { get; set; }
}