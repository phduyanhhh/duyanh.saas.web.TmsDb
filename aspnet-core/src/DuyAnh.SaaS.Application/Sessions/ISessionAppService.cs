using Abp.Application.Services;
using DuyAnh.SaaS.Sessions.Dto;
using System.Threading.Tasks;

namespace DuyAnh.SaaS.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
