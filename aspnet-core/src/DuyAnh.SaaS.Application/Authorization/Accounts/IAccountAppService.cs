using Abp.Application.Services;
using DuyAnh.SaaS.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
