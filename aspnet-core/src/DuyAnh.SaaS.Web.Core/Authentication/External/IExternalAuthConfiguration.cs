using System.Collections.Generic;

namespace DuyAnh.SaaS.Authentication.External
{
    public interface IExternalAuthConfiguration
    {
        List<ExternalLoginProviderInfo> Providers { get; }
    }
}
