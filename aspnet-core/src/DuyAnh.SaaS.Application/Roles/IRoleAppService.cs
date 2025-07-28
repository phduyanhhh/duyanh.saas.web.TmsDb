using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DuyAnh.SaaS.Roles.Dto;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Roles;

public interface IRoleAppService : IAsyncCrudAppService<RoleDto, int, PagedRoleResultRequestDto, CreateRoleDto, RoleDto>
{
    Task<ListResultDto<PermissionDto>> GetAllPermissions();

    Task<GetRoleForEditOutput> GetRoleForEdit(EntityDto input);

    Task<ListResultDto<RoleListDto>> GetRolesAsync(GetRolesInput input);
}
